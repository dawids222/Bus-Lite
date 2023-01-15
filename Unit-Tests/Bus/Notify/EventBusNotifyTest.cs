using LibLite.Bus.Lite.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LibLite.Bus.Lite.Tests.Bus.Notify
{
    [TestClass]
    public class EventBusNotifyTest : EventBusBaseTest
    {
        [TestMethod]
        public void NotifiesListenerAboutPushedEvent()
        {
            var expected = "success";
            var result = "";
            EventBus.Subscribe<string>(this, (x) => { result = x; });

            EventBus.Notify(expected);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void NotifiesMultipleListenersAboutPushedEvent()
        {
            var counter = 0;
            EventBus.Subscribe<Event>(this, (x) => { counter++; });
            EventBus.Subscribe<Event>(this, (x) => { counter++; });
            EventBus.Subscribe<Event>(this, (x) => { counter++; });
            EventBus.Subscribe<Event>(this, (x) => { counter++; });
            EventBus.Subscribe<Event>(this, (x) => { counter++; });

            EventBus.Notify(new Event());

            Assert.AreEqual(5, counter);
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
        public void ListenersDoNotWorkWithGenericEvents()
        {
            var counter = 0;
            EventBus.Subscribe<IEvent>(this, (x) => { counter++; });

            EventBus.Notify(new Event());

            Assert.AreEqual(0, counter);
        }

        [TestMethod]
        public void ListenersDoNotThrow()
        {
            var counter = 0;
            EventBus.Subscribe<Event>(this, (x) => { throw new Exception(); });
            EventBus.Subscribe<Event>(this, (x) => { counter++; });

            EventBus.Notify(new Event());

            Assert.AreEqual(1, counter);
        }
    }
}
