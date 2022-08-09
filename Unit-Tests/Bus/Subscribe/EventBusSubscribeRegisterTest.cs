using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;
using LibLite.Bus.Lite.Tests.Models;

namespace LibLite.Bus.Lite.Tests.Bus.Subscribe
{
    [TestClass]
    public class EventBusSubscribeRegisterTest : EventBusBaseTest
    {
        [TestMethod]
        public void SubscribeDoesNotAddToHandlers()
        {
            EventBus.Subscribe(this, (string e) => { });
            EventBus.Subscribe(this, new StringListener());

            Assert.AreEqual(0, EventBus.Handlers.Count());
        }

        [TestMethod]
        public void RegisterDoesNotAddToListeners()
        {
            EventBus.Register(this, (StringEvent e) => Task.FromResult(""));
            EventBus.Register(this, new StringHandler());

            Assert.AreEqual(0, EventBus.Listeners.Count());
        }
    }
}
