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
using WpfApp1.Properties;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int level = 120;
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            AboutBox1 aboutBox1 = new AboutBox1();
            aboutBox1.Show();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void NewImage_Click(object sender, RoutedEventArgs e)
        {
            string str = (sender as RadioButton).Name.ToString();
            if(str == "Flag")
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri("C:/Users/ШС/Pictures/10.png", UriKind.Relative));
                ImageBrush imageBrush = new ImageBrush(bitmapImage);
                Background = imageBrush;
                Foreground = Brushes.White;
            }
            else if (str == "VV")
            {
                BitmapImage bitmapImage1 = new BitmapImage(new Uri("C:/Users/ШС/Pictures/TASS_40307732-scaled.jpg", UriKind.Relative));
                ImageBrush imageBrush1 = new ImageBrush(bitmapImage1);
                Background = imageBrush1;
                Foreground = Brushes.White;
            }
            else if (str == "Green")
            {
                Background = Brushes.Green;
                Foreground = Brushes.White;
            }
            else if (str == "Blue")
            {
                Background = Brushes.Blue;
                Foreground = Brushes.White;
            }
            else if (str == "Orange")
            {
                Background = Brushes.Orange;
                Foreground = Brushes.White;
            }
            else if (str == "White")
            {
                Background = Brushes.White;
                Foreground = Brushes.Black;
            }
        }

        private void Level(object sender, RoutedEventArgs e)
        {
            string str = (sender as RadioButton).Name.ToString();

            if (str == "Easy")
            {
                level = 120;
            }
            else if (str == "Normal")
            {
                level = 60;
            }
            else if (str == "Hard")
            {
                level = 30;
            }
            
        }
        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            Game game = new Game(level);
            game.Width = this.ActualWidth;
            game.Height = this.ActualHeight;
            game.Left = this.Left;
            game.Top = this.Top;
            game.Background = this.Background;
            game.Foreground = this.Foreground;
            game.Show();
        }
    }
}
