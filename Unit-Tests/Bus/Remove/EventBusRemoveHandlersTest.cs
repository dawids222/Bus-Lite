using Bus_Lite;
using Bus_Lite.Listeners;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unit_Tests.Models;

namespace Unit_Tests.Bus.Remove
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
