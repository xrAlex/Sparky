using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Infrastructure.INPC
{
    /// <summary>Расширяет <see cref="PropertyChangedEventArgs"/> свойством
    /// для передачи нового значения изменивщегося свойства.</summary>
    public sealed class PropertyChangedValueEventArgs : PropertyChangedEventArgs
    {
        /// <summary>Новое значение изменившегося свойства.</summary>
        public object? NewValue { get; }

        /// <summary>Создаёт новый экземпляр <see cref="PropertyChangedValueEventArgs"/>.</summary>
        /// <param name="propertyName">Имя изменивщегося свойства.</param>
        /// <param name="newValue">Новое значение изменивщегося свойства.</param>
        public PropertyChangedValueEventArgs(string? propertyName, object? newValue) : base(propertyName)
        {
            NewValue = newValue;
        }
    }
}
