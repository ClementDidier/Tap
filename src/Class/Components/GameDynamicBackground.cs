using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Threading;

namespace Tap
{
    class GameDynamicBackground : DrawableGameComponent
    {
        protected const int RECTANGLES_COUNT = 20;
        protected const int MAX_SIZE = 200;
        protected const int BACKGROUND_R = 200;
        protected const int BACKGROUND_G = 200;
        protected const int BACKGROUND_B = 200;

        private static Random rand = new Random();

        private GameMain game;
        private SpriteBatch batch;
        private List<GameRectangle> rectangles;
        private Color foregroundColor;
        private Color backgroundColor;

        private bool inResult = false;
        private bool result = false;
        private Timer resultTimer;
        private GameTimer gameTimer;

        private Color animationBackgroundColor;
        private Color originBackgroundColor;
        bool firstAnimationBackgroundState = true;

        

        public GameDynamicBackground(GameMain game, GameTimer gameTimer = null) : base (game)
        {
            this.game = game;
            this.gameTimer = gameTimer;
            this.resultTimer = new Timer();
            this.foregroundColor = new Color(120, 104, 148);
            this.backgroundColor = new Color(BACKGROUND_R, BACKGROUND_G, BACKGROUND_B);
            this.originBackgroundColor = new Color(BACKGROUND_R, BACKGROUND_G, BACKGROUND_B);
            this.animationBackgroundColor = new Color(0, 200, 105);
            this.batch = game.SpriteBatch;
            this.rectangles = new List<GameRectangle>();

            // Création des rectangles
            for (int i = 0; i < RECTANGLES_COUNT; i++)
            {
                int size = rand.Next(50, MAX_SIZE);
                int x = rand.Next(1, game.Window.ClientBounds.Width);
                int y = rand.Next(1, game.Window.ClientBounds.Height);

                Thread.Sleep(10);

                GameRectangle rectangle = new GameRectangle(game, size, new Vector2(x, y));
                this.rectangles.Add(rectangle);
            }
        }

        public override void Update(GameTime gameTime)
        {
            /*if(gameTimer != null)
            {
                decimal poucentage = gameTimer.Time / gameTimer.Total;
                this.originBackgroundColor.R = (byte)(BACKGROUND_R * poucentage);
                this.originBackgroundColor.G = (byte)(BACKGROUND_G * poucentage);
                this.originBackgroundColor.B = (byte)(BACKGROUND_B * poucentage);

                this.backgroundColor = this.originBackgroundColor;
            }*/

            if (inResult)
            {
                if (firstAnimationBackgroundState)
                {
                    animationBackgroundColor = new Color(result ? 0 : 255, result ? 200 : 50, result ? 0 : 50);
                    firstAnimationBackgroundState = false;
                }

                animationBackgroundColor.R = animationBackgroundColor.R < originBackgroundColor.R ? (byte)(animationBackgroundColor.R + 5) : (byte)originBackgroundColor.R;
                animationBackgroundColor.G = animationBackgroundColor.G < originBackgroundColor.G ? (byte)(animationBackgroundColor.G + 5) : (byte)originBackgroundColor.G;
                animationBackgroundColor.B = animationBackgroundColor.B < originBackgroundColor.B ? (byte)(animationBackgroundColor.B + 5) : (byte)originBackgroundColor.B;

                this.backgroundColor = animationBackgroundColor;

                if (animationBackgroundColor.R == originBackgroundColor.R &&
                    animationBackgroundColor.G == originBackgroundColor.G &&
                    animationBackgroundColor.B >= originBackgroundColor.B)
                {
                    inResult = false;
                }
            }
            else firstAnimationBackgroundState = true;

            // Mise à jour des rectangles en fond
            for (int i = 0; i < this.rectangles.Count; i++)
            {
                GameRectangle rectangle = this.rectangles.ElementAt(i);
                rectangle.Color = this.foregroundColor;

                float pourcentage = (rectangle.Size * 1f / rectangle.OriginalSize) * 100;
                if (pourcentage > 50 )
                {
                    rectangle.Size--;
                    rectangle.IncreaseAlpha();
                }
                else if(pourcentage > 0)
                {
                    rectangle.Size--;
                    rectangle.Alpha = pourcentage / 100;
                }
                else
                {
                    Thread.Sleep(rand.Next(10, 30));
                    int x = rand.Next(1, this.game.Window.ClientBounds.Width);
                    int y = rand.Next(1, this.game.Window.ClientBounds.Height);
                    rectangle.Position = new Vector2(x, y);
                    rectangle.RandomizeSize(70, MAX_SIZE);
                    rectangle.Alpha = 0.5f;
                }
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            for (int i = 0; i < this.rectangles.Count; i++)
            {
                GameRectangle rectangle = this.rectangles.ElementAt(i);
                rectangle.Draw(gameTime);
            }
        }

        public void RaiseBadResult()
        {
            this.inResult = true;
            this.result = false;
        }

        public void RaiseGoodResult()
        {
            this.inResult = true;
            this.result = true;
        }

        public Color BackgroundColor
        {
            get { return this.backgroundColor; }
            private set { this.backgroundColor = value; }
        }
    }
}
