using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tap
{
    sealed class  PlayerModel : Model
    {
        public event OnStateChangedHandler OnStateChanged;

        public PlayerModel(GameMain game, Texture2D caseTexture) : base(game, caseTexture)
        {
            Initialize();
        }

        public override void Initialize()
        {
            base.Initialize();
            for (byte xCase = 0; xCase < this.cases.GetLength(0); xCase++)
            {
                for (byte yCase = 0; yCase < this.cases.GetLength(1); yCase++)
                {
                    this.cases[xCase, yCase] = new GameCase(game, caseTexture);
                    this.cases[xCase, yCase].OnClick += CaseClickedHandler;
                    this.cases[xCase, yCase].OnStateChanged += StateChangedHandler;
                }
            }

            this.RefreshCases();

            this.Size = new Vector2(CASES_ARRAY_WIDTH * this.cases[0, 0].Size.X + (CASES_ARRAY_WIDTH - 1) * this.Scale * CASES_MARGIN,
                    CASES_ARRAY_HEIGHT * this.cases[0, 0].Size.Y + (CASES_ARRAY_HEIGHT - 1) * this.Scale * CASES_MARGIN);
        }

        public Boolean IsFalseThan(Model model)
        {
            for (byte x = 0; x < this.cases.GetLength(0); x++)
            {
                for (byte y = 0; y < this.cases.GetLength(1); y++)
                {
                    if (this.Case(x, y).State.Equals(CaseState.Selected) && model.Case(x, y).State.Equals(CaseState.Unselected))
                        return true;
                }
            }
            return false;
        }

        private void CaseClickedHandler(object sender)
        {
            if (this.Enabled)
            {
                GameCase gc = (GameCase)sender;
                gc.SwitchState();
            }
        }

        private void StateChangedHandler(object sender)
        {
            if (OnStateChanged != null)
                OnStateChanged(sender);
        }
    }
}
