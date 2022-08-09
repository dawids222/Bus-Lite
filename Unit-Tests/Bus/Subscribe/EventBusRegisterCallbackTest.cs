using LibLite.Bus.Lite;
using LibLite.Bus.Lite.Listeners;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;
using LibLite.Bus.Lite.Tests.Models;

namespace LibLite.Bus.Lite.Tests.Bus.Subscribe
{
    [TestClass]
    public class EventBusRegisterCallbackTest : EventBusSubscribeBaseTest
    {
        protected override IEnumerable<IEventObserver> Observers => EventBus.Handlers;

        protected override ObserverToken SubscribeToBus()
        {
            return SubscribeToBus(this);
        }

        protected override ObserverToken SubscribeToBus(object owner)
        {
            return EventBus.Register<StringEvent, string>(owner, (e) => Task.FromResult(""));
        }
    }
}
