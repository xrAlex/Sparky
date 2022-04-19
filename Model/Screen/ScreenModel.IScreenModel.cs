using System.Collections.Generic;
using System.Linq;
using Common.Interfaces;

namespace Model.Screen;

internal sealed partial class ScreenModel
{
    /// <inheritdoc cref="IScreenModel.GetScreen"/>
    public IScreenContext? GetScreen(int key)
        => _screenCollection.TryGetValue(key, out var value) ? value : null;

    /// <inheritdoc cref="IScreenModel.GetAllScreens"/>
    public IEnumerable<IScreenContext> GetAllScreens()
        => _screenCollection.Values.ToArray();
}