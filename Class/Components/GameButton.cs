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
        protected SpriteBatch batch;
        protected Boolean wasClicked;
        protected float scale;

        public event OnClickHandler OnClick;

        public override void Initialize()
        {
            base.Initialize();
        }

        public GameButton(Designer designer, Texture2D texture) : base(designer.game)
        {
            this.batch = designer.game.spriteBatch;
            this.Position = Vector2.Zero;
            this.wasClicked = false;
            this.Texture = texture;
            this.Scale = 1f;
            this.Size = new Vector2(this.Texture.Width, this.Texture.Height) * this.Scale;
        }

        public override void Update(GameTime gameTime)
        {
            TouchCollection touchCollection = TouchPanel.GetState();

            if (touchCollection.Count > 0)
            {
                Rectangle hitbox = new Rectangle((int)this.Position.X, (int)this.Position.Y, (int)this.Size.X, (int)this.Size.Y);
                if (hitbox.Contains(new Point((int)touchCollection[0].Position.X, (int)touchCollection[0].Position.Y)) && !this.wasClicked)
                {
                    this.Raise_ClickedHandler();
                    this.wasClicked = true;
                }
            }
            else if (this.wasClicked) this.wasClicked = false;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            this.batch.Draw(this.Texture,
                new Rectangle((int)this.Position.X, (int)this.Position.Y, (int)(this.Texture.Width * Scale), (int)(this.Texture.Height * Scale)),
                this.Texture.Bounds, (this.wasClicked ? Color.Gray : Color.White), 0f, Vector2.Zero, SpriteEffects.None, 1f);
            base.Draw(gameTime);
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
            private set;
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
