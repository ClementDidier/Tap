using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Tap.Class.Utilities;
using Tap.Class.Components;

namespace Tap.Class
{
    public sealed class GamePanel : DrawableGameItem
    {
        private const byte MARGIN = 20;

        public GamePanel(GameMain game) : base(game)
        {
            this.Objects = new List<DrawableGameItem>();
            this.Size = new Vector2(200, 300);
        }

        public void Add(DrawableGameItem item)
        {
            GameScoreItem scoreItem = item as GameScoreItem;
            this.Objects.Add(scoreItem);

            scoreItem.Size = new Vector2(this.Size.X, GameScoreItem.HEIGHT_DEFAULT);
            item.Position = new Vector2(this.Position.X, this.Objects.Count * (item.Size.Y + MARGIN) + this.Position.Y);
        }

        public void AddRange(List<DrawableGameItem> items)
        {
            foreach (var item in items)
            {
                this.Add(item);
            }
        }

        public void Remove(DrawableGameItem item)
        {
            this.Objects.Remove(item);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            this.Objects.ForEach(item => item.Update(gameTime));
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            this.Objects.ForEach(item => item.Draw(gameTime));
        }

        public new Vector2 Position
        {
            get
            {
                return base.Position;
            }
            set
            {
                base.Position = value;
                for(int i = 0; i < this.Objects.Count; i++)
                {
                    DrawableGameItem item = this.Objects.ElementAt(i);
                    item.Position = new Vector2(value.X, i * (item.Size.Y + MARGIN) + value.Y);
                }
            }
        }

        public List<DrawableGameItem> Objects
        {
            get;
            private set;
        }
    }
}
