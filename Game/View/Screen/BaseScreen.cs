using System.Windows.Forms;
using Game.Model;

namespace Game.View.Screen
{
    public class BaseScreen : UserControl
    {
        protected GameModel _gameModel;

        public BaseScreen(GameModel gameModel)
        {
            _gameModel = gameModel;
            Dock = DockStyle.Fill;
            DoubleBuffered = true;
        }
    }
}