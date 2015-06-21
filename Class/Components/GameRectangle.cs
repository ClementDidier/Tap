using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tap
{
    class GameRectangle : DrawableGameComponent
    {
        private SpriteBatch batch;
        private Rectangle rectangle;
        private int size;
        private Color color;

        public GameRectangle(Designer designer, int size, Vector2 position) : base(designer.game)
        {
            this.batch = designer.game.spriteBatch;
            this.color = Color.Gray;
            this.size = size;
            this.rectangle = new Rectangle((int)position.X, (int)position.Y, size, size);
            this.Alpha = 0.5f;
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(GameTime gameTime)
        {
            this.DrawRectangle(batch, this.rectangle, this.color, 1);
        }

        public int OriginalSize
        {
            get { return this.size; }
            private set { this.size = value; }
        }

        public int Size
        {
            get { return rectangle.Width; }
            set { rectangle.Width = value; rectangle.Height = value; }
        }

        public Vector2 Position
        {
            get
            {
                return new Vector2(rectangle.X, rectangle.Y);
            }
            set 
            {
                rectangle.X = (int)value.X;
                rectangle.Y = (int)value.Y;
            }
        }

        public Rectangle Rectangle
        {
            get { return this.rectangle; }
            set { this.rectangle = value; }
        }

        public Color Color
        {
            get { return this.color; }
            set { this.color = value; }
        }
        public float Alpha
        {
            get;
            set;
        }

        public void IncreaseAlpha()
        {
            if (this.color.A < 255)
            {
                this.Alpha++;
            }
        }

        public void RandomizeSize(int min, int max)
        {
            Random rand = new Random();
            int size = rand.Next(min, max);
            this.size = size;
            this.Size = size;
        }

        public override string ToString()
        {
            return string.Format("[X : {0}, Y : {1}, Size : {2}]", this.Rectangle.X, this.Rectangle.Y, this.Size);
        }

        private static Texture2D pointTexture;
        private void DrawRectangle(SpriteBatch spriteBatch, Rectangle rectangle, Color color, int lineWidth)
        {
            if (pointTexture == null)
            {
                pointTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
                pointTexture.SetData<Color>(new Color[] { Color.White });
            }

            spriteBatch.Draw(pointTexture, new Rectangle(rectangle.X, rectangle.Y, lineWidth, rectangle.Height + lineWidth), color * this.Alpha);
            spriteBatch.Draw(pointTexture, new Rectangle(rectangle.X, rectangle.Y, rectangle.Width + lineWidth, lineWidth), color * this.Alpha);
            spriteBatch.Draw(pointTexture, new Rectangle(rectangle.X + rectangle.Width, rectangle.Y, lineWidth, rectangle.Height + lineWidth), color * this.Alpha);
            spriteBatch.Draw(pointTexture, new Rectangle(rectangle.X, rectangle.Y + rectangle.Height, rectangle.Width + lineWidth, lineWidth), color * this.Alpha);
        }     
    }
}
