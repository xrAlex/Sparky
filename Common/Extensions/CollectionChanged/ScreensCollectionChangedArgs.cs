using System;
using Common.Interfaces;

namespace Common.Extensions.CollectionChanged
{
    public class ScreensCollectionChangedArgs : EventArgs
    {
        public IScreenContext Screen { get; }
        public CollectionChangedAction Action { get; }

        public ScreensCollectionChangedArgs(IScreenContext screen, CollectionChangedAction action)
        {
            Action = action;
            Screen = screen;
        }
    }
}
