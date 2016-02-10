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
    public delegate void OnStateChangedHandler(object sender);

    public class GameCase : GameButton
    {
        public event OnStateChangedHandler OnStateChanged;

        public override void Initialize()
        {
            base.Initialize();
        }

        public GameCase(Designer game, Texture2D texture) : base(game, texture)
        {
            this.State = CaseState.Unselected;
        }

        public override void Draw(GameTime gameTime)
        {
            Rectangle rect = new Rectangle((int)this.Position.X, (int)this.Position.Y, (int)(this.Texture.Width * Scale), (int)(this.Texture.Height * Scale));
            this.batch.Draw(this.Texture, rect, this.Texture.Bounds, (this.State.Equals(CaseState.Selected) ? Color.Gray : Color.White), 0f, Vector2.Zero, SpriteEffects.None, 1f);
            this.DrawBorder(rect);
        }

        public void SwitchState()
        {
            this.State = this.State.Equals(CaseState.Selected) ? CaseState.Unselected : CaseState.Selected;
            this.Raise_StateChangedHandler();
        }

        public CaseState State
        {
            get;
            set;
        }

        protected virtual void Raise_StateChangedHandler()
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
