using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tap
{
    public sealed class EndMenuDesigner : Designer
    {
        public EndMenuDesigner(GameMain game) : base(game)
        {

        }

        public override void LoadContent()
        {

        }

        public override void UnloadContent()
        {

        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(GameTime gameTime)
        {
            game.GraphicsDevice.Clear(Color.Black);
            game.spriteBatch.Begin();



            game.spriteBatch.End();
        }
    }
}
