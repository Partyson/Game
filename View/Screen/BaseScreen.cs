using System.Windows.Forms;
using Game.Model;

namespace Game.View.Screen
{
    public class BaseScreen : UserControl
    {
        protected readonly GameModel GameModel;

        protected BaseScreen(GameModel gameModel)
        {
            GameModel = gameModel;
            Dock = DockStyle.Fill;
            DoubleBuffered = true;
        }
    }
}