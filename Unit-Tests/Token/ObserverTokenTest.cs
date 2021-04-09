using Bus_Lite;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unit_Tests.Token
{
    [TestClass]
    public class ObserverTokenTest
    {
        private ObserverToken Token { get; set; }

        [TestInitialize]
        public void Before()
        {
            Token = new ObserverToken();
        }

        [TestMethod]
        public void ContainsCurrentDateTime()
        {
            Assert.AreEqual(DateTime.Now.Date.ToString(), Token.GenerationDateTime.Date.ToString());
        }

        [TestMethod]
        public void GuidIsUnique()
        {
            var token = new ObserverToken();

            Assert.AreNotEqual(token.Guid, Token.Guid);
        }

        [TestMethod]
        public void GetHashCodeIsUnique()
        {
            var token = new ObserverToken();

            Assert.AreNotEqual(token.GetHashCode(), Token.GetHashCode());
        }
    }
}
