using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tap.Class.Utilities;

namespace Tap.Class.Components
{
    public class GameScoreItem : DrawableGameItem
    {
        private float scale;
        private Vector2 iconPosition;
        private CustomerScore scoreData;
        private GameLabel nameLabel;
        private GameLabel scoreLabel;
        private Texture2D icon;

        // TODO : Finir l'implémentation
        public GameScoreItem(GameMain game, Texture2D texture, SpriteFont font, CustomerScore score) : base(game)
        {
            this.Texture = texture;
            this.Size = new Vector2(this.Texture.Width, this.Texture.Height);
            this.iconPosition = Vector2.Zero;
            this.scoreData = score;
            this.nameLabel = new GameLabel(game, font);
            this.scoreLabel = new GameLabel(game, font);

            this.Initialize();
        }

        public override void Initialize()
        {
            base.Initialize();

            this.nameLabel.Caption = this.scoreData.Name;
            this.scoreLabel.Caption = $"{this.scoreData.Points} pts";
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            this.Batch.Draw(this.Texture, this.Position, Color.White);

            this.nameLabel.Draw(gameTime);
            this.scoreLabel.Draw(gameTime);

            if(this.Icon != null)
                this.Batch.Draw(this.Icon, this.Position + this.iconPosition, Color.White);
        }

        public Texture2D Texture
        {
            get;
            private set;
        }

        public Texture2D Icon
        {
            get
            {
                return icon;
            }
            private set
            {
                if (value != null)
                {
                    this.iconPosition = new Vector2(0.5f * value.Width, 0.5f * (this.Size.Y - value.Height));
                    this.icon = value;
                }
            }
        }

        public SpriteFont Font
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
    }
}
