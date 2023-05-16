using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TPW_PN_JS.Logika;
using TPW_PN_JS.Prezentacja.Model;

namespace UnitTestProject1
{
    [TestClass]
    public class BallManagerTest
    {
        [TestMethod]
        public void TestGenerateBalls()
        {
            BallManager ballManager = new BallManager();
            int ballCount = 10;
            ObservableCollection<Ball> balls = ballManager.GenerateBalls(ballCount);

            Assert.AreEqual(ballCount, balls.Count, "Liczba wygenerowanych kulek nie zgadza się.");
        }

        [TestMethod]
        public async Task TestUpdateBallsPosition()
        {
            BallManager ballManager = new BallManager();
            int ballCount = 10;
            ObservableCollection<Ball> balls = ballManager.GenerateBalls(ballCount);

            ObservableCollection<Ball> initialBallsState = new ObservableCollection<Ball>(balls.Select(ball => new Ball
            {
                X = ball.X,
                Y = ball.Y,
                SpeedX = ball.SpeedX,
                SpeedY = ball.SpeedY
            }));

            await ballManager.UpdateBallsPositionAsync(balls, 0.5);

            bool positionsChanged = false;
            for (int i = 0; i < balls.Count; i++)
            {
                if (balls[i].X != initialBallsState[i].X || balls[i].Y != initialBallsState[i].Y)
                {
                    positionsChanged = true;
                    break;
                }
            }

            Assert.IsTrue(positionsChanged, "Pozycje kulek nie zmieniły się.");
        }
    }
}
