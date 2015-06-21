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
    public sealed class PlayDesigner : Designer
    {
        private PlayerModel playerModel;
        private ReferentModel referentModel;
        private GameScore score;
        private GameTimer timer;
        private GameDynamicBackground background;

        private Texture2D tapButtonTexture;
        private SpriteFont gameFont;

        public PlayDesigner(GameMain game) : base(game)
        {
            this.tapButtonTexture = ContentHandler.Load<Texture2D>(GameResources.ButtonTexture);
            this.gameFont = ContentHandler.Load<SpriteFont>(GameResources.Font);
        }

        public override void LoadContent()
        {
            playerModel = new PlayerModel(this, tapButtonTexture);
            playerModel.OnStateChanged += PlayerModel_OnStateChanged;
            playerModel.Scale = game.Window.ClientBounds.Width / playerModel.Size.X - 0.5f;
            Debug.WriteLine(game.Window.ClientBounds.Width);
            playerModel.Position = new Vector2(game.Window.ClientBounds.Width / 2 - playerModel.Size.X / 2,
                game.Window.ClientBounds.Height - 1.3f * playerModel.Size.Y);

            referentModel = new ReferentModel(this, tapButtonTexture, 0.4f);
            referentModel.Position = new Vector2(playerModel.Position.X + playerModel.Size.X - referentModel.Size.X,
                game.Window.ClientBounds.Width / 2 - referentModel.Size.Y / 2 + 10);

            score = new GameScore(this, gameFont, Color.LightGreen);
            score.Position = new Vector2(playerModel.Position.X,
                game.Window.ClientBounds.Width / 2 - referentModel.Size.Y);

            timer = new GameTimer(this, gameFont, Color.WhiteSmoke);
            timer.Position = new Vector2(playerModel.Position.X,
                game.Window.ClientBounds.Width / 2 - referentModel.Size.Y / 2);
            timer.OnStop += timer_OnStop;

            background = new GameDynamicBackground(this, timer);
        }

        private void timer_OnStop(object sender)
        {
            System.Threading.Thread.Sleep(1000);
            NavigationHelper.NavigateTo(GameState.EndMenu);
        }

        public override void UnloadContent()
        {

        }

        public override void Update(GameTime gameTime)
        {
            playerModel.Enabled = !timer.IsEnd;
            referentModel.Enabled = !timer.IsEnd;

            background.Update(gameTime);
            playerModel.Update(gameTime);
            referentModel.Update(gameTime);
            score.Update(gameTime);
            timer.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            game.GraphicsDevice.Clear(background.BackgroundColor);
            game.spriteBatch.Begin();

            background.Draw(gameTime);
            playerModel.Draw(gameTime);
            referentModel.Draw(gameTime);
            score.Draw(gameTime);
            timer.Draw(gameTime);

            game.spriteBatch.End();
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
                timer.Add(referentModel.SelectedCasesCount * 0.2M);
                score.Add(referentModel.SelectedCasesCount);
                referentModel.GenerateNewStage();
                playerModel.Clear();

                background.RaiseGoodResult();
            }
        }
    }
}
