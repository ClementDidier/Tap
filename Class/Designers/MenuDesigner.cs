using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tap
{
    public sealed class MenuDesigner : Designer
    {
        private GameDynamicBackground background;
        private Texture2D tapButtonTexture;
        private GameButton playButton;
        

        public MenuDesigner(GameMain game) : base(game)
        {

        }

        public override void LoadContent()
        {
            background = new GameDynamicBackground(this);

            this.tapButtonTexture = ContentHandler.Load<Texture2D>(GameResources.ButtonTexture);
            this.playButton = new GameButton(this, this.tapButtonTexture);
            this.playButton.Size = new Vector2(300, 100);
            this.playButton.Text = "Jouer";
            this.playButton.TextColor = Color.White;
            this.playButton.Position = new Vector2(game.Window.ClientBounds.Width / 2 - this.playButton.Size.X / 2, game.Window.ClientBounds.Height / 2 - this.playButton.Size.Y / 2);
            
            this.playButton.OnClick += playButton_OnClick;
        }

        private void playButton_OnClick(object sender)
        {
            NavigationHelper.NavigateTo(GameState.Play);
        }

        public override void UnloadContent()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            this.background.Update(gameTime);
            this.playButton.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            game.GraphicsDevice.Clear(background.BackgroundColor);
            game.spriteBatch.Begin();

            this.background.Draw(gameTime);
            this.playButton.Draw(gameTime);
            

            game.spriteBatch.End();
        }
    }
}
