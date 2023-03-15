using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            var mainWindow = new TPW_PN_JS.MainWindow();

            // Act
            var actual = TPW_PN_JS.MainWindow.word;

            // Assert
            var expected = "Hello World";
            Assert.AreEqual(expected, actual);
        }
    }
}
