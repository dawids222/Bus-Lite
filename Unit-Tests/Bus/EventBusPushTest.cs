using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unit_Tests.Models;

namespace Unit_Tests.Bus
{
    [TestClass]
    public class EventBusPushTest : EventBusBaseTest
    {
        [TestMethod]
        public void PushesEventToListeners()
        {
            var expected = "success";
            var result = "";
            EventBus.Subscribe<string>(this, (x) => { result = x; });

            EventBus.Push(expected);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ListenersHandleCorrectTypesOfEvents()
        {
            var counter = 0;
            EventBus.Subscribe<string>(this, (x) => { counter++; });
            EventBus.Subscribe<int>(this, (x) => { counter++; });

            EventBus.Push("");

            Assert.AreEqual(1, counter);
        }

        [TestMethod]
        public void ListenersWorksWithGenericEvents()
        {
            var counter = 0;
            EventBus.Subscribe<IEvent>(this, (x) => { counter++; });

            EventBus.Push(new Event());

            Assert.AreEqual(1, counter);
        }
    }
}
