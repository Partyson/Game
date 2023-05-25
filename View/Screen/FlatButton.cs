using System.Drawing;
using System.Windows.Forms;

namespace Game.View.Screen
{
    public class FlatButton : Button
    {
        public FlatButton(string text)
        {
            Text = text;
            FontHeight = 10;
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.MouseOverBackColor = Color.FromArgb(100, 255, 255, 255);
            ForeColor = Color.White;
            BackColor = Color.Transparent;
            AutoSize = true;
            Size = new Size(100, 50);
        }
    }
}