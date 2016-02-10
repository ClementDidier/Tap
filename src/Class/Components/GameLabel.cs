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

        private const byte MAX_BORDERS_THICKNESS = 5;
        private const string DEFAULT_LABEL_CAPTION ="";

        private SpriteBatch batch;
        private SpriteFont font;
        private byte borderThickness;

        protected string caption;

        public GameLabel(Designer designer, SpriteFont font, Color color) : base(designer.game)
        {
            this.batch = designer.game.spriteBatch;
            this.caption = DEFAULT_LABEL_CAPTION;
            this.font = font;
            this.Position = Vector2.Zero;
            this.Color = color;
            this.BorderColor = Color.White;
            this.borderThickness = 0;
        }

        public override void Update(GameTime gameTime) 
        { 

        }

        public override void Draw(GameTime gameTime)
        {
            this.DrawBorders();
            this.batch.DrawString(this.font, this.ToString(), this.Position, this.Color);
        }

        private void DrawBorders()
        {
            this.batch.DrawString(this.font, this.ToString(), new Vector2(this.Position.X + this.BorderThickness, this.Position.Y), this.BorderColor);
            this.batch.DrawString(this.font, this.ToString(), new Vector2(this.Position.X - this.BorderThickness, this.Position.Y), this.BorderColor);
            this.batch.DrawString(this.font, this.ToString(), new Vector2(this.Position.X, this.Position.Y + this.BorderThickness), this.BorderColor);
            this.batch.DrawString(this.font, this.ToString(), new Vector2(this.Position.X, this.Position.Y - this.BorderThickness), this.BorderColor);
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

        public String Caption 
        {
            get { return this.caption; }
            set 
            {
                this.caption = value; 
                ChangedValueHandler(this); 
            } 
        }

        public Vector2 Position 
        { 
            get; 
            set; 
        }

        public Color Color 
        { 
            get; 
            set; 
        }

        public Color BorderColor
        { 
            get;
            set; 
        }

        public byte BorderThickness 
        { 
            get { return this.borderThickness; }
            set { this.borderThickness = (value < MAX_BORDERS_THICKNESS) ? value : MAX_BORDERS_THICKNESS; } 
        }

        public Vector2 Size
        {
            get { return font.MeasureString(this.caption); }
        }
    }
}
