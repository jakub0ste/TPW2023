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
                    SpeedY = random.Next(-100, 100) / 100.0,
                    Mass = 1
                });
            }

            return balls;
        }

        public void UpdateBallsPosition(ObservableCollection<Ball> balls, double deltaTime)
        {
            const double boundary = 400;
            const double ballSize = 20; // assuming ball size is 20

            foreach (var ball in balls)
            {
                double newX = ball.X + ball.SpeedX * deltaTime;
                double newY = ball.Y + ball.SpeedY * deltaTime;

                if (newX < 0 || newX + ballSize > boundary)
                {
                    ball.SpeedX = -ball.SpeedX;
                    newX = ball.X + ball.SpeedX * deltaTime;
                }

                if (newY < 0 || newY + ballSize > boundary)
                {
                    ball.SpeedY = -ball.SpeedY;
                    newY = ball.Y + ball.SpeedY * deltaTime;
                }

                ball.X = newX;
                ball.Y = newY;
            }

            for (int i = 0; i < balls.Count; i++)
            {
                for (int j = i + 1; j < balls.Count; j++)
                {
                    Ball ball1 = balls[i];
                    Ball ball2 = balls[j];

                    double dx = ball1.X - ball2.X;
                    double dy = ball1.Y - ball2.Y;
                    double distance = Math.Sqrt(dx * dx + dy * dy);

                    if (distance < ballSize)
                    {
                        double angle = Math.Atan2(dy, dx);
                        double sin = Math.Sin(angle);
                        double cos = Math.Cos(angle);

                        double ball1RotatedX = 0;
                        double ball2RotatedX = distance;
                        double ball1RotatedSpeedX = ball1.SpeedX * cos + ball1.SpeedY * sin;
                        double ball2RotatedSpeedX = ball2.SpeedX * cos + ball2.SpeedY * sin;
                        double ball1RotatedSpeedY = ball1.SpeedY * cos - ball1.SpeedX * sin;
                        double ball2RotatedSpeedY = ball2.SpeedY * cos - ball2.SpeedX * sin;

                        double ball1FinalSpeedX = ((ball1.Mass - ball2.Mass) * ball1RotatedSpeedX + 2 * ball2.Mass * ball2RotatedSpeedX) / (ball1.Mass + ball2.Mass);
                        double ball2FinalSpeedX = (2 * ball1.Mass * ball1RotatedSpeedX + (ball2.Mass - ball1.Mass) * ball2RotatedSpeedX) / (ball1.Mass + ball2.Mass);

                        ball1.SpeedX = ball1FinalSpeedX * cos - ball1RotatedSpeedY * sin;
                        ball1.SpeedY = ball1FinalSpeedX * sin + ball1RotatedSpeedY * cos;
                        ball2.SpeedX = ball2FinalSpeedX * cos - ball2RotatedSpeedY * sin;
                        ball2.SpeedY = ball2FinalSpeedX * sin + ball2RotatedSpeedY * cos;

                        // Add a small displacement to ensure balls do not stick
                        double overlap = 0.5 * (distance - ballSize);
                        ball1.X -= overlap * (ball1.X - ball2.X) / distance;
                        ball1.Y -= overlap * (ball1.Y - ball2.Y) / distance;
                        ball2.X += overlap * (ball1.X - ball2.X) / distance;
                        ball2.Y += overlap * (ball1.Y - ball2.Y) / distance;
                    }
                }
            }
        }


    }
}
