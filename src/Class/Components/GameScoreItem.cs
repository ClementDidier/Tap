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
        public const float MARGIN = 10;
        public const float HEIGHT_DEFAULT = 73.5f + MARGIN * 2;
        

        private static Color NAME_LABEL_COLOR_DEFAULT = Color.Orange;
        private static Color SCORE_LABEL_COLOR_DEFAULT = Color.Gray;

        private float scale;
        private Vector2 iconPosition;
        private CustomerScore scoreData;
        private Texture2D icon;

        public GameScoreItem(GameMain game, Texture2D texture, SpriteFont font, CustomerScore score) : base(game)
        {
            this.Texture = texture;
            this.scale = 1f;
            this.Size = new Vector2(this.Texture.Width, this.Texture.Height) * this.scale;
            this.iconPosition = Vector2.Zero;
            this.scoreData = score;
            this.Font = font;
            this.Label1 = new GameLabel(game, font);
            this.Label2 = new GameLabel(game, font);
            this.Color = Color.White;

            this.Initialize();
        }

        public override void Initialize()
        {
            base.Initialize();

            this.Label1.Color = NAME_LABEL_COLOR_DEFAULT;
            this.Label2.Color = SCORE_LABEL_COLOR_DEFAULT;
            this.Label1.Caption = this.scoreData.Name;
            this.Label1.Position = new Vector2(Icon != null ? iconPosition.X + Icon.Width + 20 : 20, this.Texture.Height * 1 / 4) + this.Position;
            this.Label2.Caption = this.scoreData.Points.ToString();
            this.Label2.Position = new Vector2(Icon != null ? iconPosition.X + Icon.Width + 20 : 20, this.Texture.Height * 3 / 4 + MARGIN) + this.Position;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // TODO : Modifier car redondance de modification de la position de chaque label
            this.Label1.Position = new Vector2((Icon != null ? iconPosition.X + Icon.Width + 20 : 20), this.Texture.Height * 1 / 4) + this.Position;
            this.Label2.Position = new Vector2((Icon != null ? iconPosition.X + Icon.Width + 20 : 20), this.Texture.Height * 3 / 4 + MARGIN) + this.Position;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            Rectangle rectangle = new Rectangle(this.Position.ToPoint(), this.Size.ToPoint());
            this.Batch.Draw(this.Texture, rectangle, this.Texture.Bounds, this.Color);

            this.Label1.Draw(gameTime);
            this.Label2.Draw(gameTime);

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

        public Color Color
        {
            get;
            set;
        }

        public GameLabel Label1 { get; set; }

        public GameLabel Label2 { get; set; }

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
