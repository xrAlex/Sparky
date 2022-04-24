using Common.Interfaces;
using Model.Repository;
using Newtonsoft.Json;

namespace Model.Settings;

internal sealed partial class AppSettingsModel : IAppSettingsModel
{
    private readonly string _filePath;

    [JsonProperty("Screens data")]
    public ScreenRepository ScreenRepository { get; } = new();

    [JsonProperty("Ignored applications")]
    public ApplicationRepository IgnoredAppRepository { get; } = new();

    [JsonProperty("Full screen check enabled")]
    public bool IsFullScreenAppCheckEnabled { get; set; }

    [JsonProperty("Gamma smoothing enabled")]
    public bool IsGammaSmoothingEnabled { get; set; }

    [JsonProperty("Localization")]
    public string? CurrentLocalizationKey { get; set; }

    [JsonIgnore]
    public bool Loaded { get; private set; }

    public AppSettingsModel(string configurationFilePath)
    {
        _filePath = configurationFilePath;
    }
}