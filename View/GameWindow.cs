using System;
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
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _gameModel.GameState = GameState.Menu;
        }

        private void SetGameState(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Game:
                    SetScreenThreadSafe(new GameScreen(_gameModel));
                    break;
                case GameState.Menu:
                    SetScreenThreadSafe(new MenuScreen(_gameModel));
                    break;
                case GameState.Tutorial:
                    SetScreenThreadSafe(new TutorialScreen(_gameModel));
                    break;
            }
        }

        private void SetScreenThreadSafe(BaseScreen screen) => BeginInvoke((Action)(() => SetScreen(screen)));

        private void SetScreen(BaseScreen screen)
        {
            foreach (Control control in Controls)
                control.Dispose();

            Controls.Clear();
            Controls.Add(screen);
            screen.Focus();
        }
    }
}