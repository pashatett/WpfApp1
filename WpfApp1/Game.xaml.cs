using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace WpfApp1
{
    public partial class Game : Window
    {
        GameTimer timer {  get; set; }

        private int secretNumber;
        private Random random = new Random();
        TimeSpan _time;
        DispatcherTimer _timer;
        public Game(int level)
        {
            timer = new GameTimer();
            DataContext = timer;

            InitializeComponent();
            StartNewGame();

            _time = TimeSpan.FromSeconds(level);

            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                Time.Text = _time.ToString("c");
                if (_time == TimeSpan.Zero) Close();
                _time = _time.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);

            _timer.Start();
        }

        private void StartNewGame()
        {
            secretNumber = random.Next(0, 101);
            tbResult.Text = "Новая игра начата. Введи число от 0 до 100";
            btnCheck.IsEnabled = true;
        }

        int count = 0;

        private void btnCheck_Click(object sender, RoutedEventArgs e)
        {
            
            int guess;
            if (!int.TryParse(tbxGuess.Text, out guess))
            {
                tbResult.Text = "Invalid input! Please enter a valid number.";
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
            if(e.Key == System.Windows.Input.Key.Enter)
            {
                btnCheck_Click(sender, e);
            }
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
