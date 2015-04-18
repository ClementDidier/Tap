using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.ComponentModel;

namespace Tap
{
    static class GameResources
    {
        public const String ButtonTexture = "TapButton";
        public const String TimerFont = "Font";
    }

    static class ContentHandler
    {
        public static Dictionary<String, Object> Contents = new Dictionary<String, Object>();

        public static void Add<T>(String file)
        {
            Contents.Add(file, GameMain.Content.Load<T>(file));
        }

        public static T Load<T>(String file)
        {
            return (T) Contents[file];
        }
    }
}
