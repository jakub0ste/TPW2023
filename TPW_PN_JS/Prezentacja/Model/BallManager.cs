using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPW_PN_JS.Logika;
using System.Collections.Concurrent;
using System.Threading;

namespace TPW_PN_JS.Prezentacja.Model
{
    public class BallManager
    {
        private ConcurrentBag<Ball> balls = new ConcurrentBag<Ball>(); 
        private const double boundary = 400;
        private const double ballSize = 20;

        public ConcurrentBag<Ball> GenerateAndStartBalls(int ballCount) //Pozwala na bezpieczne dodawanie i pobieranie elementów z różnych wątków.
        {
            Random random = new Random();

            for (int i = 0; i < ballCount; i++)
            {
                double x = random.NextDouble() * (400);
                double y = random.NextDouble() * (400);

                Ball ball = new Ball
                {
                    X = x,
                    Y = y,
                    SpeedX = random.Next(-100, 100) / 100.0,
                    SpeedY = random.Next(-100, 100) / 100.0,
                    Mass = 1,
                    //jak go dodać do Iball
                    Lock = new object()  // Each ball has a lock object
                };

                balls.Add(ball);
                                                                                            //Thread starting //Concurrent programming by Thread class
                Thread ballThread = new Thread(() => UpdateBallPosition(ball));
                ballThread.Start();
            }

            return balls;
        }

        private void UpdateBallPosition(Ball ball)
        {
            while (true)
            {
                double deltaTime = 0.5;
                double newX = ball.X + ball.SpeedX * deltaTime;
                double newY = ball.Y + ball.SpeedY * deltaTime;

                lock (ball.Lock) // Only one thread can enter this block at a time

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

                foreach (var otherBall in balls)
                {
                    if (otherBall != ball)
                    {
                        double dx = ball.X - otherBall.X;
                        double dy = ball.Y - otherBall.Y;
                        double distance = Math.Sqrt(dx * dx + dy * dy);

                        if (distance < ballSize)
                        {
                            double angle = Math.Atan2(dy, dx);
                            double sin = Math.Sin(angle);
                            double cos = Math.Cos(angle);

                            // Rotating the velocity vectors

                            double ballRotatedSpeedX = ball.SpeedX * cos + ball.SpeedY * sin;
                            double otherBallRotatedSpeedX = otherBall.SpeedX * cos + otherBall.SpeedY * sin;
                            double ballRotatedSpeedY = ball.SpeedY * cos - ball.SpeedX * sin;
                            double otherBallRotatedSpeedY = otherBall.SpeedY * cos - otherBall.SpeedX * sin;

                            // Updating the velocities after the collision

                            double ballFinalSpeedX = ((ball.Mass - otherBall.Mass) * ballRotatedSpeedX + 2 * otherBall.Mass * otherBallRotatedSpeedX) / (ball.Mass + otherBall.Mass);
                            double otherBallFinalSpeedX = (2 * ball.Mass * ballRotatedSpeedX + (otherBall.Mass - ball.Mass) * otherBallRotatedSpeedX) / (ball.Mass + otherBall.Mass);

                            ball.SpeedX = ballFinalSpeedX * cos - ballRotatedSpeedY * sin;
                            ball.SpeedY = ballFinalSpeedX * sin + ballRotatedSpeedY * cos;
                            otherBall.SpeedX = otherBallFinalSpeedX * cos - otherBallRotatedSpeedY * sin;
                            otherBall.SpeedY = otherBallFinalSpeedX * sin + otherBallRotatedSpeedY * cos;

                            // Displacing the balls slightly to avoid them from sticking to each other

                            double overlap = 0.5 * (distance - ballSize);
                            ball.X -= overlap * (ball.X - otherBall.X) / distance;
                            ball.Y -= overlap * (ball.Y - otherBall.Y) / distance;
                            otherBall.X += overlap * (ball.X - otherBall.X) / distance;
                            otherBall.Y += overlap * (ball.Y - otherBall.Y) / distance;
                        }
                    }

                    Thread.Sleep(2); //TaskParallerLibrary -> biblioteka
                }
            }
        }
    }
}

