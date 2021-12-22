using System;
using Common.Interfaces;

namespace Common.Extensions.CollectionChanged
{
    public class ScreensCollectionChangedArgs : EventArgs
    {
        public IScreenContext Screen { get; }
        public CollectionChangedAction Action { get; }

        public override string ToString()
            => $"Screen: {Screen}, Action: {Action}";

        public ScreensCollectionChangedArgs(IScreenContext screen, CollectionChangedAction action)
        {
            Action = action;
            Screen = screen;
        }
    }
}
