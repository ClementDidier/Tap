using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tap.Class.Utilities
{
    public interface IDrawableItem
    {
        Vector2 Position { get; set; }

        Vector2 Size { get; set; }
    }
}
