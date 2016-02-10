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
        private float originalScale;

        public PlayerModel(Designer designer, Texture2D caseTexture) : base(designer, caseTexture)
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
                    this.cases[xCase, yCase] = new GameCase(designer, caseTexture);
                    this.cases[xCase, yCase].OnClick += CaseClickedHandler;
                    this.cases[xCase, yCase].OnStateChanged += StateChangedHandler;
                }
            }

            this.RefreshCases();

            this.Size = new Vector2(CASES_ARRAY_WIDTH * this.cases[0, 0].Size.X + (CASES_ARRAY_WIDTH - 1) * this.Scale * CASES_MARGIN,
                    CASES_ARRAY_HEIGHT * this.cases[0, 0].Size.Y + (CASES_ARRAY_HEIGHT - 1) * this.Scale * CASES_MARGIN);

            this.originalScale = this.Scale;
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

        public override void Update(GameTime gameTime)
        {
            if (this.InScaleAnimation)
            {
                this.Scale -= 0.015f;

                if (this.Scale < this.originalScale)
                {
                    this.Scale = this.originalScale;
                    this.InScaleAnimation = false;
                }

                this.Position = new Vector2(Game.Window.ClientBounds.Width / 2 - this.Size.X / 2, Game.Window.ClientBounds.Height - 1.3f * this.Size.Y);
            }
            else
            {
                base.Update(gameTime);
            }
        }

        public void AnimateScale()
        {
            if (!this.InScaleAnimation)
            {
                this.originalScale = this.Scale;
                this.Scale = 2f;
                this.InScaleAnimation = true;
            }
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

        public bool InScaleAnimation
        {
            get;
            private set;
        }
    }
}
