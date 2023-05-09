using System.Drawing;
using System.Windows.Forms;
using Game.Model;

namespace Game.View.Screen
{
    public class GameOverScreen : BaseScreen
    {
        public GameOverScreen(GameModel gameModel) : base(gameModel)
        {
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawString(_gameModel.Player.Position.ToString() ,SystemFonts.DialogFont, new SolidBrush(Color.Black), new PointF(10, 10));

        }
    }
}