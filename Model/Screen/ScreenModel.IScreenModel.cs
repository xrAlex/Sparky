using System.Collections.Generic;
using Common.DTO;
using Common.Interfaces;
using Model.Entities;

namespace Model.Screen
{
    internal sealed partial class ScreenModel
    {
        /// <inheritdoc cref="IScreenModel.DeleteScreen"/>
        public bool DeleteScreen(int key)
            => _screenCollection.Remove(key);

        /// <inheritdoc cref="IScreenModel.AddScreen"/>
        public bool AddScreen(ScreenContextDTO screenDTO)
            => _screenCollection.Add(screenDTO);

        /// <inheritdoc cref="IScreenModel.GetScreen"/>
        public IScreenContext? GetScreen(int key)
            => _screenCollection.TryGetValue(key, out var value) ? value : null;

        /// <inheritdoc cref="IScreenModel.GetAllScreens"/>
        public IEnumerable<IScreenContext> GetAllScreens()
            => _screenCollection.Values;
    }
}
