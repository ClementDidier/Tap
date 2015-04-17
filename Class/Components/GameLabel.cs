using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tap
{
    class GameLabel : DrawableGameComponent
    {
        public delegate void OnValueChangedHandler(Object sender);
        public event OnValueChangedHandler OnValueChanged;

        private const byte MAX_BORDERS_WEIGHT = 5;
        private const string DEFAULT_LABEL_CAPTION ="";

        private SpriteBatch batch;
        private SpriteFont font;
        private byte bordersWeight;

        protected string caption;

        public GameLabel(Designer designer, SpriteFont font, Color color) : base(designer.game)
        {
            this.batch = designer.game.spriteBatch;
            this.caption = DEFAULT_LABEL_CAPTION;
            this.font = font;
            this.Position = Vector2.Zero;
            this.Color = color;
            this.Borders = false;
            this.BordersColor = Color.White;
            this.bordersWeight = 1;
        }

        public override void Update(GameTime gameTime) { }

        public override void Draw(GameTime gameTime)
        {
            if (this.Borders) this.DrawBorders();
            this.batch.DrawString(this.font, this.ToString(), this.Position, this.Color);
        }

        private void DrawBorders()
        {
            this.batch.DrawString(this.font, this.ToString(), new Vector2(this.Position.X + this.BordersWeight, this.Position.Y), this.BordersColor);
            this.batch.DrawString(this.font, this.ToString(), new Vector2(this.Position.X - this.BordersWeight, this.Position.Y), this.BordersColor);
            this.batch.DrawString(this.font, this.ToString(), new Vector2(this.Position.X, this.Position.Y + this.BordersWeight), this.BordersColor);
            this.batch.DrawString(this.font, this.ToString(), new Vector2(this.Position.X, this.Position.Y - this.BordersWeight), this.BordersColor);
        }

        public override string ToString()
        {
            return this.Caption;
        }

        protected virtual void ChangedValueHandler(object sender)
        {
            if (OnValueChanged != null)
                OnValueChanged(sender);
        }

        public String Caption { get { return this.caption; } set { this.caption = value; ChangedValueHandler(this); } }
        public Vector2 Position { get; set; }
        public Color Color { get; set; }
        public Boolean Borders { get; set; }
        public Color BordersColor { get; set; }
        public byte BordersWeight { get { return this.bordersWeight; } set { this.bordersWeight = (value < MAX_BORDERS_WEIGHT) ? value : MAX_BORDERS_WEIGHT; } }
    }
}
