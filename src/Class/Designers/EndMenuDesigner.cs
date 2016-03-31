using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Tap.Class;
using Tap.Class.Components;
using Tap.Class.Utilities;

namespace Tap
{
    public sealed class EndMenuDesigner : Designer
    {
        private const byte MAX_SCORE_SHOWN_COUNT = 5;

        private Texture2D validButtonTexture;
        private Texture2D replayButtonTexture;
        private Texture2D homeButtonTexture;
        private Texture2D scoreItemTexture;
        private SpriteFont fontTexture;

        private GameScoreItem scoreItem;
        private GameButton replayButton;
        private GameButton homeButton;
        private GameButton validButton;
        private GamePanel scorePanel;
        private Timer timer;
        private CustomerScoreDataHelper dataHelper;
        private List<CustomerScore> scoresList;

        public EndMenuDesigner(GameMain game) : base(game)
        {
            this.Initialize();
        }

        public async void Initialize()
        {
            this.dataHelper = new CustomerScoreDataHelper(MAX_SCORE_SHOWN_COUNT);
            this.scoresList = await this.dataHelper.GetRankingAsync();
        }

        private void replayButton_OnClick(object sender)
        {
            NavigationHelper.NavigateTo(GameState.Play, TransitionType.None);
        }

        private void HomeButton_OnClick(object sender)
        {
            NavigationHelper.NavigateTo(GameState.Menu, TransitionType.None);
        }

        private async void ValidButton_OnClick(object sender)
        {
            string entry = await KeyboardInput.Show("Entrez votre pseudonyme !", "En validant votre pseudonyme, vous acceptez l'envoi de votre score dans le classement en ligne.");
            await MessageBox.Show(string.IsNullOrEmpty(entry).ToString(), "Description", new List<string> {"Valider","Annuler"});
        }

        public override void LoadContent(object obj = null)
        {
            this.validButtonTexture = ContentHandler.Load<Texture2D>(GameResources.ValidGreenButton);
            this.replayButtonTexture = ContentHandler.Load<Texture2D>(GameResources.ReplayOrangeButton);
            this.homeButtonTexture = ContentHandler.Load<Texture2D>(GameResources.HomeGrayButton);
            this.scoreItemTexture = ContentHandler.Load<Texture2D>(GameResources.CustomerScoreBackground);
            this.fontTexture = ContentHandler.Load<SpriteFont>(GameResources.FontSpriteFontName);

            GameScore score = obj as GameScore;
            CustomerScore currentScore = new CustomerScore("User", (score != null) ? (int)score.Score : -1);

            this.timer = new Timer();

            this.scoreItem = new GameScoreItem(this.Game, this.scoreItemTexture, this.fontTexture, currentScore);
            this.scoreItem.Color = Color.Orange;
            this.scoreItem.Size = new Vector2(Game.Window.ClientBounds.Width * 3 / 4, GameScoreItem.HEIGHT_DEFAULT);
            this.scoreItem.Label1.Color = Color.White;
            this.scoreItem.Label1.BorderColor = Color.Black;
            this.scoreItem.Label1.BorderThickness = 1;
            this.scoreItem.Label2.BorderColor = Color.White;
            this.scoreItem.Label2.BorderThickness = 1;
            this.scoreItem.Position = new Vector2(Game.Window.ClientBounds.Width / 2 - this.scoreItem.Size.X / 2, Game.Window.ClientBounds.Height / 6 - this.scoreItem.Size.Y / 2);


            this.scorePanel = new GamePanel(this.Game);
            this.scorePanel.Size = new Vector2(Game.Window.ClientBounds.Width * 3 / 4, 5 * GameScoreItem.HEIGHT_DEFAULT);
            this.scorePanel.Position = new Vector2(Game.Window.ClientBounds.Width / 2 - this.scorePanel.Size.X / 2, Game.Window.ClientBounds.Height * 2 / 5 - this.scorePanel.Size.Y / 2);

            this.scoresList.ForEach(item => this.scorePanel.Add(new GameScoreItem(this.Game, this.scoreItemTexture, this.fontTexture, item)));

            this.replayButton = new GameButton(this.Game, this.replayButtonTexture);
            this.replayButton.Scale = 1.5f;
            this.replayButton.Position = new Vector2(Game.Window.ClientBounds.Width / 4 - this.replayButton.Size.X / 2, Game.Window.ClientBounds.Height * 4 / 5 - this.replayButton.Size.Y / 2);

            this.homeButton = new GameButton(this.Game, this.homeButtonTexture);
            this.homeButton.Scale = 1.5f;
            this.homeButton.Position = new Vector2(Game.Window.ClientBounds.Width / 2 - this.homeButton.Size.X / 2, Game.Window.ClientBounds.Height * 4 / 5 - this.homeButton.Size.Y / 2);

            this.validButton = new GameButton(this.Game, this.validButtonTexture);
            this.validButton.Scale = 1.5f;
            this.validButton.Position = new Vector2(Game.Window.ClientBounds.Width * 3 / 4 - this.validButton.Size.X / 2, Game.Window.ClientBounds.Height * 4 / 5 - this.validButton.Size.Y / 2);

            this.replayButton.OnClick += replayButton_OnClick;
            this.homeButton.OnClick += HomeButton_OnClick;
            this.validButton.OnClick += ValidButton_OnClick;

            Thread.Sleep(1000);
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
                this.homeButton.Enabled = !this.InNavigationState;
                this.validButton.Enabled = !this.InNavigationState;
            }

            this.scoreItem.Update(gameTime);
            this.replayButton.Update(gameTime);
            this.homeButton.Update(gameTime);
            this.validButton.Update(gameTime);
            this.scorePanel.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Clear(Color.LightGray);

            this.scoreItem.Draw(gameTime);
            this.replayButton.Draw(gameTime);
            this.homeButton.Draw(gameTime);
            this.validButton.Draw(gameTime);
            this.scorePanel.Draw(gameTime);
        }
    }
}
