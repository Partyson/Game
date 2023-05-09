using System.Drawing;
using System.Windows.Forms;
using Game.Model;
using Game.View.Screen;

namespace Game.View
{
    public class GameWindow : Form
    {
        private readonly GameModel _gameModel;

        public GameWindow(GameModel gameModel)
        {
            ClientSize = GameSettings.ViewportSize;
            MaximizeBox = false;
            _gameModel = gameModel;
            _gameModel.GameStateChanged += SetGameState;
            _gameModel.GameState = GameState.Game;
        }

        private void SetGameState(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Game:
                    SetScreen(new GameScreen(_gameModel));
                    break;
                case GameState.GameOver:
                    SetScreen(new GameOverScreen(_gameModel));
                    break;
            }
        }

        private void SetScreen(BaseScreen screen)
        {
            foreach (Control control in Controls)
                control.Dispose();
            Controls.Clear();
            Controls.Add(screen);
        }
    }
}