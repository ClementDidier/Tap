using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tap
{
    public abstract class Model : DrawableGameComponent
    {
        protected const uint CASES_ARRAY_WIDTH = 3;
        protected const uint CASES_ARRAY_HEIGHT = 3;
        protected const uint CASES_MARGIN = 10;
        
        private SpriteBatch batch;
        private Vector2 position;
        private float scale;

        protected Designer designer;
        protected Texture2D caseTexture;
        protected GameCase[,] cases;

        public Model(Designer designer, Texture2D caseTexture) : base(designer.game)
        {
            this.position = Vector2.Zero;
            this.scale = 1f;
            this.Size = Vector2.Zero;
            this.batch = designer.game.spriteBatch;
            this.cases = new GameCase[Model.CASES_ARRAY_WIDTH, Model.CASES_ARRAY_HEIGHT];
            this.designer = designer;
            this.caseTexture = caseTexture;
            this.Enabled = true;
        }

        protected void RefreshCases()
        {
            for (int xCase = 0; xCase < this.cases.GetLength(0); xCase++)
            {
                for (int yCase = 0; yCase < this.cases.GetLength(1); yCase++)
                {
                    this.cases[xCase, yCase].Scale = this.scale;
                    this.cases[xCase, yCase].Position = new Vector2(xCase * (this.cases[xCase, yCase].Size.X + this.Scale * CASES_MARGIN),
                        yCase * (this.cases[xCase, yCase].Size.Y + this.Scale * CASES_MARGIN)) + this.Position;
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            foreach (GameCase gc in this.cases)
            {
                gc.Update(gameTime);
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (GameCase gc in this.cases) 
                gc.Draw(gameTime);
            base.Draw(gameTime);
        }

        public void Clear()
        {
            foreach (GameCase gc in this.cases)
                gc.State = CaseState.Unselected;
        }

        public Vector2 Position
        {
            get { return this.position; }
            set
            {
                this.position = value;
                RefreshCases();
            }
        }

        public float Scale
        {
            get { return this.scale; }
            set
            {
                this.scale = value;
                RefreshCases();
            }
        }

        public Vector2 Size { get; set; }

        public GameCase Case(byte x, byte y)
        {
            if (x > this.cases.GetLength(0) || y > this.cases.GetLength(1))
                throw new ArgumentOutOfRangeException("Les arguments sont en dehors du tableau");
            return this.cases[x, y];
        }

        public Boolean IsEmpty()
        {
            for (byte x = 0; x < this.cases.GetLength(0); x++)
            {
                for (byte y = 0; y < this.cases.GetLength(1); y++)
                {
                    if (this.Case(x, y).State.Equals(CaseState.Selected))
                        return false;
                }
            }
            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            Model model = (Model)obj;
            for (int xCase = 0; xCase < this.cases.GetLength(0); xCase++)
            {
                for (int yCase = 0; yCase < this.cases.GetLength(1); yCase++)
                {
                    if (!this.cases[xCase, yCase].Equals(model.cases[xCase, yCase])) return false;
                }
            }
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
