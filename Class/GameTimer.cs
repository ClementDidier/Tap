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
    class GameTimer : GameLabel
    {
        private const decimal START_TIMER_VALUE = 20M;
        private const decimal END_TIMER_VALUE = 0;
        private const decimal ELAPSED_TIME_VALUE = 0.1M;

        private GameFrameCounter frameCounter;

        public GameTimer(GameMain game, SpriteFont font, Color color) : base(game, font, color)
        {
            this.Time = START_TIMER_VALUE;
            this.IsEnd = false;
            this.frameCounter = new GameFrameCounter();
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
                if (this.Time > END_TIMER_VALUE) 
                { 
                    this.Time -= ELAPSED_TIME_VALUE;
                    this.caption = this.Time.ToString().Replace(',', '.');
                }
                else this.IsEnd = true;
            }
        }

        public void Add(decimal additionnalTime)
        {
            this.Time += additionnalTime;
        }

        public decimal Time { get; private set; }
        public Boolean IsEnd { get; private set; }
    }
}
