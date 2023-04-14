using Microsoft.VisualStudio.TestTools.UnitTesting;
using TPW_PN_JS.Logika;
using TPW_PN_JS.Dane;

namespace UnitTestProject1
{
    [TestClass]
    public class BallTest
    {
        [TestMethod]
        public void TestBallPropertyChanged()
        {
            IBall ball = new Ball();
            string changedPropertyName = null;

            ball.PropertyChanged += (sender, args) =>
            {
                changedPropertyName = args.PropertyName;
            };

            ball.X = 10;
            Assert.AreEqual(nameof(Ball.X), changedPropertyName);
            Assert.AreEqual(ball.X, 10);
            ball.Y = 5;
            Assert.AreEqual(nameof(Ball.Y), changedPropertyName);
            Assert.AreEqual(ball.Y, 5);
            ball.Speed = 2;
            Assert.AreEqual(nameof(Ball.Speed), changedPropertyName);
            Assert.AreEqual(ball.Speed, 2);


        }
    }
}
