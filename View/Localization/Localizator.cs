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
            set
            {
                if (Equals(value, _defaultDto)) return;
                _defaultDto = value;
                RaisePropertyChanged(DefaultArg);
            }
        }

        public LocalizationDto? Current
        {
            get => _defaultDto; 
            set
            {
                if (Equals(value, _currentDto)) return;
                _currentDto = value;
                RaisePropertyChanged(CurrentArg);
            }
        }

        public static RelayCommand SetLocalization { get; } = new (
            key =>
            {
                Instance.Current = key == null ? _defaultDto : LocalizationsDict[key];
            },
            key => key == null || LocalizationsDict.ContainsKey(key));


        // Создаёт экземпляр и записывает его в список экземпляров.
        public Localizator()
        {
            Instances.Add(new WeakReference(this));
        }

        // Подымает PropertyChanged для всех существующих экземпляров.
        private static void RaisePropertyChanged(PropertyChangedEventArgs arg)
        {
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
