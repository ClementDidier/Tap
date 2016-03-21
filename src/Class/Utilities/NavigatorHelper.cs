namespace Tap
{
    enum GameState
    {
        Menu,
        Play,
        EndMenu,
        Loading
    }

    sealed class NavigationHelper
    {
        public static void NavigateTo(GameState state, TransitionType type,  object obj = null)
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

            GameMain.Designer.InNavigationState = true;
            GameMain.Designer.LoadContent(obj);
        }
    }
}
