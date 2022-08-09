using LibLite.Bus.Lite;
using LibLite.Bus.Lite.Contract;
using LibLite.Bus.Lite.Exceptions;
using LibLite.Bus.Lite.Listeners;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using LibLite.Bus.Lite.Tests.Models;

namespace LibLite.Bus.Lite.Tests.Bus.Subscribe
{
    [TestClass]
    public class EventBusSubscribeListenerTest : EventBusSubscribeBaseTest
    {
        private IEventListener<string> Listener { get; set; }

        protected override IEnumerable<IEventObserver> Observers => EventBus.Listeners;

        [TestInitialize]
        public override void Before()
        {
            base.Before();
            Listener = new StringListener();
        }

        protected override ObserverToken SubscribeToBus()
        {
            return SubscribeToBus(this);
        }

        protected override ObserverToken SubscribeToBus(object owner)
        {
            return EventBus.Subscribe(owner, Listener);
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
