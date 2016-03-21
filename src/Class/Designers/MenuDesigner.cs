using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using Tap.Class.Components;

namespace Tap
{
    public sealed class MenuDesigner : Designer
    {
        private GameDynamicBackground background;
        private Texture2D tapButtonTexture;
        private Texture2D menuButtonTexture;
        private Texture2D logo;
        private SpriteFont font;
        private GameImage logoImage;
        private GameButton playButton;
        private GameButton tutorialButton;
        private GameButton leaveButton;
        private Timer timer;

#if DEBUG
        GameNotification notif;
#endif

        public MenuDesigner(GameMain game) : base(game)
        {
            
        }

        public override void LoadContent(object obj = null)
        {
            this.timer = new Timer();

            this.background = new GameDynamicBackground(this.Game);

            this.menuButtonTexture = ContentHandler.Load<Texture2D>(GameResources.MenuButtonTextureName);
            this.tapButtonTexture = ContentHandler.Load<Texture2D>(GameResources.TapButtonTextureName);
            this.logo = ContentHandler.Load<Texture2D>(GameResources.LogoTextureName);
            this.font = ContentHandler.Load<SpriteFont>(GameResources.FontSpriteFontName);

            this.logoImage = new GameImage(this.Game, this.logo);
            this.logoImage.Position = new Vector2(this.Game.Window.ClientBounds.Width / 2 - this.logoImage.Texture.Width / 2, this.Game.Window.ClientBounds.Height / 4 - this.logoImage.Texture.Height / 2);

            this.playButton = new GameButton(this.Game, this.menuButtonTexture);
            this.playButton.Size = new Vector2(300, 100);
            this.playButton.Text = Resources.AppResources.ButtonPlayText;
            this.playButton.TextColor = Color.White;
            this.playButton.BackgroundColor = new Color(90, 235, 130);
            this.playButton.Position = new Vector2(Game.Window.ClientBounds.Width / 2 - this.playButton.Size.X / 2, Game.Window.ClientBounds.Height / 2 - this.playButton.Size.Y / 2);

            this.tutorialButton = new GameButton(this.Game, this.menuButtonTexture);
            this.tutorialButton.Size = new Vector2(300, 100);
            this.tutorialButton.Text = Resources.AppResources.ButtonTutorialText;
            this.tutorialButton.TextColor = new Color(150, 150, 150);
            this.tutorialButton.BackgroundColor = Color.Gray;
            this.tutorialButton.BorderColor = Color.Black;
            this.tutorialButton.Position = new Vector2(this.playButton.Position.X, this.playButton.Position.Y + this.tutorialButton.Size.Y + 20);

            this.leaveButton = new GameButton(this.Game, this.menuButtonTexture);
            this.leaveButton.Size = new Vector2(300, 100);
            this.leaveButton.Text = Resources.AppResources.ButtonLeaveText;
            this.leaveButton.TextColor = Color.White;
            this.leaveButton.BackgroundColor = Color.LightGray;
            this.leaveButton.BorderColor = Color.Black;
            this.leaveButton.Position = new Vector2(this.tutorialButton.Position.X, this.tutorialButton.Position.Y + this.leaveButton.Size.Y + 20);

            this.playButton.OnClick += playButton_OnClick;
            this.tutorialButton.OnClick += TutorialButton_OnClick;
            this.leaveButton.OnClick += LeaveButton_OnClick;

#if DEBUG
            notif = new GameSlideNotification(this.Game, this.font);
            notif.Label.BorderThickness = 1;
            notif.Label.BorderColor = Color.Black;
            notif.Label.Color = Color.White;
#endif
        }

        private void TutorialButton_OnClick(object sender)
        {
#if DEBUG
            // Exemple GameCustomersScores communication
            /*GameCustomersScores gcs = new GameCustomersScores();
            List<GameCustomerScore> scores = await gcs.GetRankingAsync();
            if (await gcs.AddRankingAsync(new GameCustomerScore("application2", 2000)))
            {
                scores = await gcs.GetRankingAsync();
                scores.ForEach(x => Debug.WriteLine(x.ToString()));
            }*/

            notif.Show("Tutoriel indisponible", 500, 3000, 500);
#endif
        }

        private void playButton_OnClick(object sender)
        {
            NavigationHelper.NavigateTo(GameState.Play, TransitionType.None);
        }

        private void LeaveButton_OnClick(object sender)
        {
            Game.Exit();
        }

        public override void UnloadContent()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            if (this.InNavigationState)
            {
                this.InNavigationState = !this.timer.WaitTicks(10);
                this.playButton.Enabled = !this.InNavigationState;
                this.tutorialButton.Enabled = !this.InNavigationState;
                this.leaveButton.Enabled = !this.InNavigationState;
            }

            this.background.Update(gameTime);
            this.logoImage.Update(gameTime);
            this.playButton.Update(gameTime);
            this.tutorialButton.Update(gameTime);
            this.leaveButton.Update(gameTime);

#if DEBUG
            notif.Update(gameTime);
#endif
        }

        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Clear(background.BackgroundColor);

            this.background.Draw(gameTime);
            this.logoImage.Draw(gameTime);
            this.playButton.Draw(gameTime);
            this.tutorialButton.Draw(gameTime);
            this.leaveButton.Draw(gameTime);

#if DEBUG
            notif.Draw(gameTime);
#endif
        }
    }
}
