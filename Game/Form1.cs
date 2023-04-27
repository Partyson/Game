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
            var label = new Label();
            label.Location = new Point(0, 0);
            label.Size = new Size(ClientSize.Width, 30);
            label.Text = "Input number";
            Controls.Add(label);
            var input = new TextBox();
            input.Location = new Point(0, label.Bottom);
            input.Size = label.Size;
            Controls.Add(input);

            var button = new Button();
            button.Location = new Point(0, input.Bottom);
            button.Size = label.Size;
            button.Text = "Increment";
            button.Click += (sender, args) =>
            {
                var number = int.Parse(input.Text);
                number++;
                input.Text = number.ToString();
            };
            Controls.Add(button);
            InitializeComponent();
        }
        
        protected override void OnFormClosing(FormClosingEventArgs eventArgs)
        {
            var result = MessageBox.Show("Действительно закрыть?", "",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) 
                eventArgs.Cancel = true;
        }
    }
}