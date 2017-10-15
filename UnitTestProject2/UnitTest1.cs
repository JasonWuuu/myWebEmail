using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BLL;

namespace UnitTestProject2
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string expected = "{\"id\":1,\"name\":\"wucong\"}";
            string actual = new UserService().GetUser();

            Assert.AreEqual(expected, actual);
        }

        public void TestMethod2()
        {
            string expected = "{\"id\":2,\"name\":\"wucong\"}";
            string actual = new UserService().GetUser();

            Assert.AreEqual(expected, actual);
        }
    }
}
