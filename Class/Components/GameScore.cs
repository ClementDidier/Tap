using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Tap
{
    class GameScore : GameLabel
    {
        private const uint DEFAULT_SCORE_VALUE = 0;
        private const byte MAX_BORDERS_WEIGHT = 5;


        public GameScore(Designer game, SpriteFont font, Color color) : base(game, font, color)
        {
            this.Score = DEFAULT_SCORE_VALUE;
            this.caption = string.Format("{0} pts", this.Score);
            this.BorderColor = Color.Black;
            this.BorderThickness = 1;
        }

        public void Add(uint additionnalScore)
        {
            this.Score += additionnalScore;
            this.ChangedValueHandler(this);
        }

        public void Reset()
        {
            this.Score = DEFAULT_SCORE_VALUE;
            this.ChangedValueHandler(this);
        }

        protected override void ChangedValueHandler(Object sender)
        {
            this.caption = string.Format("{0} pts", this.Score);
            base.ChangedValueHandler(sender);
        }

        public uint Score { get; private set; }
    }
}
