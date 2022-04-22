using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.Entities;
using Common.Extensions;
using Common.Interfaces;
using Common.WinApi;
using Model.Screen;
using Model.Settings;

namespace Model.PeriodObserver;

internal sealed class PeriodObserverModel : IPeriodObserverModel
{
    private readonly AppSettingsModel _settings;
    private readonly ScreenModel _screenModel;
    private CancellationTokenSource? _cts;

    public event EventHandler? ObserverStarted;
    public event EventHandler? ObserverStopped;

    private enum Period
    {
        Day,
        Night
    }

    public PeriodObserverModel(IAppSettingsModel settings, IScreenModel screenModel)
    {
        _settings = (AppSettingsModel)settings;
        _screenModel = (ScreenModel)screenModel;
    }

    /// <inheritdoc cref="IPeriodObserverModel.StartWatch()"/>
    public void StartWatch()
    {
        _cts = new CancellationTokenSource();
        Task.Run(() => Cycle(_cts.Token), _cts.Token).ConfigureAwait(false);
    }

    /// <inheritdoc cref="IPeriodObserverModel.StopWatch"/>
    public void StopWatch()
    {
        _cts?.Cancel();
    }

    /// <inheritdoc cref="IPeriodObserverModel.RefreshAllScreensColorConfiguration"/>
    public void RefreshAllScreensColorConfiguration()
    {
        foreach (var screen in _screenModel.GetAllScreens())
        {
            var (currentPeriod, _) = GetCurrentPeriod(screen);
            screen.CurrentColorConfiguration = currentPeriod == Period.Day
                ? screen.DayColorConfiguration
                : screen.NightColorConfiguration;
        }
    }

    /// <inheritdoc cref="IPeriodObserverModel.ForceDefaultColorConfiguration"/>
    public void ForceDefaultColorConfiguration()
    {
        foreach (var screen in _screenModel.GetAllScreens())
        {
            screen.SetDefaultColorConfiguration();
        }
    }

    /// <summary>
    /// Циклично проверяет настройки отображения устройств
    /// </summary>
    private void Cycle(CancellationToken token)
    {
        ObserverStarted?.Invoke(this, EventArgs.Empty);

        while (!token.IsCancellationRequested)
        {
            foreach (var screen in _screenModel.GetAllScreens())
            {
                RefreshColorConfiguration(screen);
            }
            Thread.Sleep(100);
        }

        ObserverStopped?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Обновляет цветовую конфигурацию утсройства отображения для текущего периода времени
    /// </summary>
    private void RefreshColorConfiguration(IScreenContext screen)
    {
        var (currentPeriod, remainingTime) = GetCurrentPeriod(screen);
        var newConfiguration = GetNewColorConfiguration(currentPeriod, screen, remainingTime);

        if (!screen.CurrentColorConfiguration.IsCloseTo(newConfiguration))
        {
            newConfiguration = SmoothOutColorTransition(screen.CurrentColorConfiguration, newConfiguration);
        }

        screen.CurrentColorConfiguration = newConfiguration;
    }

    /// <summary>
    /// Подсчитывает значения цветовой конфигурации для текщуего период времени
    /// </summary>
    private ColorConfiguration GetNewColorConfiguration(Period currentPeriod, IScreenContext screen, float remainingTime)
    {
        var screenDayConfig = screen.DayColorConfiguration;
        var screenNightConfig = screen.NightColorConfiguration;

        if (_settings.IsFullScreenAppCheckEnabled)
        {
            if (currentPeriod == Period.Night && IsFullScreenAppFounded(screen))
            {
                return screenDayConfig;
            }
        }

        if (_settings.IsGammaSmoothingEnabled && remainingTime > 0 && remainingTime <= 10)
        {
            return currentPeriod == Period.Day
                ? GetTransientColorConfiguration(screenNightConfig, screenDayConfig, remainingTime)
                : GetTransientColorConfiguration(screenDayConfig, screenNightConfig, remainingTime);
        }

        return currentPeriod == Period.Day ? screenDayConfig : screenNightConfig;
    }

    /// <summary>
    /// Сглаживает переход цветовой конфигурации от периода к периоду
    /// </summary>
    private static ColorConfiguration SmoothOutColorTransition(ColorConfiguration currentConfiguration, ColorConfiguration targetConfiguration)
    {
        var temperature = currentConfiguration.ColorTemperature;
        var brightness = currentConfiguration.Brightness;
        var targetTemperature = targetConfiguration.ColorTemperature;
        var targetBrightness = targetConfiguration.Brightness;
        const float temperatureStepSize = 60f;
        const float brightnessStepSize = 0.016f;

        var temperatureAbsDelta = Math.Abs(targetTemperature - temperature);
        var brightnessAbsDelta = Math.Abs(targetBrightness - brightness);

        var temperatureSteps = temperatureAbsDelta / temperatureStepSize;
        var brightnessSteps = brightnessAbsDelta / brightnessStepSize;

        var temperatureAdaptedStep = temperatureSteps >= brightnessSteps
            ? temperatureStepSize
            : Math.Abs(temperatureAbsDelta / brightnessSteps);

        var brightnessAdaptedStep = brightnessSteps >= temperatureSteps
            ? brightnessStepSize
            : Math.Abs(brightnessAbsDelta / temperatureSteps);

        temperature = targetTemperature >= temperature
            ? (temperature + temperatureAdaptedStep).ClampMax(targetTemperature)
            : (temperature - temperatureAdaptedStep).ClampMin(targetTemperature);

        brightness = targetBrightness >= brightness
            ? (brightness + brightnessAdaptedStep).ClampMax(targetBrightness)
            : (brightness - brightnessAdaptedStep).ClampMin(targetBrightness);

        return new ColorConfiguration(temperature, brightness);
    }

    /// <summary>
    /// Получает текущее значение цветовой конфигурации
    /// в зависимоти от оставшегося времени до смены периода
    /// </summary>
    private static ColorConfiguration GetTransientColorConfiguration(ColorConfiguration targetValues, ColorConfiguration startValues, float remainingTime)
    {
        if (startValues.IsCloseTo(targetValues))
        {
            return targetValues;
        }

        var multiplier = remainingTime * 0.1f;

        return new ColorConfiguration
        (
            startValues.ColorTemperature.Lerp(targetValues.ColorTemperature, multiplier),
            startValues.Brightness.Lerp(targetValues.Brightness, multiplier)
        );
    }

    /// <summary>
    /// Возвращает текущий период со временем до его кончания
    /// </summary>
    private static (Period period, float remainingTime) GetCurrentPeriod(IScreenContext screen)
    {
        var dayStart = screen.DayStartTime;
        var nightStart = screen.NightStartTime;
        var currentTime = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, 0);
        var nightStartTime = new TimeSpan(nightStart.Hour, nightStart.Minute, 0);
        var dayStartTime = new TimeSpan(dayStart.Hour, dayStart.Minute, 0);
        float remainingTime;

        // время смены конфигурации в текущем дне
        if (dayStartTime <= nightStartTime)
        {
            if (currentTime >= dayStartTime && currentTime < nightStartTime)
            {
                remainingTime = (float)(nightStartTime.TotalMinutes - currentTime.TotalMinutes);
                return (Period.Day, remainingTime);
            }

            remainingTime = (float)(dayStartTime.TotalMinutes - currentTime.TotalMinutes);
            return (Period.Night, remainingTime);
        }

        // время смены конфигурации в следующем дне
        if (currentTime >= dayStartTime || currentTime < nightStartTime)
        {
            const int midNight = 1440;
            if (currentTime.TotalMinutes == 0)
            {
                remainingTime = (float)(nightStartTime.TotalMinutes - currentTime.TotalMinutes);
            }
            else
            {
                remainingTime = (float)(nightStartTime.TotalMinutes - (currentTime.TotalMinutes - midNight));
            }
            return (Period.Day, remainingTime);
        }

        remainingTime = (float)(dayStartTime.TotalMinutes - currentTime.TotalMinutes);
        return (Period.Night, remainingTime);
    }

    /// <summary>
    /// Проверяет есть ли в системе окно развернутое на полный экран
    /// </summary>
    private bool IsFullScreenAppFounded(IScreenContext screen)
    {
        var screenBounds = screen.Bounds;
        return WinApiWrapper.IsForegroundWindowOnFullScreen(ref screenBounds, out var windowHandle)
               && !IsAppExePathInIgnored(WinApiWrapper.TryGetExecutablePath(windowHandle));
    }

    /// <summary>
    /// Проверяет есть в в игрнориемых пользователем приложениях
    /// путь указанного приложения
    /// </summary>
    private bool IsAppExePathInIgnored(string? processExePath)
        => _settings.IgnoredAppRepository.GetData().Contains(processExePath);
}