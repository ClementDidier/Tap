using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace Tap
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GameMain : Game
    {
        private const string BUTTON_TEXTURE_NAME = "TapButton";
        private const string TIMER_FONT_NAME = "Font";

        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;

        private Texture2D tapButtonTexture;
        private SpriteFont gameFont;

        private static PlayerModel playerModel;
        private static ReferentModel referentModel;
        private static GameScore score;
        private static GameTimer timer;

        public GameMain()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            tapButtonTexture = Content.Load<Texture2D>(BUTTON_TEXTURE_NAME);
            gameFont = Content.Load<SpriteFont>(TIMER_FONT_NAME);

            playerModel = new PlayerModel(this, tapButtonTexture);
            playerModel.OnStateChanged += PlayerModel_OnStateChanged;
            playerModel.Position = new Vector2(Window.ClientBounds.Width / 2 - playerModel.Size.X / 2,
                Window.ClientBounds.Height - 1.3f * playerModel.Size.Y);

            referentModel = new ReferentModel(this, tapButtonTexture, 0.4f);
            referentModel.Position = new Vector2(playerModel.Position.X + playerModel.Size.X - referentModel.Size.X,
                Window.ClientBounds.Width / 2 - referentModel.Size.Y / 2 + 10);

            score = new GameScore(this, gameFont);
            score.Position = new Vector2(playerModel.Position.X,
                Window.ClientBounds.Width / 2 - referentModel.Size.Y);

            timer = new GameTimer(this, gameFont);
            timer.Position = new Vector2(playerModel.Position.X,
                Window.ClientBounds.Width / 2 - referentModel.Size.Y / 2);
        }

        private void PlayerModel_OnStateChanged(object sender)
        {
            if(playerModel.IsFalseThan(referentModel))
            {
                playerModel.Clear();
            }
            else if(playerModel.Equals(referentModel))
            {
                timer.Add(referentModel.SelectedCasesCount * 0.2M);
                score.Add(referentModel.SelectedCasesCount);
                referentModel.GenerateNewStage();
                playerModel.Clear();
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            playerModel.Enabled = !timer.IsEnd;
            referentModel.Enabled = !timer.IsEnd;

            playerModel.Update(gameTime);
            referentModel.Update(gameTime);
            score.Update(gameTime);
            timer.Update(gameTime);
            base.Update(gameTime); 
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            playerModel.Draw(gameTime);
            referentModel.Draw(gameTime);
            score.Draw(gameTime);
            timer.Draw(gameTime);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
