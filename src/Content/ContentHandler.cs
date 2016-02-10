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
        public const string MenuButtonTextureName = "MenuButton";
        public const string TapButtonTextureName = "TapButton";
        public const string LogoTextureName = "Logo";
        public const string FontSpriteFontName = "Font";
    }

    static class ContentHandler
    {
        public static Dictionary<string, object> Contents = new Dictionary<string, object>();

        public static void Add<T>(string file)
        {
            Contents.Add(file, GameMain.Content.Load<T>(file));
        }

        public static T Load<T>(string file)
        {
            return (T) Contents[file];
        }
    }
}
