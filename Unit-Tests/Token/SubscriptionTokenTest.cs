using Bus_Lite;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unit_Tests.Token
{
    [TestClass]
    public class SubscriptionTokenTest
    {
        private SubscriptionToken Token { get; set; }

        [TestInitialize]
        public void Before()
        {
            Token = new SubscriptionToken();
        }

        [TestMethod]
        public void ContainsCurrentDateTime()
        {
            Assert.AreEqual(DateTime.Now.Date.ToString(), Token.GenerationDateTime.Date.ToString());
        }

        [TestMethod]
        public void GuidIsUnique()
        {
            var token = new SubscriptionToken();

            Assert.AreNotEqual(token.Guid, Token.Guid);
        }

        [TestMethod]
        public void GetHashCodeIsUnique()
        {
            var token = new SubscriptionToken();

            Assert.AreNotEqual(token.GetHashCode(), Token.GetHashCode());
        }
    }
}
