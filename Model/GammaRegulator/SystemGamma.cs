using System;
using Common.Entities;
using Common.WinApi;
using Common.WinApi.Entities;

namespace Model.GammaRegulator;

internal static class SystemGamma
{
    /// <summary>
    /// Применяет параметры цветовой гаммы для устройства
    /// </summary>
    public static void ApplyColorConfiguration(ref ColorConfiguration colorConfiguration, nint deviceContext)
    {
        const ushort maxChannelValue = 256;
        const ushort channelMult = 255;

        var RGBmask = ConvertKelvinsToRGB(colorConfiguration.ColorTemperature);

        var channels = new GammaRamp
        {
            Red = new ushort[maxChannelValue],
            Green = new ushort[maxChannelValue],
            Blue = new ushort[maxChannelValue]
        };

        for (var i = 0; i < maxChannelValue; i++)
        {
            channels.Red[i] = (ushort)(i * channelMult * RGBmask.Red * colorConfiguration.Brightness);
            channels.Green[i] = (ushort)(i * channelMult * RGBmask.Green * colorConfiguration.Brightness);
            channels.Blue[i] = (ushort)(i * channelMult * RGBmask.Blue * colorConfiguration.Brightness);
        }

        var successfully = WinApiWrapper.SetScreenGamma(deviceContext, ref channels);
        if (!successfully)
        {
            // TODO: Обработать
            //Console.WriteLine(
            //    $"Could not set gamma for screen : {deviceContext}" +
            //    $" [DC: {deviceContext} Color Temperature: {colorConfiguration.ColorTemperature} " +
            //    $"Brightness: {colorConfiguration.Brightness}]");
        }
    }

    /// <summary>
    /// Конвертирует цветовую температуру (Kelvin) в RGB формат
    /// </summary>
    /// <remarks> <see href="http://tannerhelland.com/4435/convert-temperature-rgb-algorithm-code">Источник алгоритма</see> </remarks>
    /// <returns> Цветовая маска <see cref="RGBMask"/></returns>
    private static RGBMask ConvertKelvinsToRGB(double kelvins)
    {
        return new RGBMask
        (
            red: kelvins > 6600
                ? Math.Clamp(Math.Pow(kelvins / 100 - 60, -0.1332047592) * 329.698727446 / 255, 0, 1)
                : 1,
            green: kelvins > 6600
                ? Math.Clamp(Math.Pow(kelvins / 100 - 60, -0.0755148492) * 288.1221695283 / 255, 0, 1)
                : Math.Clamp((Math.Log(kelvins / 100) * 99.4708025861 - 161.1195681661) / 255, 0, 1),
            blue: kelvins >= 6600
                ? 1
                : kelvins <= 1900
                    ? 0
                    : Math.Clamp((Math.Log(kelvins / 100 - 10) * 138.5177312231 - 305.0447927307) / 255, 0, 1)
        );
    }
}