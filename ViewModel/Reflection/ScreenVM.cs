﻿using Common.Entities;
using Common.Infrastructure.INPC;
using Common.Interfaces;

namespace ViewModel.Reflection
{
    public class ScreenVM : INPCBase
    {
        private IScreenContext _screen = null!;
        private bool _isSelected;
        private float _imageOpacity = 1.0f;
        private float _dayBrightness;
        private float _dayColorTemperature;
        private float _nightBrightness;
        private float _nightColorTemperature;
        private byte _nightStartHour;
        private byte _nightStartMinute;
        private byte _dayStartHour;
        private byte _dayStartMinute;

        public IScreenContext Screen
        {
            get => _screen;
            set => Set(ref _screen!, value);
        }

        public bool IsSelected
        {
            get => _isSelected;
            set => Set(ref _isSelected, value);
        }

        public float ImageOpacity
        {
            get => _imageOpacity;
            set => Set(ref _imageOpacity, value);
        }

        public float DayBrightness
        {
            get => _dayBrightness; 
            set => Set(ref _dayBrightness, value);
        }

        public float DayColorTemperature 
        { 
            get => _dayColorTemperature; 
            set => Set(ref _dayColorTemperature, value);
        }

        public float NightBrightness
        {
            get => _nightBrightness;
            set => Set(ref _nightBrightness, value);
        }

        public float NightColorTemperature
        {
            get => _nightColorTemperature;
            set => Set(ref _nightColorTemperature, value);
        }

        public byte NightStartHour
        {
            get => _nightStartHour;
            set => Set(ref _nightStartHour, value);
        }

        public byte NightStartMin
        {
            get => _nightStartMinute;
            set => Set(ref _nightStartMinute, value);
        }

        public byte DayStartHour
        {
            get => _dayStartHour;
            set => Set(ref _dayStartHour, value);
        }

        public byte DayStartMin
        {
            get => _dayStartMinute;
            set => Set(ref _dayStartMinute, value);
        }

        protected override void OnPropertyChanged(in string propertyName, in object oldValue, in object newValue)
        {
            base.OnPropertyChanged(propertyName, oldValue, newValue);

            switch (propertyName)
            {
                case nameof(DayColorTemperature):
                case nameof(DayBrightness):
                    Screen.DayColorConfiguration = new ColorConfiguration(DayColorTemperature, DayBrightness);
                    break;
                case nameof(NightColorTemperature):
                case nameof(NightBrightness):
                    Screen.NightColorConfiguration = new ColorConfiguration(NightColorTemperature, NightBrightness);
                    break;
                case nameof(NightStartHour):
                case nameof(NightStartMin):
                    Screen.NightStartTime = new PeriodStartTime(NightStartHour, _nightStartMinute);
                    break;
                case nameof(DayStartHour):
                case nameof(DayStartMin):
                    Screen.DayStartTime = new PeriodStartTime(NightStartHour, _nightStartMinute);
                    break;
            }
        }

        public override string ToString() 
            => _screen.FriendlyName;

        public ScreenVM(IScreenContext screen)
        {
            Screen = screen;
        }
    }
}
