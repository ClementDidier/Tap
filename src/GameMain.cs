using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace Tap
{
    /// <summary>
    /// This is the main type for your designer
    /// </summary>
    public class GameMain : Game
    {
        public static new ContentManager Content;

        public GameMain() : base()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content = new ContentManager(this.Services, "Content");
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.SpriteBatch = new SpriteBatch(GraphicsDevice);
            ContentHandler.Add<Texture2D>(GameResources.LogoTextureName);
            ContentHandler.Add<Texture2D>(GameResources.MenuButtonTextureName);
            ContentHandler.Add<Texture2D>(GameResources.TapButtonTextureName);
            ContentHandler.Add<Texture2D>(GameResources.CustomerScoreBackground);
            ContentHandler.Add<Texture2D>(GameResources.RedCrossButton);
            ContentHandler.Add<SpriteFont>(GameResources.FontSpriteFontName);


            // Load Designers
            MenuDesigner    = new MenuDesigner(this);
            PlayDesigner    = new PlayDesigner(this);
            EndMenuDesigner = new EndMenuDesigner(this);

            NavigationHelper.NavigateTo(GameState.Menu, TransitionType.None);

            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            Designer.UnloadContent();
            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            Designer.Update(gameTime);
            base.Update(gameTime); 
        }

        protected override void Draw(GameTime gameTime)
        {
            this.SpriteBatch.Begin();

            Designer.Draw(gameTime);
            base.Draw(gameTime);

            this.SpriteBatch.End();
        }

        public GraphicsDeviceManager Graphics { get; private set; }

        public SpriteBatch SpriteBatch { get; private set; }

        public static Designer Designer { get; set; }

        public static MenuDesigner MenuDesigner { get; private set; }

        public static PlayDesigner PlayDesigner { get; private set; }

        public static EndMenuDesigner EndMenuDesigner { get; private set; }
    }
}
