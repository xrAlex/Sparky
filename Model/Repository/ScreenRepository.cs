using Model.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Common.Entities;
using Model.Entities.Domain;

namespace Model.Repository
{
    /// <summary>
    /// Хранилище данных ScreenModel
    /// </summary>
    internal sealed class ScreenRepository
    {
        [JsonProperty]
        private readonly Dictionary<int, ScreenUserSettings> _screens = new();

        public bool TryAdd(int key, ScreenContext screen)
        {
            if (_screens.ContainsKey(key))
            {
                return false;
            }

            var screenSettings = new ScreenUserSettings(screen.DisplayCode)
            {
                DayColorConfiguration = screen.DayColorConfiguration,
                DayStartTime = screen.DayStartTime,
                IsActive = screen.IsActive,
                NightColorConfiguration = screen.NightColorConfiguration,
                NightStartTime = screen.NightStartTime,
                Bounds = screen.Bounds
            };

            _screens.Add(key, screenSettings);
            return true;
        }

        
        public bool TryGetValue(int key, [MaybeNullWhen(false)] out ScreenUserSettings value)
            => _screens.TryGetValue(key, out value);

        public bool TryUpdate(int key, string? property, object? newValue)
        {
            if (!TryGetValue(key, out var screen) || property == null || newValue == null)
            {
                return false;
            }

            switch (property)
            {
                case nameof(ScreenUserSettings.DayColorConfiguration):
                    screen.DayColorConfiguration = (ColorConfiguration)newValue;
                    break;
                case nameof(ScreenUserSettings.DayStartTime):
                    screen.DayStartTime = (PeriodStartTime)newValue;
                    break;
                case nameof(ScreenUserSettings.IsActive):
                    screen.IsActive = (bool)newValue;
                    break;
                case nameof(ScreenUserSettings.NightColorConfiguration):
                    screen.NightColorConfiguration = (ColorConfiguration)newValue;
                    break;
                case nameof(ScreenUserSettings.NightStartTime):
                    screen.NightStartTime = (PeriodStartTime) newValue;
                    break;
            }

            return true;
        }

        public void Delete(int key) 
            => _screens.Remove(key);

        public void Clear()
            => _screens.Clear();

        public IReadOnlyDictionary<int, ScreenUserSettings> GetData() 
            => _screens.ToDictionary(key => key.Key, value => value.Value);
    }
}
