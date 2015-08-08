using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using System.Threading;

namespace Tap
{
    public sealed class EndMenuDesigner : Designer
    {
        private GameLabel scoreLabel;
        private GameButton replayButton;
        private GameButton backButton;
        private GameButton leaveButton;
        private Timer timer;

        public EndMenuDesigner(GameMain game) : base(game)
        {
            this.timer = new Timer();

            this.scoreLabel = new GameLabel(this, ContentHandler.Load<SpriteFont>(GameResources.Font), Color.LightGreen);
            this.scoreLabel.BorderThickness = 1;
            this.scoreLabel.BorderColor = Color.White;

            this.replayButton = new GameButton(this, ContentHandler.Load<Texture2D>(GameResources.ButtonTexture));
            this.replayButton.Size = new Vector2(300, 100);
            this.replayButton.Text = "Rejouer";
            this.replayButton.TextColor = Color.White;
            this.replayButton.BackgroundColor = Color.LawnGreen;
            this.replayButton.BorderColor = Color.White;
            this.replayButton.Position = new Vector2(game.Window.ClientBounds.Width / 2 - this.replayButton.Size.X / 2, game.Window.ClientBounds.Height / 2 - this.replayButton.Size.Y / 2);

            this.backButton = new GameButton(this, ContentHandler.Load<Texture2D>(GameResources.ButtonTexture));
            this.backButton.Size = new Vector2(300, 100);
            this.backButton.Text = "Menu";
            this.backButton.TextColor = Color.White;
            this.backButton.BackgroundColor = Color.LightGreen;
            this.backButton.BorderColor = Color.White;
            this.backButton.Position = new Vector2(this.replayButton.Position.X, this.replayButton.Position.Y + this.backButton.Size.Y + 20);

            this.leaveButton = new GameButton(this, ContentHandler.Load<Texture2D>(GameResources.ButtonTexture));
            this.leaveButton.Size = new Vector2(300, 100);
            this.leaveButton.Text = "Quitter";
            this.leaveButton.TextColor = Color.White;
            this.leaveButton.BackgroundColor = Color.Gray;
            this.leaveButton.BorderColor = Color.White;
            this.leaveButton.Position = new Vector2(this.backButton.Position.X, this.backButton.Position.Y + this.leaveButton.Size.Y + 20);

            this.replayButton.OnClick += replayButton_OnClick;
            this.backButton.OnClick += BackButton_OnClick;
            this.leaveButton.OnClick += LeaveButton_OnClick;
        }

        private void replayButton_OnClick(object sender)
        {
            NavigationHelper.NavigateTo(GameState.Play);
        }

        private void BackButton_OnClick(object sender)
        {
            NavigationHelper.NavigateTo(GameState.Menu);
        }

        private void LeaveButton_OnClick(object sender)
        {
            game.Exit();
        }

        public override void LoadContent(object obj = null)
        {
            GameScore score = obj as GameScore;
            this.scoreLabel.Caption = string.Format("Votre score : {0}", (score != null) ? score.Caption : "0 pts");
            this.scoreLabel.Position = new Vector2(game.Window.ClientBounds.Width / 2 - this.scoreLabel.Size.X / 2, game.Window.ClientBounds.Height / 3 - 20);
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
            game.GraphicsDevice.Clear(Color.Black);
            game.spriteBatch.Begin();

            this.scoreLabel.Draw(gameTime);
            this.replayButton.Draw(gameTime);
            this.backButton.Draw(gameTime);
            this.leaveButton.Draw(gameTime);

            game.spriteBatch.End();
        }
    }
}
