using LibLite.Bus.Lite;
using LibLite.Bus.Lite.Listeners;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;
using LibLite.Bus.Lite.Tests.Models;

namespace LibLite.Bus.Lite.Tests.Bus.Remove
{
    [TestClass]
    public class EventBusRemoveHandlersTest : EventBusRemoveBaseTest
    {
        protected override IEnumerable<IEventObserver> Observers => EventBus.Handlers;

        protected override ObserverToken SubscribeToBus()
        {
            return SubscribeToBus(this);
        }

        protected override ObserverToken SubscribeToBus(object owner)
        {
            return EventBus.Register(owner, (StringEvent e) => Task.FromResult(""));
        }
    }
}
