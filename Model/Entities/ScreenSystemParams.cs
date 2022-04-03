using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Entities;
using Common.Interfaces;

namespace Model.Entities
{
    internal struct ScreenSystemParams
    {
        public ScreenBounds Bounds { get; }
        public string SystemName { get; }
        public string FriendlyName { get; }
        public int DisplayCode { get; }
        public nint Handle { get; }

        public ScreenSystemParams(int displayCode, string friendlyName,
            string systemName, ScreenBounds bounds, nint handle)
        {
            DisplayCode = displayCode;
            FriendlyName = friendlyName;
            SystemName = systemName;
            Bounds = bounds;
            Handle = handle;
        }
    }
}
