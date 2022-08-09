using LibLite.Bus.Lite;
using LibLite.Bus.Lite.Listeners;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace LibLite.Bus.Lite.Tests.Bus.Remove
{
    [TestClass]
    public class EventBusRemoveListenersTest : EventBusRemoveBaseTest
    {
        protected override IEnumerable<IEventObserver> Observers => EventBus.Listeners;

        protected override ObserverToken SubscribeToBus()
        {
            return SubscribeToBus(this);
        }

        protected override ObserverToken SubscribeToBus(object owner)
        {
            return EventBus.Subscribe(owner, (string e) => { });
        }
    }
}
