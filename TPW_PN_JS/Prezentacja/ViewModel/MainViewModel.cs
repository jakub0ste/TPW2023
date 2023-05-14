using System.Collections.ObjectModel;
using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Threading;
using TPW_PN_JS.Logika;
using TPW_PN_JS.Prezentacja.ViewModel;
using TPW_PN_JS.Prezentacja.Model;
using System.Threading;

namespace WpfApp1.Prezentacja.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private int _ballCount;
        private DispatcherTimer _timer;
        private ObservableCollection<Ball> _balls;
        private BallManager _ballManager;

        public int BallCount
        {
            get => _ballCount;
            set
            {
                _ballCount = value;
                OnPropertyChanged(nameof(BallCount));
            }
        }

        public ObservableCollection<Ball> Balls
        {
            get => _balls;
            set
            {
                _balls = value;
                OnPropertyChanged(nameof(Balls));
            }
        }

        public ICommand StartCommand { get; }

        public MainViewModel()
        {
            StartCommand = new RelayCommand(Start);
            InitializeTimer();
            _ballManager = new BallManager();
        }

        private void InitializeTimer()
        {
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(16) // Ustaw interwał na 60 klatek na sekundę
            };
            _timer.Tick += OnTimerTick;
        }

        private async void Start()
        {
            _timer.Stop();
            var newBalls = _ballManager.GenerateBalls(BallCount);
            Balls = new ObservableCollection<Ball>(newBalls);
            _timer.Start();
        }

        private async void OnTimerTick(object sender, EventArgs e)
        {
            var newBalls = new ObservableCollection<Ball>(Balls);
            await _ballManager.UpdateBallsPosition(newBalls, 0.5);
            Balls = newBalls;
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


}
