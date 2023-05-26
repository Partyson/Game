using System.Drawing;
using System.Windows.Forms;
using Game.Assets.Images;
using Game.Model;

namespace Game.View.Screen
{
    public sealed class TutorialScreen : BaseScreen
    {
        public TutorialScreen(GameModel gameModel) : base(gameModel)
        {
            var cancelButton = new FlatButton("Cancel");
            cancelButton.Click += (sender, args) => gameModel.GameState = GameState.Menu;
            cancelButton.Location = new Point(900, 800);
            Controls.Add(cancelButton);
            BackgroundImage = new Images().Tutorial;
            BackgroundImageLayout = ImageLayout.None;
            BackColor = Color.Black;
        }
    }
}