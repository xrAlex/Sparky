using Common.Interfaces;
using Model.Repository;

namespace Model.Settings;

internal sealed partial class AppSettingsModel : IAppSettingsModel
{
    private readonly string _filePath;
    public ScreenRepository ScreenRepository { get; } = new();
    public ApplicationRepository IgnoredAppRepository { get; } = new();
    public bool IsFullScreenAppCheckEnabled { get; set; }
    public bool IsGammaSmoothingEnabled { get; set; }
    public string CurrentLocalizationKey { get; set; } = "Rus";

    public AppSettingsModel(string configurationFilePath)
    {
        _filePath = configurationFilePath;
    }
}