using System.Collections.Generic;

namespace Tap
{
    static class GameResources
    {
        public const string MenuButtonTextureName = "MenuButton";
        public const string TapButtonTextureName = "TapButton";
        public const string LogoTextureName = "Logo";
        public const string CustomerScoreBackground = "CustomerScoreBackground";
        public const string RedCrossButton = "RedCrossButton";
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
