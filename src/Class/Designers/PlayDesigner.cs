using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Threading;

namespace Tap
{
    public sealed class PlayDesigner : Designer
    {
        private PlayerModel playerModel;
        private ReferentModel referentModel;
        private GameScore score;
        private GameTimer timer;
        private GameDynamicBackground background;
        private GameButton homeButton;
        private Texture2D tapButtonTexture;
        private Texture2D homeButtonTexture;
        private SpriteFont gameFont;

        public PlayDesigner(GameMain game) : base(game)
        {
            this.tapButtonTexture = ContentHandler.Load<Texture2D>(GameResources.TapButtonTextureName);
            this.homeButtonTexture = ContentHandler.Load<Texture2D>(GameResources.HomeGrayButton);
            this.gameFont = ContentHandler.Load<SpriteFont>(GameResources.FontSpriteFontName);
        }

        public override void LoadContent(object obj = null)
        {
            this.playerModel = new PlayerModel(this.Game, tapButtonTexture);
            this.playerModel.OnStateChanged += PlayerModel_OnStateChanged;
            this.playerModel.Scale = Game.Window.ClientBounds.Width / playerModel.Size.X - 0.5f;

            this.playerModel.Position = new Vector2(Game.Window.ClientBounds.Width / 2 - playerModel.Size.X / 2,
                Game.Window.ClientBounds.Height - 1.3f * playerModel.Size.Y);

            this.referentModel = new ReferentModel(this.Game, tapButtonTexture, 0.4f);
            this.referentModel.Position = new Vector2(playerModel.Position.X + playerModel.Size.X - referentModel.Size.X,
                Game.Window.ClientBounds.Width / 2 - referentModel.Size.Y / 2 + 10);

            this.homeButton = new GameButton(this.Game, this.homeButtonTexture);
            this.homeButton.Scale = 1.3f;
            this.homeButton.Position = new Vector2(this.playerModel.Position.X + this.playerModel.Size.X - this.homeButton.Size.X,
                1.5f * this.homeButton.Size.Y);
            this.homeButton.OnClick += HomeButton_OnClick;

            this.score = new GameScore(this.Game, gameFont, Color.LightGreen);
            this.score.Position = new Vector2(playerModel.Position.X,
                Game.Window.ClientBounds.Width / 2 - referentModel.Size.Y);

            this.timer = new GameTimer(this.Game, gameFont, Color.WhiteSmoke);
            this.timer.Position = new Vector2(playerModel.Position.X,
                Game.Window.ClientBounds.Width / 2 - referentModel.Size.Y / 2);
            this.timer.OnStop += timer_OnStop;

            this.background = new GameDynamicBackground(this.Game, timer);
        }

        private void HomeButton_OnClick(object sender)
        {
            NavigationHelper.NavigateTo(GameState.Menu, TransitionType.None);
        }

        private void timer_OnStop(object sender)
        {
            NavigationHelper.NavigateTo(GameState.EndMenu, TransitionType.None, score);
        }

        public override void UnloadContent()
        {

        }

        public override void Update(GameTime gameTime)
        {
            playerModel.Enabled = !timer.IsEnd || !timer.IsSuspend;
            referentModel.Enabled = !timer.IsEnd || !timer.IsSuspend;

            background.Update(gameTime);
            playerModel.Update(gameTime);
            referentModel.Update(gameTime);
            homeButton.Update(gameTime);
            score.Update(gameTime);
            timer.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Clear(background.BackgroundColor);

            background.Draw(gameTime);
            playerModel.Draw(gameTime);
            referentModel.Draw(gameTime);
            homeButton.Draw(gameTime);
            score.Draw(gameTime);
            timer.Draw(gameTime);
        }

        private void PlayerModel_OnStateChanged(object sender)
        {
            if (playerModel.IsFalseThan(referentModel))
            {
                playerModel.Clear();
                background.RaiseBadResult();
            }
            else if (playerModel.Equals(referentModel))
            {
                timer.Add(referentModel.SelectedCasesCount * 0.3M);
                score.Add(referentModel.SelectedCasesCount);
                referentModel.GenerateNewStage();
                playerModel.Clear();
                playerModel.AnimateScale();

                background.RaiseGoodResult();
            }
        }
    }
}
