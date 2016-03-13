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
    /// Interaction logic for GameStart.xaml
    /// </summary>
    public partial class GameStart : Window
    {
        public GameStart()
        {
            InitializeComponent();
        }

        // Closes the GameStart window and opens the main game window
        private void Button_Click_Yes(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            App.Current.MainWindow = main;
            this.Close();
            main.Show();
        }

        // Exits the game
        private void Button_Click_No(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
