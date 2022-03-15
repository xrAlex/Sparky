﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Common.Infrastructure.INPC
{
    /// <summary>Делегат для сравнения двух экземпляров.</summary>
    /// <typeparam name="T">Тип экземпляров.</typeparam>
    /// <param name="left">Первый из сравниваемых объектов.</param>
    /// <param name="right">Второй из сравниваемых объектов.</param>
    /// <returns>Возвращает <see langword="true"/>  для одинаковых объектов.</returns>
    public delegate bool Equality<in T>(T left, T right);

    /// <summary>Базовый класс с реализацией <see cref="INotifyPropertyChanged"/>.</summary>
    public abstract partial class INPCBase : INotifyPropertyChanged
    {
        /// <inheritdoc cref="INotifyPropertyChanged"/>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>Защищённый метод для создания события <see cref="PropertyChanged"/>.</summary>
        /// <param name="propertyName">Имя изменившегося свойства. 
        /// Если значение не задано, то используется имя метода или свойства в котором был вызов.</param>
        protected void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>Защищённый метод для создания события <see cref="PropertyChanged"/>
        /// с аргументом содержащим дополнительное свойство.</summary>
        /// <param name="propertyName">Имя изменившегося свойства. 
        /// Если значение не задано, то используется имя метода или свойства в котором был вызов.</param>
        /// <param name="newValue">Новое значение изменившегося свойства.</param>
        protected void RaisePropertyChanged([CallerMemberName] string? propertyName = null, in object? newValue = default)
            => PropertyChanged?.Invoke(this, new PropertyChangedValueEventArgs(propertyName, newValue));

        /// <summary>Защищённый метод для присвоения значения полю и
        /// создания события <see cref="PropertyChanged"/>.</summary>
        /// <typeparam name="T">Тип поля и присваиваемого значения.</typeparam>
        /// <param name="propertyField">Ссылка на поле.</param>
        /// <param name="newValue">Присваиваемое значение.</param>
        /// <param name="propertyName">Имя изменившегося свойства. 
        /// Если значение не задано, то используется имя метода в котором был вызов.</param>
        /// <param name="equality">Делегат для сравнения значения поля с новым значением.</param>
        /// <returns>Возвращает <see langword="true"/>, если значение изменилось и
        /// было поднято событие <see cref="PropertyChanged"/>.</returns>
        /// <remarks>Метод предназначен для использования в сеттере свойства.<br/>
        /// Сравнение нового значения со значением поля производится следующим образом:<br/>
        /// - Если оба равны <see langword="null"/>, то считаются равными;<br/>
        /// - Если один равен <see langword="null"/>, а другой нет, то считаются неравными;<br/>
        /// - Если оба не равны <see langword="null"/>, то сравниваются ссылки на объекты;<br/>
        /// - Если ссылки на разные объекты, то сравниваются делегатом <paramref name="equality"/>;<br/>
        /// - Если делегат <paramref name="equality"/> равен <see langword="null"/>,
        /// то сравниваются методом <see cref="IEquatable{T}.Equals(T)"/>, если он есть у <typeparamref name="T"/>, или <see cref="object.Equals(object, object)"/>.<br/>
        /// Если присваиваемое значение не эквивалентно значению поля, то оно присваивается полю.<br/>
        /// После присвоения создаётся событие <see cref="PropertyChanged"/> вызовом
        /// метода <see cref="RaisePropertyChanged(string)"/>
        /// с передачей ему параметра <paramref name="propertyName"/>.<br/>
        /// После создания события вызывается метод <see cref="OnPropertyChanged(in string, in object, in object)"/>.</remarks>
        protected bool Set<T>(ref T? propertyField, T? newValue, Equality<T?>? equality = null, [CallerMemberName] string? propertyName = null)
        {
            var isFieldNull = propertyField == null;
            var isValueNull = newValue == null;
            var isEquals = isFieldNull && isValueNull;

            if (!(isFieldNull || isValueNull))
            {
                if (propertyField != null)
                    isEquals = ReferenceEquals(propertyField, newValue) 
                               || (equality?.Invoke(propertyField, newValue) 
                                   ?? (propertyField is IEquatable<T> equatable 
                                       ? equatable.Equals(newValue) 
                                       : propertyField.Equals(newValue)));
            }

            if (!isEquals)
            {
                var oldValue = propertyField;

                propertyField = newValue;
                RaisePropertyChanged(propertyName, newValue);
                OnPropertyChanged(propertyName, oldValue, newValue);
            }

            return !isEquals;
        }

        /// <summary>Защищённый виртуальный метод вызывается после присвоения значения
        /// свойству и после создания события <see cref="PropertyChanged"/>.</summary>
        /// <param name="propertyName">Имя изменившегося свойства.</param>
        /// <param name="oldValue">Старое значение свойства.</param>
        /// <param name="newValue">Новое значение свойства.</param>
        /// <remarks>Переопределяется в производных классах для реализации
        /// реакции на изменение значения свойства.<br/>
        /// Рекомендуется в переопределённом методе первым оператором вызывать базовый метод.<br/>
        /// Если в переопределённом методе не будет вызова базового, то возможно нежелательное изменение логики базового класса.</remarks>
        protected virtual void OnPropertyChanged(in string propertyName, in object oldValue, in object newValue) { }
    }
}
