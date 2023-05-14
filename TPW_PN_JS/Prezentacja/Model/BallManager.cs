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
        private double ballDiameter = 20;

        public ObservableCollection<Ball> GenerateBalls(int ballCount)
        {
            ObservableCollection<Ball> balls = new ObservableCollection<Ball>();
            Random random = new Random();

            for (int i = 0; i < ballCount; i++)
            {
                double x = random.NextDouble() * (430 - ballDiameter);
                double y = random.NextDouble() * (430 - ballDiameter);

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

        public async Task UpdateBallsPosition(ObservableCollection<Ball> balls, double deltaTime)
        {
            const double boundary = 400;
            List<Task> tasks = new List<Task>();

            foreach (var ball in balls)
            {
                tasks.Add(Task.Run(() =>
                {
                    double newX = ball.X + ball.SpeedX * deltaTime;
                    double newY = ball.Y + ball.SpeedY * deltaTime;

                    if (newX < 0 || newX + ballDiameter > boundary)
                    {
                        ball.SpeedX = -ball.SpeedX;
                        newX = ball.X + ball.SpeedX * deltaTime;
                    }

                    if (newY < 0 || newY + ballDiameter > boundary)
                    {
                        ball.SpeedY = -ball.SpeedY;
                        newY = ball.Y + ball.SpeedY * deltaTime;
                    }

                    foreach (var otherBall in balls.Where(b => b != ball))
                    {
                        double dx = otherBall.X - newX;
                        double dy = otherBall.Y - newY;
                        double distance = Math.Sqrt(dx * dx + dy * dy);

                        if (distance < ballDiameter)
                        {
                            ball.SpeedX = -ball.SpeedX;
                            ball.SpeedY = -ball.SpeedY;
                            newX = ball.X + ball.SpeedX * deltaTime;
                            newY = ball.Y + ball.SpeedY * deltaTime;
                            break;
                        }
                    }

                    ball.X = newX;
                    ball.Y = newY;
                }));
            }

            await Task.WhenAll(tasks);
        }
    }
}
