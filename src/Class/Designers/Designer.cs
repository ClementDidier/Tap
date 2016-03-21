using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tap
{
    public abstract class Designer : INavigable
    {
        public GameMain Game { get; private set; }

        public bool InNavigationState { get; set; }

        public Designer(GameMain game)
        {
            this.Game = game;
        }

        public abstract void LoadContent(object obj = null);

        public abstract void UnloadContent();

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(GameTime gameTime);       
    }
}
