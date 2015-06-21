using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tap
{
    public sealed class EndMenuDesigner : Designer
    {
        private GameButton replayButton;

        public EndMenuDesigner(GameMain game) : base(game)
        {
            this.replayButton = new GameButton(this, ContentHandler.Load<Texture2D>(GameResources.ButtonTexture));
            this.replayButton.Size = new Vector2(300, 100);
            this.replayButton.Text = "Rejouer";
            this.replayButton.TextColor = Color.White;
            this.replayButton.BackgroundColor = Color.Green;
            this.replayButton.BorderColor = Color.White;
            this.replayButton.Position = new Vector2(game.Window.ClientBounds.Width / 2 - this.replayButton.Size.X / 2, game.Window.ClientBounds.Height / 2 - this.replayButton.Size.Y / 2);

            this.replayButton.OnClick += replayButton_OnClick;
            System.Threading.Thread.Sleep(1000);
        }

        private void replayButton_OnClick(object sender)
        {
            NavigationHelper.NavigateTo(GameState.Play);
        }

        public override void LoadContent()
        {

        }

        public override void UnloadContent()
        {

        }

        public override void Update(GameTime gameTime)
        {
            this.replayButton.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            game.GraphicsDevice.Clear(Color.Black);
            game.spriteBatch.Begin();

            this.replayButton.Draw(gameTime);

            game.spriteBatch.End();
        }
    }
}
