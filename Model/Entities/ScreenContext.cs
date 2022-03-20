﻿using System;
using System.Runtime.InteropServices.ComTypes;
using Common.Entities;
using Common.Infrastructure.INPC;
using Common.Interfaces;
using Model.GammaRegulator;

namespace Model.Entities
{
    /// <summary>
    /// Контекст данных источника отображения
    /// </summary>
    internal sealed class ScreenContext : INPCBase, IScreenContext
    {
        private bool _isActive;
        private ColorConfiguration _dayColorConfiguration;
        private ColorConfiguration _nightColorConfiguration;
        private ColorConfiguration _currentColorConfiguration;
        private PeriodStartTime _dayStartTime;
        private PeriodStartTime _nightStartTime;
        private ScreenBounds _bounds;

        /// <inheritdoc cref="IScreenContext.SystemName"/>
        public string SystemName { get; }

        /// <inheritdoc cref="IScreenContext.FriendlyName"/>
        public string FriendlyName { get; }

        /// <inheritdoc cref="IScreenContext.DisplayCode"/>
        public int DisplayCode { get; }

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

            // TODO: надо бы это уюрать отсюда
            switch (propertyName)
            {
                case nameof(CurrentColorConfiguration):
                    SystemGamma.ApplyColorConfiguration(CurrentColorConfiguration, SystemName);
                    break;
                case nameof(NightColorConfiguration):
                    SystemGamma.ApplyColorConfiguration(NightColorConfiguration, SystemName);
                    break;
                case nameof(DayColorConfiguration):
                    SystemGamma.ApplyColorConfiguration(DayColorConfiguration, SystemName);
                    break;
            }
        }

        public ScreenContext(int displayCode, string systemName, string friendlyName)
        {
            if (string.IsNullOrWhiteSpace(systemName))
            {
                throw new ArgumentNullException(nameof(systemName));
            }

            DisplayCode = displayCode;
            SystemName = systemName;
            FriendlyName = friendlyName;
        }
    }
}
