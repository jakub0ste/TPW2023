// MainWindow.xaml.cs
using System.Windows;


namespace TPW_PN_JS.View
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }
    //public partial class MainWindow : Window
    //{
    //    private List<Kulka> list;
    //    private Random random = new Random();
    //    private DispatcherTimer timer;
    //    private IKulkaService kulkaService = new KulkaService();

    //    public MainWindow()
    //    {
    //        list = new List<Kulka>();
    //        InitializeComponent();
    //        timer = new DispatcherTimer();
    //        timer.Interval = TimeSpan.FromMilliseconds(2);
    //        timer.Tick += Timer_Tick;
    //    }

    //    private void Timer_Tick(object sender, EventArgs e)
    //    {
    //        BallCanvas.Children.Clear();

    //        foreach (var ball in list)
    //        {
    //            kulkaService.AktualizujPozycje(ball, Boundary);
    //            var ellipse = new Ellipse
    //            {
    //                Width = 20 * 2,
    //                Height = 20 * 2,
    //                Fill = Brushes.Blue
    //            };

    //            Canvas.SetLeft(ellipse, ball.X - 20);
    //            Canvas.SetTop(ellipse, ball.Y - 20);
    //            BallCanvas.Children.Add(ellipse);
    //        }
    //    }

    //    private double LosowaPredkosc()
    //    {
    //        return (random.NextDouble() * 2 - 1) * 5;
    //    }

    //    public void AddBallsButton_Click(object sender, RoutedEventArgs e)
    //    {
    //        if (int.TryParse(BallCountTextBox.Text, out int numberOfBalls))
    //        {
    //            list.Clear();
    //            BallCanvas.Children.Clear();

    //            for (int i = 0; i < numberOfBalls; i++)
    //            {
    //                double leftBoundary = Boundary.Margin.Left + 20;
    //                double topBoundary = Boundary.Margin.Top + 20;
    //                double rightBoundary = leftBoundary + Boundary.Width - 40;
    //                double bottomBoundary = topBoundary + Boundary.Height - 40;

    //                double x = random.Next((int)leftBoundary, (int)rightBoundary);
    //                double y = random.Next((int)topBoundary, (int)bottomBoundary);
    //                Kulka kulka = new Kulka(x, y, LosowaPredkosc(), LosowaPredkosc());
    //                list.Add(kulka);

    //                var ellipse = new Ellipse
    //                {
    //                    Width = 20 * 2,
    //                    Height = 20 * 2,
    //                    Fill = Brushes.Blue
    //                };

    //                Canvas.SetLeft(ellipse, kulka.X - 20);
    //                Canvas.SetTop(ellipse, kulka.Y - 20);
    //                BallCanvas.Children.Add(ellipse);
    //            }
    //        }
    //        else
    //        {
    //            MessageBox.Show("Wprowadź poprawną liczbę kulek.");

    //        }
    //        // Inicjalizacja i losowe rozmieszczenie kul
    //        int ballCount = Convert.ToInt32(BallCountTextBox.Text);
    //        list.Clear();
    //        Random rnd = new Random();
    //        for (int i = 0; i < ballCount; i++)
    //        {
    //            double x = rnd.Next((int)Boundary.Margin.Left + 20, (int)(Boundary.Margin.Left + Boundary.Width) - 20);
    //            double y = rnd.Next((int)Boundary.Margin.Top + 20, (int)(Boundary.Margin.Top + Boundary.Height) - 20);
    //            double velocityX = rnd.NextDouble() * 10 - 5;
    //            double velocityY = rnd.NextDouble() * 10 - 5;
    //            list.Add(new Kulka(x, y, velocityX, velocityY));
    //        }
    //        timer.Start();
    //    }
    //}
}