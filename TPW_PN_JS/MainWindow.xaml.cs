// MainWindow.xaml.cs
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace TPW_PN_JS
{
    public partial class MainWindow : Window
    {
        private List<Kulka> list;
        private Random random = new Random();
        private DispatcherTimer timer;

        public MainWindow()
        {
            list = new List<Kulka>();
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1); // Przykładowy interwał 33ms = ~30 klatek na sekundę
            timer.Tick += Timer_Tick;
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            BallCanvas.Children.Clear();

            foreach (var ball in list)
            {
                ball.AktualizujPozycje(Boundary);
                var ellipse = new Ellipse
                {
                    Width = 20 * 2,
                    Height = 20 * 2,
                    Fill = Brushes.Blue
                };

                Canvas.SetLeft(ellipse, ball.X - 20);
                Canvas.SetTop(ellipse, ball.Y - 20);
                BallCanvas.Children.Add(ellipse);
            }
        }

        private double LosowaPredkosc()
        {
            return (random.NextDouble() * 2 - 1) * 5;
        }

        private void AddBallsButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(BallCountTextBox.Text, out int numberOfBalls))
            {
                list.Clear();
                BallCanvas.Children.Clear();

                for (int i = 0; i < numberOfBalls; i++)
                {
                    double leftBoundary = Boundary.Margin.Left + 20;
                    double topBoundary = Boundary.Margin.Top + 20;
                    double rightBoundary = leftBoundary + Boundary.Width - 40;
                    double bottomBoundary = topBoundary + Boundary.Height - 40;

                    double x = random.Next((int)leftBoundary, (int)rightBoundary);
                    double y = random.Next((int)topBoundary, (int)bottomBoundary);
                    Kulka kulka = new Kulka(x, y, LosowaPredkosc(), LosowaPredkosc());
                    list.Add(kulka);

                    var ellipse = new Ellipse
                    {
                        Width = 20 * 2,
                        Height = 20 * 2,
                        Fill = Brushes.Blue
                    };

                    Canvas.SetLeft(ellipse, kulka.X - 20);
                    Canvas.SetTop(ellipse, kulka.Y - 20);
                    BallCanvas.Children.Add(ellipse);
                }
            }
            else
            {
                MessageBox.Show("Wprowadź poprawną liczbę kulek.");

            }
            // Inicjalizacja i losowe rozmieszczenie kul
            int ballCount = Convert.ToInt32(BallCountTextBox.Text);
            list.Clear();
            Random rnd = new Random();
            for (int i = 0; i < ballCount; i++)
            {
                double x = rnd.Next((int)Boundary.Margin.Left + 20, (int)(Boundary.Margin.Left + Boundary.Width) - 20);
                double y = rnd.Next((int)Boundary.Margin.Top + 20, (int)(Boundary.Margin.Top + Boundary.Height) - 20);
                double velocityX = rnd.NextDouble() * 10 - 5;
                double velocityY = rnd.NextDouble() * 10 - 5;
                list.Add(new Kulka(x, y, velocityX, velocityY));
            }
            timer.Start();
        }
    }

    public class Kulka
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        public double VelocityX
        {
            get; set;
        }
        public double VelocityY { get; set; }
        public Kulka(double x, double y, double velocityX, double velocityY)
        {
            X = x;
            Y = y;
            VelocityX = velocityX;
            VelocityY = velocityY;
        }

        public void AktualizujPozycje(Rectangle boundary)
        {
            double leftBoundary = boundary.Margin.Left + 20;
            double topBoundary = boundary.Margin.Top + 20;
            double rightBoundary = leftBoundary + boundary.Width - 40;
            double bottomBoundary = topBoundary + boundary.Height - 40;

            double newX = X + VelocityX;
            double newY = Y + VelocityY;

            if (newX >= leftBoundary && newX <= rightBoundary)
            {
                X = newX;
            }
            else
            {
                VelocityX = -VelocityX;
            }

            if (newY >= topBoundary && newY <= bottomBoundary)
            {
                Y = newY;
            }
            else
            {
                VelocityY = -VelocityY;
            }
        }
    }
}

