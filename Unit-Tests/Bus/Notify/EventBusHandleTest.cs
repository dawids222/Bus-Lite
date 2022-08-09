using LibLite.Bus.Lite.Contract;
using LibLite.Bus.Lite.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace LibLite.Bus.Lite.Tests.Bus.Notify
{
    [TestClass]
    public class EventBusHandleTest : EventBusBaseTest
    {
        [TestMethod]
        public async Task HandlesEvent()
        {
            var expected = "success";
            var result = "";
            var @event = new StringEvent(expected);
            EventBus.Register(this, async (StringEvent e) => await Task.FromResult(e.Value));

            result = await EventBus.Handle(@event);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task FirstRegisteredHandlerHandlesEvent()
        {
            var @event = new StringEvent();
            EventBus.Register(this, async (StringEvent e) => await Task.FromResult(e.Value));
            EventBus.Register(this, async (StringEvent e) =>
            {
                Assert.Fail();
                return await Task.FromResult("fail");
            });

            await EventBus.Handle(@event);
        }

        [TestMethod]
        public async Task AfterRemoveFirstRegisteredHandlerHandlesEvent()
        {
            var @event = new StringEvent();
            var failEventToken = EventBus.Register(this, async (StringEvent e) =>
            {
                Assert.Fail();
                return await Task.FromResult("fail");
            });
            EventBus.Register(this, async (StringEvent e) => await Task.FromResult(e.Value));

            EventBus.Remove(failEventToken);
            await EventBus.Handle(@event);
        }

        [TestMethod]
        public async Task HandlersHandleCorrectTypesOfEvents()
        {
            var @event = new StringEvent();
            EventBus.Register(this, (IEvent<int> x) =>
            {
                Assert.Fail();
                return Task.FromResult(0);
            });
            EventBus.Register(this, (StringEvent x) =>
            {
                return Task.FromResult("");
            });

            await EventBus.Handle(@event);
        }

        [TestMethod]
        public async Task HandlersWorksWithGenericEvents()
        {
            var @event = new StringEvent();
            var counter = 0;
            EventBus.Register(this, (IEvent<string> x) =>
            {
                counter++;
                return Task.FromResult("");
            });

            await EventBus.Handle(@event);

            Assert.AreEqual(1, counter);
        }
    }
}
