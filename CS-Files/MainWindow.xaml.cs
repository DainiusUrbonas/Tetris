using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TetrisWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer Timer; // Timer for moving
        Board myBoard; // The game board
        // Constructor
        public MainWindow()
        {
            InitializeComponent();
        }

        // Creates the timer thingy and calls GameStart
        void MainWindow_Initialized(object sender, EventArgs e)
        {
            Timer = new DispatcherTimer();
            Timer.Tick += new EventHandler(GameTick);
            Timer.Interval = new TimeSpan(0, 0, 0, 0, 400);
            GameStart();
        }

        // Creates the new game board and starts the timer
        private void GameStart()
        {
            MainGrid.Children.Clear();
            myBoard = new Board(MainGrid);
            Timer.Start();
            //GamePause();
        }

        // Every tick of timer it changes the time interval acording to the level player is in
        // and prints out the score and level. Then checks if the game ended, if so closes the game window and opens
        // the GameEnd window
        void GameTick(object sender, EventArgs e)
        {
            int s, l, n;
            s = myBoard.getScore();
            l = myBoard.getLevel();
            n = 410 - (l * 30);
            Timer.Interval = new TimeSpan(0, 0, 0, 0, n);
            Score.Content = "Score=" + myBoard.getScore().ToString("0000");
            Lines.Content = "Level=" + myBoard.getLevel().ToString("0000");
            myBoard.CurrTetriminoMoveDown();
            if(myBoard.getGameEnd())
            {
                GameEnd main = new GameEnd(s, l);
                App.Current.MainWindow = main;
                this.Close();
                main.Show();
                Timer.Stop();
            }
        }

        // Pauses or resumes the game
        private void GamePause()
        {
            if (Timer.IsEnabled) Timer.Stop();
            else Timer.Start();
        }

        // Handles the keyboard button events.
        private void HandleKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    if (Timer.IsEnabled) myBoard.CurrTetriminoMoveLeft();
                    break;
                case Key.Right:
                    if (Timer.IsEnabled) myBoard.CurrTetriminoMoveRight();
                    break;
                case Key.Down:
                    if (Timer.IsEnabled) myBoard.CurrTetriminoMoveDown();
                    break;
                case Key.Up:
                    if (Timer.IsEnabled) myBoard.CurrTetriminoMoveRotate();
                    break;
                case Key.Space:
                    GameStart();
                    break;
                case Key.P:
                    GamePause();
                    break;
                default:
                    break;
            }
        }
    }
}
