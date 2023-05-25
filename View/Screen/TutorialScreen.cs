using System.Drawing;
using System.Windows.Forms;
using Game.Assets.Images;
using Game.Model;

namespace Game.View.Screen
{
    public class TutorialScreen : BaseScreen
    {
        public TutorialScreen(GameModel gameModel) : base(gameModel)
        {
            BackColor = Color.Black;
            var cancelButton = new FlatButton("Cancel");
            cancelButton.Click += (sender, args) => gameModel.GameState = GameState.Menu;
            Controls.Add(cancelButton);
            BackgroundImage = new Images().Tutorial;
            BackgroundImageLayout = ImageLayout.Stretch;
        }
    }
}