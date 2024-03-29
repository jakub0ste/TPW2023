﻿using System.Collections.ObjectModel;
using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Threading;
using TPW_PN_JS.Logika;
using TPW_PN_JS.Prezentacja.ViewModel;
using TPW_PN_JS.Prezentacja.Model;

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
                Interval = TimeSpan.FromMilliseconds(2)
            };
            //_timer.Tick += OnTimerTick;
        }

        private void Start()
        {
            _timer.Stop();

            _ballManager = new BallManager(); // This will create a new BallManager instance

            Balls = new ObservableCollection<Ball>(_ballManager.GenerateAndStartBalls(BallCount));

            _timer.Start();
        }



        /* private void OnTimerTick(object sender, EventArgs e)
         {
             // No need to call UpdateBallsPosition here because it is already being called in the BallManager.
         }*/

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}



