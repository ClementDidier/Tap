using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace Tap
{
    /// <summary>
    /// This is the main type for your designer
    /// </summary>
    public class GameMain : Game
    {
        private const string BUTTON_TEXTURE_NAME = "TapButton";
        private const string TIMER_FONT_NAME = "Font";

        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;

        private Texture2D tapButtonTexture;
        private SpriteFont gameFont;

        public GameMain() : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            tapButtonTexture = Content.Load<Texture2D>(BUTTON_TEXTURE_NAME);
            gameFont = Content.Load<SpriteFont>(TIMER_FONT_NAME);

            /* Load Designers */
            PlayDesigner = new PlayDesigner(this, gameFont, tapButtonTexture);
            
            /******************/

            NavigatorHelper.SetGameState(GameState.Play);

            Designer.LoadContent();
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
            Designer.Draw(gameTime);
            base.Draw(gameTime);
        }

        public static Designer Designer { get; set; }

        public static MenuDesigner MenuDesigner { get; private set; }
        public static PlayDesigner PlayDesigner { get; private set; }
        public static EndMenuDesigner EndMenuDesigner { get; private set; }
    }
}
