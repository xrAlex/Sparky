using Common.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace View.Localization.WithDto
{
    /// <summary>Локализатор. Все значения содержатся в статических членах.
    /// Экземпляры - это прокси к статическим членам.</summary>
    public class Localizator : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        // Содержит список всех созданных экземпляров.
        private static readonly List<WeakReference> instances = new();

        // Создаёт экземпляр и записывает его в список экземпляров.
        public Localizator()
        {
            instances.Add(new WeakReference(this));
        }

        // Подымает PropertyChanged для всех существующих экземпляров.
        private static void RaisePropertyChanged(PropertyChangedEventArgs arg)
        {
            for (int i = instances.Count - 1; i >= 0; i--)
            {
                // Проверка существования экземпляра
                Localizator? localizator = instances[i].Target as Localizator;
                if (localizator == null)
                {
                    // Если экземпляра нет, то удаляется слабая ссылка из списка.
                    instances.RemoveAt(i);
                }
                else
                {
                    // Если есть экземпляр то для него подымается PropertyChanged.
                    localizator.PropertyChanged?.Invoke(localizator, arg);
                }
            }
        }

        // Словарь всех локализаций.
        private static readonly Dictionary<object, LocalizationDto> localizations = new();

        /// <summary>Словарь локализаций.</summary>
        public Dictionary<object, LocalizationDto> Localizations => localizations;

        public static Localizator Instance { get; } = new Localizator();

        private static LocalizationDto? defaultDto;
        private static readonly PropertyChangedEventArgs defaultArg = new PropertyChangedEventArgs(nameof(Default));
        public LocalizationDto? Default
        {
            get => defaultDto; set
            {
                if (Equals(value, defaultDto)) return;
                defaultDto = value;
                RaisePropertyChanged(defaultArg);
            }
        }


        private static LocalizationDto? currentDto;
        private static readonly PropertyChangedEventArgs currentArg = new PropertyChangedEventArgs(nameof(Current));
        public LocalizationDto? Current
        {
            get => defaultDto; set
            {
                if (Equals(value, currentDto)) return;
                currentDto = value;
                RaisePropertyChanged(currentArg);
            }
        }

        public static RelayCommand SetLocalization { get; } = new RelayCommand(
            key =>
            {
                if (key == null)
                    Instance.Current = defaultDto;
                else
                    Instance.Current = localizations[key];
            },
            key => key == null || localizations.ContainsKey(key));

        public RelayCommand SetLocalizationCommand => SetLocalization;
    }
}
