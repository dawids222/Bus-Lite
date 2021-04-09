using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;
using Unit_Tests.Models;

namespace Unit_Tests.Bus.Remove
{
    [TestClass]
    public class EventBusRemoveListenersHandlersTest : EventBusBaseTest
    {
        [TestMethod]
        public void RemovesOnlyListenersByToken()
        {
            var listenerToken = EventBus.Subscribe(this, (string e) => { });
            var handlerToken = EventBus.Register(this, (StringEvent e) => Task.FromResult(""));

            EventBus.Remove(listenerToken);

            Assert.AreEqual(1, EventBus.Handlers.Count());
        }

        [TestMethod]
        public void RemovesOnlyHandlersByToken()
        {
            var listenerToken = EventBus.Subscribe(this, (string e) => { });
            var handlerToken = EventBus.Register(this, (StringEvent e) => Task.FromResult(""));

            EventBus.Remove(handlerToken);

            Assert.AreEqual(1, EventBus.Listeners.Count());
        }

        [TestMethod]
        public void RemovesEveryObserverByOwner()
        {
            var listenerToken = EventBus.Subscribe(this, (string e) => { });
            var handlerToken = EventBus.Register(this, (StringEvent e) => Task.FromResult(""));

            EventBus.Remove(this);

            Assert.AreEqual(0, EventBus.Listeners.Count());
            Assert.AreEqual(0, EventBus.Handlers.Count());
        }
    }
}
