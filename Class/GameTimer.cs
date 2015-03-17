using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tap
{
    class GameTimer : DrawableGameComponent
    {
        private const decimal START_TIMER_VALUE = 20M;
        private const decimal END_TIMER_VALUE = 0;
        private const decimal ELAPSED_TIME_VALUE = 0.1M;

        private GameFrameCounter frameCounter;
        private SpriteFont font;
        private SpriteBatch batch;

        public GameTimer(GameMain game, SpriteFont font) : base(game)
        {
            this.batch = game.spriteBatch;
            this.Time = START_TIMER_VALUE;
            this.IsEnd = false;
            this.Position = Vector2.Zero;
            this.frameCounter = new GameFrameCounter();
            this.font = font;
        }

        public void Reset()
        {
            this.Time = START_TIMER_VALUE;
            this.IsEnd = false;
        }

        public override void Update(GameTime gameTime)
        {
            frameCounter.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            if(frameCounter.Wait100ms())
            {
                if (this.Time > END_TIMER_VALUE) this.Time -= ELAPSED_TIME_VALUE;
                else this.IsEnd = true;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            this.batch.DrawString(this.font, this.Time.ToString().Replace(',', '.'), this.Position, Color.Gray);
        }

        public void Add(decimal additionnalTime)
        {
            this.Time += additionnalTime;
        }

        public decimal Time { get; private set; }

        public Boolean IsEnd { get; private set; }

        public Vector2 Position { get; set; }
    }
}
