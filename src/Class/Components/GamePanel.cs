using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Tap.Class.Utilities;

namespace Tap.Class
{
    public sealed class GamePanel : DrawableGameItem
    {
        // TODO : Finir l'implémentation
        public GamePanel(GameMain game) : base(game)
        {
            this.Objects = new List<DrawableGameItem>();
        }

        public void Add(DrawableGameItem item)
        {
            this.Objects.Add(item);
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

        public List<DrawableGameItem> Objects
        {
            get;
            private set;
        }
    }
}
