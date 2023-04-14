using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using TPW_PN_JS.Logika;
using WpfApp1.Prezentacja.ViewModel;

namespace UnitTestProject1
{
    [TestClass]
    public class MainViewModelTest
    {
        [TestMethod]
        public void TestBallCountProperty()
        {
            MainViewModel viewModel = new MainViewModel();
            int ballCount = 10;

            viewModel.BallCount = ballCount;

            Assert.AreEqual(ballCount, viewModel.BallCount, "Lists are not equal");
        }

        [TestMethod]
        public void TestBallsProperty()
        {
            MainViewModel viewModel = new MainViewModel();
            ObservableCollection<Ball> balls = new ObservableCollection<Ball>
            {
                new Ball { X = 1, Y = 1, SpeedX = 0.5, SpeedY = 0.5 },
                new Ball { X = 2, Y = 2, SpeedX = -0.5, SpeedY = -0.5 }
            };

            viewModel.Balls = balls;

            CollectionAssert.AreEqual(balls, viewModel.Balls, "Lists are not equal");
        }

        [TestMethod]
        public void TestStartCommand()
        {
            MainViewModel viewModel = new MainViewModel();
            int ballCount = 10;
            viewModel.BallCount = ballCount;

            viewModel.StartCommand.Execute(null);

            Assert.AreEqual(ballCount, viewModel.Balls.Count, "Number of generated balls is wrong");
            Assert.IsTrue(viewModel.Balls[0].X >= 0, "Position X of first ball is wrong");
            Assert.IsTrue(viewModel.Balls[0].Y >= 0, "Position Y of first ball is wrong");
        }
    }
}
