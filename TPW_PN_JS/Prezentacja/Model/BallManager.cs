using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPW_PN_JS.Logika;

namespace TPW_PN_JS.Prezentacja.Model
{
    public class BallManager
    {
        public ObservableCollection<Ball> GenerateBalls(int ballCount)
        {
            ObservableCollection<Ball> balls = new ObservableCollection<Ball>();
            Random random = new Random();

            for (int i = 0; i < ballCount; i++)
            {
                double x = random.NextDouble() * (400);
                double y = random.NextDouble() * (400);

                balls.Add(new Ball
                {
                    X = x,
                    Y = y,
                    SpeedX = random.Next(-100, 100) / 100.0,
                    SpeedY = random.Next(-100, 100) / 100.0
                });
            }

            return balls;
        }

        public void UpdateBallsPosition(ObservableCollection<Ball> balls, double deltaTime)
        {
            const double boundary = 400;

            foreach (var ball in balls)
            {
                double newX = ball.X + ball.SpeedX * deltaTime;
                double newY = ball.Y + ball.SpeedY * deltaTime;

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
    }
}
