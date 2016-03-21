using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tap
{
    sealed class ReferentModel : Model
    {
        public ReferentModel(GameMain game, Texture2D caseTexture, float scale) : base(game, caseTexture)
        {
            Initialize();
            this.Scale = scale;
            this.Size = new Vector2(CASES_ARRAY_WIDTH * this.cases[0, 0].Size.X + (CASES_ARRAY_WIDTH - 1) * this.Scale * CASES_MARGIN,
                    CASES_ARRAY_HEIGHT * this.cases[0, 0].Size.Y + (CASES_ARRAY_HEIGHT - 1) * this.Scale * CASES_MARGIN);

            this.GenerateNewStage();
        }

        public override void Initialize()
        {
            base.Initialize();
            
            for (int xCase = 0; xCase < this.cases.GetLength(0); xCase++)
                for (int yCase = 0; yCase < this.cases.GetLength(1); yCase++)
                    this.cases[xCase, yCase] = new GameCase(MainGame, caseTexture);
        }

        public void GenerateNewStage()
        {
            Random rand = new Random();
            this.SelectedCasesCount = 0;
            for (int xCase = 0; xCase < this.cases.GetLength(0); xCase++)
            {
                for (int yCase = 0; yCase < this.cases.GetLength(1); yCase++)
                {
                    if (rand.Next(100) > 50)
                    {
                        this.cases[xCase, yCase].State = CaseState.Selected;
                        this.SelectedCasesCount++;
                    }
                    else this.cases[xCase, yCase].State = CaseState.Unselected;
                }
            }
            if (this.SelectedCasesCount == 0)
                this.GenerateNewStage();
        }

        public byte SelectedCasesCount { get; private set; }
    }
}
