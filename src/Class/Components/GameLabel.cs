using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tap
{
    public class GameLabel : DrawableGameComponent
    {
        public delegate void OnValueChangedHandler(object sender);
        public event OnValueChangedHandler OnValueChanged;

        private const byte MAX_BORDERS_THICKNESS = 5;
        private const string DEFAULT_LABEL_CAPTION ="";

        private static Color DEFAULT_FONT_COLOR = Color.White;
        private static Color DEFAULT_BORDER_COLOR = Color.White;

        private SpriteBatch batch;
        private SpriteFont font;
        private byte borderThickness;

        protected string caption;

        public GameLabel(GameMain game, SpriteFont font) : base(game)
        {
            this.batch = game.SpriteBatch;
            this.font = font;
            this.caption = DEFAULT_LABEL_CAPTION;
            this.Size = font.MeasureString(this.caption);
            this.Position = Vector2.Zero;
            this.BorderColor = DEFAULT_BORDER_COLOR;
            this.borderThickness = 0;
            this.Color = DEFAULT_FONT_COLOR;
            this.Alpha = 1f;
        }

        public GameLabel(GameMain game, SpriteFont font, Color fontColor) : this(game, font)
        {
            this.Color = fontColor;
        }

        public override void Draw(GameTime gameTime)
        {
            this.DrawBorders();
            this.batch.DrawString(this.font, this.ToString(), this.Position, this.Color * this.Alpha);
        }

        private void DrawBorders()
        {
            this.batch.DrawString(this.font, this.ToString(), new Vector2(this.Position.X + this.BorderThickness, this.Position.Y), this.BorderColor * this.Alpha);
            this.batch.DrawString(this.font, this.ToString(), new Vector2(this.Position.X - this.BorderThickness, this.Position.Y), this.BorderColor * this.Alpha);
            this.batch.DrawString(this.font, this.ToString(), new Vector2(this.Position.X, this.Position.Y + this.BorderThickness), this.BorderColor * this.Alpha);
            this.batch.DrawString(this.font, this.ToString(), new Vector2(this.Position.X, this.Position.Y - this.BorderThickness), this.BorderColor * this.Alpha);
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

        public string Caption 
        {
            get { return this.caption; }
            set 
            {
                if(this.caption != value && value != null)
                    this.Size = font.MeasureString(value);
                this.caption = value;
                
                ChangedValueHandler(this); 
            } 
        }

        public Vector2 Position { get; set; }

        public Color Color { get; set; }

        public Color BorderColor { get; set;  }

        public byte BorderThickness 
        { 
            get { return this.borderThickness; }
            set { this.borderThickness = (value < MAX_BORDERS_THICKNESS) ? value : MAX_BORDERS_THICKNESS; } 
        }

        public Vector2 Size
        {
            get;
            private set;
        }

        public float Alpha
        {
            get;
            set;
        }
    }
}
