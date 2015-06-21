using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tap
{
    enum GameState
    {
        Menu,
        Play,
        EndMenu
    }

    sealed class NavigationHelper
    {
        public static void NavigateTo(GameState state)
        {
            switch(state)
            {
                case GameState.Menu:
                    GameMain.Designer = GameMain.MenuDesigner;
                    break;
                case GameState.Play:
                    GameMain.Designer = GameMain.PlayDesigner;
                    break;
                case GameState.EndMenu:
                    GameMain.Designer = GameMain.EndMenuDesigner;
                    break;
            }
            GameMain.Designer.LoadContent();
        }
    }
}
