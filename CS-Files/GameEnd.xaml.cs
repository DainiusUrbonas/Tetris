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
using System.Windows.Shapes;

namespace TetrisWpf
{
    /// <summary>
    /// Interaction logic for GameEnd.xaml
    /// </summary>
    public partial class GameEnd : Window
    {
        // Constructor. Tells the player about the lost game and prints out the score
        public GameEnd(int s, int l)
        {
            InitializeComponent();
            Text_Block.Text = "   You Lose!\nYour score: " + s.ToString();
        }

        // Closes the game
        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
