using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
using WpfApp1.Properties;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int level = 120;
        int diap = 100;

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
            if (str == "Flag")
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

        private void Diapazone(object sender, RoutedEventArgs e)
        {
            string str = (sender as RadioButton).Name.ToString();

            if (str == "Ten")
            {
                diap = 10;
            }
            else if (str == "Hundred")
            {
                diap = 100;
            }
            else if (str == "Thousand")
            {
                diap = 1000;
            }
        }

        GameTimer timer { get; set; }

        private int secretNumber;
        private Random random = new Random();
        TimeSpan _time;
        DispatcherTimer _timer;

        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            count = 0;
            Start.Visibility = Visibility.Hidden;
            Gamee.Visibility = Visibility.Visible;

            tbxGuess.Focus();

            if (StartGameButton.Content == "Продолжить")
            {
                _timer.Start();
            }
            else
            {
                StartNewGame(diap);

                _time = TimeSpan.FromSeconds(level);

                _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
                {
                    Time.Text = _time.ToString("c");
                    if (_time == TimeSpan.Zero) Close();
                    _time = _time.Add(TimeSpan.FromSeconds(-1));
                }, Application.Current.Dispatcher);

                _timer.Start();
            }
        }

        private void StartNewGame(int diap)
        {
            secretNumber = random.Next(0, diap + 1);
            tbResult.Text = $"Новая игра начата. Введи число от 0 до {diap}";
            btnCheck.IsEnabled = true;
        }

        int count = 0;

        private void btnCheck_Click(object sender, RoutedEventArgs e)
        {

            int guess;
            if (!int.TryParse(tbxGuess.Text, out guess))
            {
                tbResult.Text = "Введено не число, введите число!!";
                tbxGuess.Text = null;
                return;
            }

            if (guess == secretNumber)
            {
                tbResult.Text = $"Отлично! Вы угадали число!\nВам потребовалось {count} попыток";
                btnCheck.IsEnabled = false;
                _timer.Stop();
            }
            else if (guess < secretNumber)
            {
                tbResult.Text = "Введенное число меньше загаданного. Введите другое число.";
                count++;
            }
            else
            {
                tbResult.Text = "Введенное число больше загаданного. Введите другое число.";
                count++;
            }
            tbxGuess.Text = null;
        }

        private void tbxGuess_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                btnCheck_Click(sender, e);
            }
        }

        int counte = 0;
        private void btnCheck_Copy_Click(object sender, RoutedEventArgs e)
        {
            if (counte == 0)
            {
                _timer.Stop();
                counte++;
            }
            else
            {
                _timer.Start();
                counte--;
            }
        }

        private void btnCheck_Copy1_Click(object sender, RoutedEventArgs e)
        {
            Start.Visibility = Visibility.Visible;
            Gamee.Visibility = Visibility.Hidden;

            
            if (btnCheck.IsEnabled == true)//не угадал еще
            {
                _timer.Stop();
                StartGameButton.Content = "Продолжить";//спросить как поменять

            }
            else//угадал уже
            {
                NewGame(sender, e);
            }
        }

        private void NewGame(object sender, RoutedEventArgs e)
        {
            count = 0;
            Start.Visibility = Visibility.Hidden;
            Gamee.Visibility = Visibility.Visible;

            tbxGuess.Focus();
            StartNewGame(diap);

            _time = TimeSpan.FromSeconds(level);

            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                Time.Text = _time.ToString("c");
                if (_time == TimeSpan.Zero) Close();
                _time = _time.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);

            _timer.Start();
        }
    }

    public class GameTimer : INotifyPropertyChanged
    {
        private int _timer;

        public int Timer
        {
            get { return _timer; }
            set
            {
                _timer = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Timer)));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}