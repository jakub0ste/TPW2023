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
        const double boundary = 400;
        const double ballDiameter = 40; // Średnica kuli to dwukrotność promienia

        public ObservableCollection<Ball> GenerateBalls(int ballCount)
        {
            ObservableCollection<Ball> balls = new ObservableCollection<Ball>();
            Random random = new Random();

            for (int i = 0; i < ballCount; i++)
            {
                double x = random.NextDouble() * (boundary - ballDiameter);
                double y = random.NextDouble() * (boundary - ballDiameter);

                balls.Add(new Ball
                {
                    X = x,
                    Y = y,
                    SpeedX = random.Next(-100, 100) / 100.0,
                    SpeedY = random.Next(-100, 100) / 100.0,
                    //SpeedX = 1,
                    //SpeedY = 1,
                    //Mass = random.Next(1, 10) // Przykładowa masa, do zmiany zgodnie z wymaganiami
                    Mass = 0.1,
                }); 
            }

            return balls;
        }

        public async Task UpdateBallsPosition(ObservableCollection<Ball> balls, double deltaTime)
        {
            foreach (var ball in balls)
            {
                double newX = ball.X + ball.SpeedX * deltaTime;
                double newY = ball.Y + ball.SpeedY * deltaTime;

                foreach (var otherBall in balls.Where(b => b != ball))
                {
                    double dx = otherBall.X - newX;
                    double dy = otherBall.Y - newY;
                    double distance = Math.Sqrt(dx * dx + dy * dy);

                    if (distance < ballDiameter)
                    {
                        double collisionAngle = Math.Atan2(dy, dx);

                        // Przesuń kulki z powrotem do momentu tuż przed zderzeniem
                        double overlap = ballDiameter - distance;
                        double separationDistance = overlap / 2;
                        newX -= separationDistance * Math.Cos(collisionAngle);
                        newY -= separationDistance * Math.Sin(collisionAngle);
                        otherBall.X += separationDistance * Math.Cos(collisionAngle);
                        otherBall.Y += separationDistance * Math.Sin(collisionAngle);

                        double speed1 = Math.Sqrt(ball.SpeedX * ball.SpeedX + ball.SpeedY * ball.SpeedY);
                        double speed2 = Math.Sqrt(otherBall.SpeedX * otherBall.SpeedX + otherBall.SpeedY * otherBall.SpeedY);

                        double direction1 = Math.Atan2(ball.SpeedY, ball.SpeedX);
                        double direction2 = Math.Atan2(otherBall.SpeedY, otherBall.SpeedX);

                        double newSpeed1 = (speed1 * (ball.Mass - otherBall.Mass) + 2 * otherBall.Mass * speed2) / (ball.Mass + otherBall.Mass);
                        double newSpeed2 = (speed2 * (otherBall.Mass - ball.Mass) + 2 * ball.Mass * speed1) / (ball.Mass + otherBall.Mass);

                        ball.SpeedX = Math.Cos(collisionAngle) * newSpeed1 + Math.Cos(collisionAngle + Math.PI / 2) * (speed1 * Math.Sin(direction1 - collisionAngle));
                        ball.SpeedY = Math.Sin(collisionAngle) * newSpeed1 + Math.Sin(collisionAngle + Math.PI / 2) * (speed1 * Math.Sin(direction1 - collisionAngle));
                        otherBall.SpeedX = Math.Cos(collisionAngle) * newSpeed2 + Math.Cos(collisionAngle + Math.PI / 2) * (speed2 * Math.Sin(direction2 - collisionAngle));
                        otherBall.SpeedY = Math.Sin(collisionAngle) * newSpeed2 + Math.Sin(collisionAngle + Math.PI / 2) * (speed2 * Math.Sin(direction2 - collisionAngle));

                        newX = ball.X + ball.SpeedX * deltaTime;
                        newY = ball.Y + ball.SpeedY * deltaTime;
                    }
                }

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

                ball.X = newX;
                ball.Y = newY;
            }
        }
    }
}

