using Bus_Lite;
using Bus_Lite.Exceptions;
using Bus_Lite.Handlers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unit_Tests.Models;

namespace Unit_Tests.Bus
{
    [TestClass]
    public class EventBusSubscribeHandlerTest : EventBusSubscribeBaseTest
    {
        private IEventHandler<string> Handler { get; set; }

        [TestInitialize]
        public override void Before()
        {
            base.Before();
            Handler = new StringHandler();
        }

        protected override SubscriptionToken SubscribeToBus()
        {
            return EventBus.Subscribe(this, Handler);
        }

        [TestMethod]
        [ExpectedException(typeof(NullHandlerException))]
        public void HandlerCanNotBeNull()
        {
            Handler = null;
            EventBus.Subscribe(this, Handler);
        }
    }
}
