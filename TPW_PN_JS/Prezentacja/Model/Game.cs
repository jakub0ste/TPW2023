﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Threading;
using TPW_PN_JS.Logika;

namespace TPW_PN_JS.Prezentacja.Model
{
    public class Game: INotifyPropertyChanged
    {
        private int _ballCount;
        private DispatcherTimer _timer;
        private ObservableCollection<Ball> _balls;

        public Game()
        {
            InitializeTimer();
        }


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
        private void InitializeTimer()
        {
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(2) // Ustaw interwał na 60 klatek na sekundę
            };
            _timer.Tick += OnTimerTick;
        }


        public void Start()
        {
            Random random = new Random();
            Balls = new ObservableCollection<Ball>();

            for (int i = 0; i < BallCount; i++)
            {
                double x = random.NextDouble() * (400);
                double y = random.NextDouble() * (400);

                Balls.Add(new Ball
                {
                    X = x,
                    Y = y,
                    SpeedX = random.Next(-100, 100) / 100.0, // Dodaj losową prędkość poziomą
                    SpeedY = random.Next(-100, 100) / 100.0  // Dodaj losową prędkość pionową
                });
            }
            _timer.Start();
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            UpdateBallsPosition();
        }

        private void UpdateBallsPosition()
        {
            const double boundary = 400; // Zmiana wartości na 400
            const double deltaTime = 0.5;
            foreach (var ball in Balls)
            {
                double newX = ball.X + ball.SpeedX * deltaTime;
                double newY = ball.Y + ball.SpeedY * deltaTime;

                // Obsługa kolizji z krawędziami obszaru Canvas
                if (newX < 0 || newX + 20 > boundary)
                {
                    ball.SpeedX = -ball.SpeedX;
                    newX = ball.X + ball.SpeedY * deltaTime;
                }

                if (newY < 0 || newY + 20 > boundary)
                {
                    ball.SpeedY = -ball.SpeedY;
                    newY = ball.Y + ball.SpeedY * deltaTime;
                }

                ball.X = newX;
                ball.Y = newY;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
