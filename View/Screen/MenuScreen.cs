using System;
using System.Drawing;
using System.Windows.Forms;
using Game.Model;

namespace Game.View.Screen
{
    public sealed class MenuScreen : BaseScreen
    {
        public MenuScreen(GameModel gameModel) : base(gameModel)
        {
            BackColor = Color.Black;
            var gameNameLabel = new Label();
            gameNameLabel.Text = "Cool White CircleShooter";
            gameNameLabel.Top = 48;
            gameNameLabel.ForeColor = Color.White;
            gameNameLabel.AutoSize = true;
            Controls.Add(gameNameLabel);
            
            var startButton = new FlatButton("Start game");
            startButton.Click += (sender, args) => gameModel.GameState = GameState.Game;
            Controls.Add(startButton);

            var tutorialButton = new FlatButton("Open tutorial");
            tutorialButton.Click += (sender, args) => gameModel.GameState = GameState.Tutorial;
            Controls.Add(tutorialButton);
            
            var exitButton = new FlatButton("   Exit   ");
            exitButton.Click += (sender, args) => Application.Exit();
            Controls.Add(exitButton);

            SizeChanged += (sender, args) =>
            {
                startButton.Left = (ClientSize.Width - startButton.Width) / 2;
                startButton.Top = (ClientSize.Height - startButton.Height) / 2;
                
                tutorialButton.Left = (ClientSize.Width - tutorialButton.Width) / 2;
                tutorialButton.Top = startButton.Bottom + 20;
                
                exitButton.Left = (ClientSize.Width - exitButton.Width) / 2;
                exitButton.Top = tutorialButton.Bottom + 20;
                
                gameNameLabel.Left = (ClientSize.Width - gameNameLabel.Width) / 2;
            };
        }
    }
}