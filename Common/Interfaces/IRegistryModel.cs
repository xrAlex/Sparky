namespace Common.Interfaces
{
    public interface IRegistryModel
    {
        bool IsAppStartupKeyFounded();
        bool IsExtendedGammaRangeActive();
        void AddAppStartupKey();
        void DeleteAppStartupKey();
        void SetDefaultGammaRangeKey();
        void SetExtendedGammaRangeKey();
    }
}
