using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using System.Threading;

namespace Tap
{
    public sealed class EndMenuDesigner : Designer
    {
        private Texture2D menuButtonTexture;
        private SpriteFont fontTexture;

        private GameLabel scoreLabel;
        private GameButton replayButton;
        private GameButton backButton;
        private GameButton leaveButton;
        private Timer timer;

        public EndMenuDesigner(GameMain game) : base(game)
        {
        }

        private void replayButton_OnClick(object sender)
        {
            NavigationHelper.NavigateTo(GameState.Play, TransitionType.None);
        }

        private void BackButton_OnClick(object sender)
        {
            NavigationHelper.NavigateTo(GameState.Menu, TransitionType.None);
        }

        private void LeaveButton_OnClick(object sender)
        {
            Game.Exit();
        }

        public override void LoadContent(object obj = null)
        {
            this.menuButtonTexture = ContentHandler.Load<Texture2D>(GameResources.MenuButtonTextureName);
            this.fontTexture = ContentHandler.Load<SpriteFont>(GameResources.FontSpriteFontName);

            this.timer = new Timer();

            this.scoreLabel = new GameLabel(this.Game, this.fontTexture, Color.LightGreen);
            this.scoreLabel.BorderThickness = 1;
            this.scoreLabel.BorderColor = Color.White;

            this.replayButton = new GameButton(this.Game, this.menuButtonTexture);
            this.replayButton.Size = new Vector2(300, 100);
            this.replayButton.Text = Resources.AppResources.ButtonReplayText;
            this.replayButton.TextColor = Color.White;
            this.replayButton.BackgroundColor = Color.LawnGreen;
            this.replayButton.BorderColor = Color.White;
            this.replayButton.Position = new Vector2(Game.Window.ClientBounds.Width / 2 - this.replayButton.Size.X / 2, Game.Window.ClientBounds.Height / 2 - this.replayButton.Size.Y / 2);

            this.backButton = new GameButton(this.Game, this.menuButtonTexture);
            this.backButton.Size = new Vector2(300, 100);
            this.backButton.Text = Resources.AppResources.ButtonMenuText;
            this.backButton.TextColor = Color.White;
            this.backButton.BackgroundColor = Color.LightGreen;
            this.backButton.BorderColor = Color.White;
            this.backButton.Position = new Vector2(this.replayButton.Position.X, this.replayButton.Position.Y + this.backButton.Size.Y + 20);

            this.leaveButton = new GameButton(this.Game, this.menuButtonTexture);
            this.leaveButton.Size = new Vector2(300, 100);
            this.leaveButton.Text = Resources.AppResources.ButtonLeaveText;
            this.leaveButton.TextColor = Color.White;
            this.leaveButton.BackgroundColor = Color.Gray;
            this.leaveButton.BorderColor = Color.White;
            this.leaveButton.Position = new Vector2(this.backButton.Position.X, this.backButton.Position.Y + this.leaveButton.Size.Y + 20);

            this.replayButton.OnClick += replayButton_OnClick;
            this.backButton.OnClick += BackButton_OnClick;
            this.leaveButton.OnClick += LeaveButton_OnClick;

            GameScore score = obj as GameScore;
            this.scoreLabel.Caption = string.Format("{0} {1}", Resources.AppResources.LabelScorePointsText, (score != null) ? score.Caption : "0 pts");
            this.scoreLabel.Position = new Vector2(this.Game.Window.ClientBounds.Width / 2 - this.scoreLabel.Size.X / 2, this.Game.Window.ClientBounds.Height / 3 - 20);
        }

        public override void UnloadContent()
        {

        }

        public override void Update(GameTime gameTime)
        {
            if (this.InNavigationState)
            {
                this.InNavigationState = !this.timer.WaitTicks(50);
                this.replayButton.Enabled = !this.InNavigationState;
                this.backButton.Enabled = !this.InNavigationState;
                this.leaveButton.Enabled = !this.InNavigationState;
            }

            this.scoreLabel.Update(gameTime);
            this.replayButton.Update(gameTime);
            this.backButton.Update(gameTime);
            this.leaveButton.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Clear(Color.Black);

            this.scoreLabel.Draw(gameTime);
            this.replayButton.Draw(gameTime);
            this.backButton.Draw(gameTime);
            this.leaveButton.Draw(gameTime);
        }
    }
}
