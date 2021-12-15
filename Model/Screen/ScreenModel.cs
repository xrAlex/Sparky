using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DTO;
using Common.Extensions.CollectionChanged;
using Common.Interfaces;
using Model.Screen.ScreenCollection;

namespace Model.Screen
{
    public sealed partial class ScreenModel
    {
        private readonly ScreenCollection.ScreenCollection _screenCollection = new();

        public event EventHandler<ScreensCollectionChangedArgs>? ScreensCollectionChanged;

        public ScreenModel()
        {
            _screenCollection.CollectionChanged += CollectionChanged;
        }

        private void CollectionChanged(object? sender, ScreensCollectionChangedArgs e) 
            => ScreensCollectionChanged?.Invoke(this, e);

        public bool DeleteScreen(int key) 
            => _screenCollection.Remove(key);

        public bool AddScreen(ScreenContextDTO screenDTO) 
            => _screenCollection.Add(screenDTO);

        public IScreenContext? GetScreen(int key) 
            => _screenCollection.TryGetValue(key, out var value)? value : null;

        public IEnumerable<IScreenContext> GetAllScreens() 
            => _screenCollection.Values;
    }
}
