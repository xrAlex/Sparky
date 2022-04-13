using System;
using System.Collections.Generic;
using System.ComponentModel;
using Common.Infrastructure.Commands;

namespace View.Localization
{
    /// <summary>Локализатор. Все значения содержатся в статических членах.
    /// Экземпляры - это прокси к статическим членам.</summary>
    public sealed class Localizator : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        // Словарь всех локализаций.
        private static readonly Dictionary<object, LocalizationDto> LocalizationsDict = new();

        /// <summary>Словарь локализаций.</summary>
        public Dictionary<object, LocalizationDto> Localizations => LocalizationsDict;

        // Содержит список всех созданных экземпляров.
        private static readonly List<WeakReference> Instances = new();

        public static Localizator Instance { get; } = new();

        private static LocalizationDto? _defaultDto;
        private static LocalizationDto? _currentDto;

        private static readonly PropertyChangedEventArgs CurrentArg = new(nameof(Current));
        private static readonly PropertyChangedEventArgs DefaultArg = new(nameof(Default));
        public RelayCommand SetLocalizationCommand => SetLocalization;

        public LocalizationDto? Default
        {
            get => _defaultDto;
            set => Set(DefaultArg, ref _defaultDto, value);
        }

        public LocalizationDto? Current
        {
            get => _currentDto;
            set => Set(CurrentArg, ref _currentDto, value);
        }

        public static RelayCommand SetLocalization { get; } = new(
            key =>
            {
                Instance.Current = key == null ? Instance.Default : LocalizationsDict[key];
            },
            key => key == null || LocalizationsDict.ContainsKey(key));


        // Создаёт экземпляр и записывает его в список экземпляров.
        public Localizator()
        {
            Instances.Add(new WeakReference(this));
        }

        // Подымает PropertyChanged для всех существующих экземпляров.
        private static void Set<T>(PropertyChangedEventArgs arg, ref T field, T value)
        {
            if (Equals(field, value)) return;
            field = value;

            for (int i = Instances.Count - 1; i >= 0; i--)
            {
                // Проверка существования экземпляра
                var localizator = Instances[i].Target as Localizator;
                if (localizator == null)
                {
                    // Если экземпляра нет, то удаляется слабая ссылка из списка.
                    Instances.RemoveAt(i);
                }
                else
                {
                    // Если есть экземпляр то для него подымается PropertyChanged.
                    localizator.PropertyChanged?.Invoke(localizator, arg);
                }
            }
        }
    }
}
