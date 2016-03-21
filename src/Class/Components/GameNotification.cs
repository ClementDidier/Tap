using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tap.Class.Components
{
    public abstract class GameNotification : DrawableGameComponent
    {
        protected const byte HEIGTH_DEFAULT = 100;
        protected const byte ANIMATION_PARTS_COUNT = 3; 
        protected const float WIDTH_FACTOR = 1;
        protected const uint MILISECONDS_DEFAULT = 10000;

        protected static Color BACKGROUND_COLOR_DEFAULT = Color.WhiteSmoke;

        public GameNotification(GameMain game, SpriteFont font) : base(game)
        {
            this.Batch = game.SpriteBatch;
            this.Texture = new Texture2D(GraphicsDevice, 1, 1);
            this.Texture.SetData(new Color[] { Color.White });
            this.Font = font;
            this.Label = new GameLabel(game, this.Font);
            this.Visible = false;
            this.Miliseconds = MILISECONDS_DEFAULT;
            this.BeginAnimationTime = MILISECONDS_DEFAULT / ANIMATION_PARTS_COUNT;
            this.EndAnimationTime = MILISECONDS_DEFAULT / ANIMATION_PARTS_COUNT;
            this.State = GameNotificationState.Nothing;
        }

        public void Show(string message)
        {
            if (this.State == GameNotificationState.Nothing)
            {
                this.Message = message;
                this.FontSize = this.Font.MeasureString(this.Message);
                this.Size = new Vector2(this.Game.Window.ClientBounds.Width * WIDTH_FACTOR, HEIGTH_DEFAULT);
                this.Position = new Vector2(this.Game.Window.ClientBounds.Width / 2 - this.Size.X / 2, -this.Size.Y);
                this.Rectangle = new Rectangle((int)this.Position.X, (int)this.Position.Y, (int)this.Size.X, (int)this.Size.Y);

                this.Visible = true;
                this.ElapsedTime = 0;
            }
        }

        public void Show(string message, uint miliseconds)
        {
            if (this.State == GameNotificationState.Nothing)
            {
                this.Miliseconds = miliseconds;
                this.BeginAnimationTime = this.Miliseconds / ANIMATION_PARTS_COUNT;
                this.EndAnimationTime = this.Miliseconds / ANIMATION_PARTS_COUNT;

                this.Show(message);
            }
        }

        public void Show(string message, uint beginAnimationTime, uint fixedAnimationTime, uint endAnimationTime)
        {
            if (this.State == GameNotificationState.Nothing)
            {
                this.Miliseconds = beginAnimationTime + endAnimationTime + fixedAnimationTime;
                this.BeginAnimationTime = beginAnimationTime;
                this.EndAnimationTime = endAnimationTime;

                this.Show(message);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (this.ElapsedTime > this.Miliseconds)
            {
                this.Visible = false;
                this.State = GameNotificationState.Nothing;
            }
            else if (this.Visible)
            {
                Vector2 fontRelativeLocation = new Vector2(this.Size.X / 2 - this.FontSize.X / 2, this.Size.Y / 2 - this.FontSize.Y / 2);
                this.Label.Position = this.Position + fontRelativeLocation;

                this.ElapsedTime += gameTime.ElapsedGameTime.Milliseconds;

                if (this.ElapsedTime <= this.BeginAnimationTime)
                {
                    this.State = GameNotificationState.Begin;
                }
                else if(this.ElapsedTime <= this.Miliseconds - this.EndAnimationTime)
                {
                    this.State = GameNotificationState.Shown;
                }
                else
                    this.State = GameNotificationState.End;
            }
        }

        public abstract override void Draw(GameTime gameTime);

        protected SpriteBatch Batch { get; set; }

        protected uint BeginAnimationTime { get; set; }
        protected uint EndAnimationTime { get; set; }

        protected Rectangle Rectangle { get; set; }

        protected Vector2 FontSize { get; set; }

        public GameLabel Label { get; protected set; }

        public Texture2D Texture { get; protected set; }

        public SpriteFont Font {  get; protected set; }

        public Color BackgroundColor { get; set; }

        public Vector2 Position { get; protected set; }

        public string Message
        {
            get
            {
                return this.Label.Caption ?? string.Empty;
            }
            protected set
            {
                this.Label.Caption = value;
            }
        }

        public uint Miliseconds { get; protected set; }

        public float ElapsedTime { get; protected set; }

        public Vector2 Size { get; protected set; }

        public GameNotificationState State { get; private set; }
    }
}
