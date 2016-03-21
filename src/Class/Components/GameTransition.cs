using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tap
{
    class GameTransition : DrawableGameComponent
    {
        private GameMain game;
        private Designer target;

        public GameTransition(GameMain game, Designer target) : base(game)
        {
            this.game = game;
            this.target = target;
        }
    }
}
