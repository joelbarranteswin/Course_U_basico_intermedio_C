using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UnitTestingIntro;

namespace UnitTestIntro
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string result = Program.Something();
            Assert.AreEqual("valor", result);
        }

        [TestMethod]
        public void TestMethod2()
        {
            string result = Program.Something();
            Assert.AreEqual("valor", result);
        }

        [TestMethod]
        public void TestMethod3()
        {
            string result = Program.Something();
            Assert.AreNotEqual(false, result);
        }
    }
}
