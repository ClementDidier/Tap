using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tap
{
    class GameScore : DrawableGameComponent
    {
        private const uint DEFAULT_SCORE_VALUE = 0;

        private SpriteBatch batch;
        private SpriteFont font;

        public GameScore(GameMain game, SpriteFont font) : base(game)
        {
            this.batch = game.spriteBatch;
            this.Score = DEFAULT_SCORE_VALUE;
            this.Position = Vector2.Zero;
            this.font = font;
        }

        public void Add(uint additionnalScore)
        {
            this.Score += additionnalScore;
        }

        public void Reset()
        {
            this.Score = DEFAULT_SCORE_VALUE;
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(GameTime gameTime)
        {
            this.batch.DrawString(this.font, String.Format("{0} pts", this.Score), this.Position, Color.DarkGreen);
        }

        public uint Score { get; private set; }

        public Vector2 Position { get; set; }
    }
}
