using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tap
{
    public delegate void OnClickHandler(object sender);

    public class GameButton : DrawableGameComponent
    {
        protected static SpriteFont DEFAULT_FONT = ContentHandler.Load<SpriteFont>(GameResources.Font);
        protected static Color DEFAULT_COLOR = Color.Black;
        protected SpriteBatch batch;
        protected bool wasClicked;
        protected float scale;

        private Texture2D pixel;
        private GameLabel textLabel;
        private Color backgroundColor;

        public event OnClickHandler OnClick;

        public override void Initialize()
        {
            base.Initialize();
        }

        public GameButton(Designer designer, Texture2D texture, SpriteFont font = null) : base(designer.game)
        {
            this.textLabel = new GameLabel(designer, font ?? DEFAULT_FONT, Color.White);
            this.backgroundColor = Color.White;
            this.batch = designer.game.spriteBatch;
            this.Position = Vector2.Zero;
            this.wasClicked = false;
            this.Texture = texture;
            this.Scale = 1f;
            this.Size = new Vector2(this.Texture.Width, this.Texture.Height) * this.Scale;
            this.TextColor = DEFAULT_COLOR;
            this.BorderColor = DEFAULT_COLOR;
            this.BorderThickness = 1;

            pixel = new Texture2D(Game.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            pixel.SetData(new[] { Color.White });
        }

        public override void Update(GameTime gameTime)
        {
            TouchCollection touchCollection = TouchPanel.GetState();

            if (touchCollection.Count > 0)
            {
                Rectangle hitbox = new Rectangle((int)this.Position.X, (int)this.Position.Y, (int)this.Size.X, (int)this.Size.Y);
                if (hitbox.Contains(new Point((int)touchCollection[0].Position.X, (int)touchCollection[0].Position.Y)) && !this.wasClicked && this.Enabled)
                {
                    this.Raise_ClickedHandler();
                    this.wasClicked = true;
                }
            }
            else if (this.wasClicked) this.wasClicked = false;

            this.textLabel.Caption = this.Text;
            this.textLabel.Color = this.TextColor;
            this.textLabel.BorderColor = Color.Black;
            this.textLabel.BorderThickness = 1;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Rectangle rectangle = new Rectangle((int)this.Position.X, (int)this.Position.Y, (int)(this.Size.X * Scale), (int)(this.Size.Y * Scale));
            this.batch.Draw(this.Texture, rectangle, this.Texture.Bounds, (this.wasClicked ? Color.Gray : this.BackgroundColor), 0f, Vector2.Zero, SpriteEffects.None, 1f);
            if(this.Text != null)
            {
                Vector2 textSize = this.textLabel.Size;
                float leftPadding = rectangle.Width / 2 - textSize.X / 2;
                float topPadding = rectangle.Height / 2 - textSize.Y / 2;
                this.textLabel.Position = new Vector2(rectangle.Left + leftPadding, rectangle.Top + topPadding);
                this.textLabel.Draw(gameTime);
            }
            
            this.DrawBorder(rectangle);
            base.Draw(gameTime);
        }
 
        protected void DrawBorder(Rectangle rectangleToDraw)
        {
            batch.Draw(pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, rectangleToDraw.Width, BorderThickness), BorderColor);
            batch.Draw(pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, BorderThickness, rectangleToDraw.Height), BorderColor);
            batch.Draw(pixel, new Rectangle((rectangleToDraw.X + rectangleToDraw.Width - BorderThickness), rectangleToDraw.Y, BorderThickness, rectangleToDraw.Height), BorderColor);
            batch.Draw(pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y + rectangleToDraw.Height - BorderThickness, rectangleToDraw.Width, BorderThickness), BorderColor);
        }

        public Vector2 Position
        {
            get;
            set;
        }

        public Texture2D Texture
        {
            get;
            set;
        }

        public Vector2 Size
        {
            get;
            set;
        }

        public String Text
        {
            get;
            set;
        }

        public Color TextColor
        {
            get;
            set;
        }

        public Color BorderColor
        {
            get;
            set;
        }

        public int BorderThickness
        {
            get;
            set;
        }

        public Color BackgroundColor
        {
            get { return this.backgroundColor; }
            set { this.backgroundColor = value; }
        }

        public float Scale
        {
            get { return this.scale; }
            set
            {
                this.scale = value;
                this.Size = new Vector2(this.Texture.Width, this.Texture.Height) * this.scale;
            }
        }

        protected virtual void Raise_ClickedHandler()
        {
            if (OnClick != null)
                OnClick(this);
        }
    }
}
