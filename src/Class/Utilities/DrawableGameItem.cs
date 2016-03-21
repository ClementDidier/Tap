using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tap.Class.Utilities
{
    public abstract class DrawableGameItem : DrawableGameComponent, IDrawableItem
    {
        public DrawableGameItem(GameMain game) : base(game)
        {
            this.Batch = game.SpriteBatch;
        }

        public Vector2 Position
        {
            get;
            set;
        }

        public Vector2 Size
        {
            get;
            set;
        }

        protected SpriteBatch Batch
        {
            get;
            private set;
        }
    }
}
