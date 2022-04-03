﻿using Common.Entities;
using Common.Infrastructure.INPC;
using Common.Interfaces;
using Model.GammaRegulator;

namespace Model.Entities.Domain
{
    /// <summary>
    /// Контекст данных источника отображения
    /// </summary>
    internal sealed class ScreenContext : INPCBase, IScreenContext
    {
        private bool _isActive = true;
        private ColorConfiguration _dayColorConfiguration = new (6600f, 1f);
        private ColorConfiguration _nightColorConfiguration = new (5400f, 0.8f);
        private PeriodStartTime _dayStartTime = new (7,0);
        private PeriodStartTime _nightStartTime = new (23,0);
        private ColorConfiguration _currentColorConfiguration = new(6600f, 1f);
        private ScreenBounds _bounds;

        /// <inheritdoc cref="IScreenContext.SystemName"/>
        public string SystemName { get; }

        /// <inheritdoc cref="IScreenContext.FriendlyName"/>
        public string FriendlyName { get; }

        /// <inheritdoc cref="IScreenContext.DisplayCode"/>
        public int DisplayCode { get; }

        /// <inheritdoc cref="IScreenContext.SystemHandle"/>
        public nint SystemHandle { get; }

        /// <inheritdoc cref="IScreenContext.IsActive"/>
        public bool IsActive
        {
            get => _isActive;
            set => Set(ref _isActive, value);
        }

        /// <inheritdoc cref="IScreenContext.DayColorConfiguration"/>
        public ColorConfiguration DayColorConfiguration
        {
            get => _dayColorConfiguration;
            set => Set(ref _dayColorConfiguration, value);
        }

        /// <inheritdoc cref="IScreenContext.NightColorConfiguration"/>
        public ColorConfiguration NightColorConfiguration
        {
            get => _nightColorConfiguration;
            set => Set(ref _nightColorConfiguration, value);
        }

        /// <inheritdoc cref="IScreenContext.CurrentColorConfiguration"/>
        public ColorConfiguration CurrentColorConfiguration
        {
            get => _currentColorConfiguration;
            set => Set(ref _currentColorConfiguration, value);
        }

        /// <inheritdoc cref="IScreenContext.DayStartTime"/>
        public PeriodStartTime DayStartTime
        {
            get => _dayStartTime;
            set => Set(ref _dayStartTime, value);
        }

        /// <inheritdoc cref="IScreenContext.NightStartTime"/>
        public PeriodStartTime NightStartTime
        {
            get => _nightStartTime;
            set => Set(ref _nightStartTime, value);
        }

        /// <inheritdoc cref="IScreenContext.Bounds"/>
        public ScreenBounds Bounds
        {
            get => _bounds;
            set => Set(ref _bounds, value);
        }

        public override string ToString()
            => FriendlyName;

        protected override void OnPropertyChanged(in string propertyName, in object oldValue, in object newValue)
        {
            base.OnPropertyChanged(in propertyName, in oldValue, in newValue);

            switch (propertyName)
            {
                case nameof(NightColorConfiguration):
                    CurrentColorConfiguration = NightColorConfiguration;
                    break;
                case nameof(DayColorConfiguration):
                    CurrentColorConfiguration = DayColorConfiguration;
                    break;
                case nameof(CurrentColorConfiguration):
                    if (IsActive)
                    {
                        SystemGamma.ApplyColorConfiguration((ColorConfiguration)newValue, SystemHandle);
                    }
                    break;
            }
        }

        /// <summary>
        /// Создает контекст устрйоства отображения на основе пользовательских настроек.
        /// </summary>
        public ScreenContext(ScreenSystemParams systemParams, ScreenUserSettings screenSettings)
        {
            DisplayCode = screenSettings.DisplayCode;
            IsActive = screenSettings.IsActive;
            SystemName = systemParams.SystemName;
            FriendlyName = systemParams.FriendlyName;
            Bounds = systemParams.Bounds;
            SystemHandle = systemParams.Handle;
            _dayColorConfiguration = screenSettings.DayColorConfiguration;
            _nightColorConfiguration = screenSettings.NightColorConfiguration;
            _isActive = screenSettings.IsActive;
            _dayStartTime = screenSettings.DayStartTime;
            _nightStartTime = screenSettings.NightStartTime;
        }

        /// <summary>
        /// Создает контекст устроqства отображения.
        /// </summary>
        public ScreenContext(ScreenSystemParams systemParams)
        {
            DisplayCode = systemParams.DisplayCode;
            SystemName = systemParams.SystemName;
            FriendlyName = systemParams.FriendlyName;
            Bounds = systemParams.Bounds;
            SystemHandle = systemParams.Handle;
        }
    }
}