using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var pictureBox = new PictureBox();
            pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox.Location = new Point(500, 500);
            var ispressedKey = false;
            pictureBox.Image = Image.FromFile("D:/university B/Game/Game/Game/Images/person.png");
            Controls.Add(pictureBox);
            var timer = new Timer();
            timer.Interval = 500;
            var random = new Random();
            timer.Tick += (sender, args) =>
            {
                var mobPosition = new Point(random.Next(0, ClientSize.Width), random.Next(0, ClientSize.Height));
                var mobPicture = new PictureBox();
                mobPicture.SizeMode = PictureBoxSizeMode.AutoSize;
                mobPicture.Image = Image.FromFile("D:/university B/Game/Game/Game/Images/mob.png");
                mobPicture.Location = mobPosition;
                Controls.Add(mobPicture);
            };
            timer.Start();
            
            this.KeyDown += (sender, args) =>
            {
                switch (args.KeyCode.ToString())
                {
                    case "A":
                        pictureBox.Location = new Point(pictureBox.Location.X - 5, pictureBox.Location.Y);
                        break;
                    case "D":
                        pictureBox.Location = new Point(pictureBox.Location.X + 5, pictureBox.Location.Y);
                        break;
                    case "W":
                        pictureBox.Location = new Point(pictureBox.Location.X, pictureBox.Location.Y - 5);
                        break;
                    case "S":
                        pictureBox.Location = new Point(pictureBox.Location.X, pictureBox.Location.Y + 5);
                        break;
                }

                ispressedKey = true;
            };
        }
        
        public static class Mob
        {
            
        }
    }
}