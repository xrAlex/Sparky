using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Extensions.CollectionChanged;
using Common.Interfaces;

namespace Model.Screen.ScreenCollection
{
    internal sealed partial class ScreenCollection : IReadOnlyDictionary<int, IScreenContext>
    {
        private readonly Dictionary<int, IScreenContext> _screens = new();

        public event EventHandler<ScreensCollectionChangedArgs>? CollectionChanged;
    }
}
