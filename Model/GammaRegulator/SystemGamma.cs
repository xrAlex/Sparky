using System;
using Common.Entities;
using Common.WinApi;

namespace Model.GammaRegulator
{
    internal static class SystemGamma
    {
        /// <summary>
        /// Converts color configuration to RGB colors for work with WinApi
        /// </summary>
        public static void ApplyColorConfiguration(ColorConfiguration colorConfiguration, string screenName)
        {
            var dc = Native.CreateDC(screenName, null, null, 0);
            const int maxChannelValue = 256;
            const int channelMult = 255;

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

            var successfully = Native.SetDeviceGammaRamp(dc, ref channels);
            if (!successfully)
            {
                // TODO: Обработать
                Console.WriteLine(
                    $"Could not set gamma for screen : {screenName}" +
                    $" [DC: {dc} Color Temperature: {colorConfiguration.ColorTemperature} " +
                    $"Brightness: {colorConfiguration.Brightness}]");
            }

            Native.DeleteDC(dc);
        }

        /// <summary>
        /// Converts color temperature (Kelvin) to RGB format
        /// </summary>
        /// <remarks> <see href="http://tannerhelland.com/4435/convert-temperature-rgb-algorithm-code">Algorithm source</see> </remarks>
        /// <returns> <see cref="RGBMask"/> color mask </returns>
        private static RGBMask ConvertKelvinsToRGB(double kelvins)
        {
            RGBMask mask = new
            (
                kelvins > 6600
                    ? Math.Clamp(Math.Pow(kelvins / 100 - 60, -0.1332047592) * 329.698727446 / 255, 0, 1)
                    : 1,
                kelvins > 6600
                    ? Math.Clamp(Math.Pow(kelvins / 100 - 60, -0.0755148492) * 288.1221695283 / 255, 0, 1)
                    : Math.Clamp((Math.Log(kelvins / 100) * 99.4708025861 - 161.1195681661) / 255, 0, 1),
                kelvins >= 6600
                    ? 1
                    : kelvins <= 1900
                        ? 0
                        : Math.Clamp((Math.Log(kelvins / 100 - 10) * 138.5177312231 - 305.0447927307) / 255, 0, 1)
            );


            return mask;
        }
    }
}
