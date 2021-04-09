using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unit_Tests.Models;

namespace Unit_Tests.Bus
{
    [TestClass]
    public class EventBusNotifyTest : EventBusBaseTest
    {
        [TestMethod]
        public void NotifiesListenersAboutPushedEvent()
        {
            var expected = "success";
            var result = "";
            EventBus.Subscribe<string>(this, (x) => { result = x; });

            EventBus.Notify(expected);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ListenersHandleCorrectTypesOfEvents()
        {
            var counter = 0;
            EventBus.Subscribe<string>(this, (x) => { counter++; });
            EventBus.Subscribe<int>(this, (x) => { counter++; });

            EventBus.Notify("");

            Assert.AreEqual(1, counter);
        }

        [TestMethod]
        public void ListenersWorksWithGenericEvents()
        {
            var counter = 0;
            EventBus.Subscribe<IEvent>(this, (x) => { counter++; });

            EventBus.Notify(new Event());

            Assert.AreEqual(1, counter);
        }
    }
}
