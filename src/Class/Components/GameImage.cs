using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tap.Class.Components
{
    public class GameImage : DrawableGameComponent
    {
        private SpriteBatch batch;

        public GameImage(Designer designer, Texture2D texture) : base(designer.game)
        {
            this.Texture = texture;
            this.Position = Vector2.Zero;
            this.batch = designer.game.spriteBatch;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            this.batch.Draw(this.Texture, this.Position, Color.White);
            base.Draw(gameTime);
        }

        public Texture2D Texture
        {
            get;
            set;
        }

        public Vector2 Position
        {
            get;
            set;
        }
    }
}
