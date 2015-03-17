using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System.Diagnostics;

namespace Tap
{
    public delegate void OnClickHandler(object sender);
    public delegate void OnStateChangedHandler(object sender);

    public class GameCase : DrawableGameComponent
    {
        private SpriteBatch batch;
        private Boolean wasClicked;
        private float scale;
        
        public event OnClickHandler OnClick;
        public event OnStateChangedHandler OnStateChanged;

        public override void Initialize()
        {
            base.Initialize();
        }

        public GameCase(GameMain game, Texture2D texture) : base(game)
        {
            this.batch = game.spriteBatch;
            this.Position = Vector2.Zero;
            this.Texture = texture;
            this.wasClicked = false;
            this.State = CaseState.Unselected;
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
                    this.ClickedHandler();
                    this.wasClicked = true;
                }
            }
            else if(this.wasClicked) this.wasClicked = false;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            this.batch.Draw(this.Texture,
                new Rectangle((int)this.Position.X, (int)this.Position.Y, (int)(this.Texture.Width * Scale), (int)(this.Texture.Height * Scale)),
                this.Texture.Bounds, (this.State.Equals(CaseState.Selected) ? Color.Gray : Color.White), 0f, Vector2.Zero, SpriteEffects.None, 1f);
            base.Draw(gameTime);
        }

        public void SwitchState()
        {
            this.State = this.State.Equals(CaseState.Selected) ? CaseState.Unselected : CaseState.Selected;
            this.StateChangedHandler();
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

        public CaseState State
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

        protected virtual void ClickedHandler()
        {
            if (OnClick != null)
                OnClick(this);
        }

        protected virtual void StateChangedHandler()
        {
            if (OnStateChanged != null)
                OnStateChanged(this);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            GameCase gc = (GameCase)obj;
            return gc.State.Equals(this.State);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
