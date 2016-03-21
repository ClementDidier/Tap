using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tap.Class.Components
{
    public class GameSlideNotification : GameNotification
    {
        public GameSlideNotification(GameMain game, SpriteFont font) : base(game, font)
        {
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (this.State == GameNotificationState.Begin)
            {
                float percent = this.ElapsedTime / this.BeginAnimationTime;
                this.Label.Alpha = percent;
                this.Position = new Vector2(this.Position.X, this.Size.Y * percent - this.Size.Y);
                this.Rectangle = new Rectangle((int)this.Position.X, (int)this.Position.Y, (int)this.Size.X, (int)this.Size.Y);
            }
            else if (this.State == GameNotificationState.End)
            {
                float percent = (this.ElapsedTime - (this.Miliseconds - this.EndAnimationTime)) / this.EndAnimationTime;
                this.Label.Alpha = 1f - percent;
                this.Position = new Vector2(this.Position.X, this.Size.Y * (-1 + (1 - percent)));
                this.Rectangle = new Rectangle((int)this.Position.X, (int)this.Position.Y, (int)this.Size.X, (int)this.Size.Y);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            if (this.Visible)
            {
                this.Batch.Draw(this.Texture, this.Rectangle, this.BackgroundColor);
                this.Label.Draw(gameTime);
            }
        }
    }
}
