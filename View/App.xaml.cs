using Common.Infrastructure;
using Common.Infrastructure.Commands;
using Common.Infrastructure.ViewModelTemplate;
using Common.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Xml;
using View.Localization;
using View.Views;
using ViewModel;

namespace View;

public partial class App : Application
{
    private static readonly string ConfigurationFilepath
        = $"{Environment.CurrentDirectory}" + "\\Settings.json";

    private IAppSettingsModel? _settings;
    private IPeriodObserverModel? _observer;
}

public partial class App
{
    protected override void OnStartup(StartupEventArgs e)
    {
        Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

        ConfigureIoC();

        _settings = IoC.GetInstance<IAppSettingsModel>();
        _observer = IoC.GetInstance<IPeriodObserverModel>();

        _settings.Load();
        _observer.RefreshAllScreensColorConfiguration();
        _observer.StartWatch();

        SetupLocalizations();

        new MainWindow().Show();

        base.OnStartup(e);
    }
}

public partial class App
{
    protected override void OnExit(ExitEventArgs e)
    {
        _observer?.StopWatch();
        _observer?.ForceDefaultColorConfiguration();

        base.OnExit(e);
    }
}

public class LocalizatorApp : Localizator
{
    private List<LocalizationImage>? _images;
    private LocalizationImage? _selectedKey;

    private const string demoXml =
@"<?xml version='1.0' encoding='utf-8' ?>
<Localization
    Language = 'Rus'
    Apply='Применить'
    AutoLaunch='Запускаться вместе с Windows'
    Brightness='Яркость'
    Cancel='Отменить'
    Day='День'
    ColorTemperature='Цветовая температура'
    DontWorkInFullScreen='Не работать в полноэкранных приложениях'
    ExtendedGammaRange='Расширенный диапозон гаммы'
    Night='Ночь'
    ApplicationsWhiteList='Список исключений'
    Reset='Сбросить'
    SmoothBrightnessChange='Плавное изменение гаммы'
    Restart='Для применения параметров требуется рестарт'
    Sunrise='Восход'
    Monitors='Мониторы'
    Sunset='Закат'
    ToTrayNotification='Приложение продолжит работу в свернутом состоянии'
    TrayClose='Закрыть'
    TrayPause='Пауза'
    TrayUnPause='Продолжить'
/>";
    public LocalizatorApp()
    {
        if (Instance is not LocalizatorApp)
            SetInstance();

        if (ViewModelBase.IsInDesignModeStatic)
        {
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(demoXml));
            using var xmlr = XmlReader.Create(stream);
            var demo = LocalizationDto.ParseXml(xmlr);
            if (!Localizations.ContainsKey(demo.Language))
                Localizations.Add(new LocalizationImage() { Name = demo.Language }, demo);
            Default = demo;
            Current = demo;
        }
        else
        {

            var rus = GetLocalization("View.Localization.Dictionaries.RusLocalization.xml");
            var eng = GetLocalization("View.Localization.Dictionaries.EngLocalization.xml");

            if (!Localizations.ContainsKey(rus.Language))
                Localizations.Add(new LocalizationImage() { Name = rus.Language }, rus);
            if (!Localizations.ContainsKey(eng.Language))
                Localizations.Add(new LocalizationImage() { Name = eng.Language }, eng);
            Default = eng;
            Current = rus;
        }
    }
    public static LocalizationDto? GetLocalization(string embeddedFileName)
    {
        using var resourceStream = Assembly
            .GetCallingAssembly()
            .GetManifestResourceStream(embeddedFileName);

        if (resourceStream == null)
            throw new ArgumentNullException(nameof(resourceStream));

        using var xmlr = XmlReader.Create(resourceStream);
        return LocalizationDto.ParseXml(xmlr);
    }

    public List<LocalizationImage>? Images
    {
        get => _images; set
        {
            _images = value;
            _images.ForEach(image =>
            {
                foreach (var item in Instance.Localizations)
                {
                    if (item.Key is LocalizationImage img && string.Equals(img.Name, image.Name))
                    {
                        img.Image = image.Image;
                        break;
                    }
                }
            });
        }
    }

    public LocalizationImage? SelectedKey
    {
        get => _selectedKey; set
        {
            if (Equals(_selectedKey, value)) return;

            _selectedKey = value;
            SetLocalization.TryExecute(value);
        }
    }
}

public class ListImages : List<LocalizationImage> { }
public class LocalizationImage
{
    public string? Name { get; set; }
    public ImageSource? Image { get; set; }

    public override string? ToString() => Name;
}

public partial class App
{
    private static void SetupLocalizations()
    {
    }


    /// <summary>
    /// Конфигурация контейнера инверсии управления
    /// </summary>
    private static void ConfigureIoC()
    {
        var container = IoC.Instance;
        ModelRegistrator.Register(container, ConfigurationFilepath);
        ViewModelRegistrator.Register(container);
    }
}