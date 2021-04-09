using Bus_Lite;
using Bus_Lite.Contract;
using Bus_Lite.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unit_Tests.Models;

namespace Unit_Tests.Bus
{
    [TestClass]
    public class EventBusSubscribeListenerTest : EventBusSubscribeBaseTest
    {
        private IEventListener<string> Listener { get; set; }

        [TestInitialize]
        public override void Before()
        {
            base.Before();
            Listener = new StringHandler();
        }

        protected override ObserverToken SubscribeToBus()
        {
            return EventBus.Subscribe(this, Listener);
        }

        [TestMethod]
        [ExpectedException(typeof(NullObserverException))]
        public void ListenerCanNotBeNull()
        {
            Listener = null;
            EventBus.Subscribe(this, Listener);
        }
    }
}
