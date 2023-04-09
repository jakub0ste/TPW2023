using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TPW_PN_JS
{
    public partial class MainWindow : Window
    {
        private List<Kulka> list;
        private Random random = new Random();

        public MainWindow()
        {
            list = new List<Kulka>();
            Kulka kulka = new Kulka(200, 300, LosowaPredkosc(), LosowaPredkosc());
            list.Add(kulka);
            Kulka kulka2 = new Kulka(130, 100, LosowaPredkosc(), LosowaPredkosc());
            list.Add(kulka2);
            InitializeComponent();
        }

        private double LosowaPredkosc()
        {
            return (random.NextDouble() * 2 - 1) * 5;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            // Inicjalizacja i losowe rozmieszczenie kul
            // Czyszczenie Canvas
            BallCanvas.Children.Clear();

            // Rysowanie kul na Canvas
            foreach (var ball in list)
            {
                ball.AktualizujPozycje();
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
    }

    public class Kulka
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        public double VelocityX { get; set; }
        public double VelocityY { get; set; }

        public Kulka(double x, double y, double velocityX, double velocityY)
        {
            X = x;
            Y = y;
            VelocityX = velocityX;
            VelocityY = velocityY;
        }

        public void AktualizujPozycje()
        {
            X += VelocityX;
            Y += VelocityY;
        }
    }
}