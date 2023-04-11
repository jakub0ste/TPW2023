using Microsoft.VisualStudio.TestTools.UnitTesting;
using TPW_PN_JS.Logika;

namespace UnitTestProject1
{
    [TestClass]
    public class KulkaTest
    {
        [TestMethod]
        public void TestBallPropertyChanged()
        {
            var kulka = new Kulka();
            string changedPropertyName = null;

            kulka.PropertyChanged += (sender, args) =>
            {
                changedPropertyName = args.PropertyName;
            };

            kulka.X = 10;

            Assert.AreEqual(nameof(Kulka.X), changedPropertyName);
        }
    }
}
