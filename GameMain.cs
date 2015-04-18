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
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;

        public new static ContentManager Content;

        public GameMain() : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content = new ContentManager(this.Services, "Content");
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ContentHandler.Add<Texture2D>(GameResources.ButtonTexture);
            ContentHandler.Add<SpriteFont>(GameResources.TimerFont);

            /* Load Designers */
            MenuDesigner    = new MenuDesigner(this);
            PlayDesigner    = new PlayDesigner(this);
            EndMenuDesigner = new EndMenuDesigner(this);
            /******************/

            NavigatorHelper.NavigateTo(GameState.Menu);

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
