using Bus_Lite;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Unit_Tests
{
    [TestClass]
    public class EventBusTest : EventBus
    {
        EventBus EventBus { get; set; }

        [TestInitialize]
        public void Before()
        {
            EventBus = new EventBus();
        }

        [TestMethod]
        public void AddsListener()
        {
            EventBus.Subscribe<string>(this, (x) => { });

            Assert.AreEqual(1, EventBus.Listeners.Count());
            Assert.AreEqual(this, EventBus.Listeners.ElementAt(0).Owner);
        }

        [TestMethod]
        public void AddsMultipleListeners()
        {
            EventBus.Subscribe<string>(this, (x) => { });
            EventBus.Subscribe<string>(this, (x) => { });
            EventBus.Subscribe<string>(this, (x) => { });
            EventBus.Subscribe<string>(this, (x) => { });
            EventBus.Subscribe<string>(this, (x) => { });

            Assert.AreEqual(5, EventBus.Listeners.Count());
        }

        [TestMethod]
        public void ReturnsSubscriptionToken()
        {
            var token = EventBus.Subscribe<string>(this, (x) => { });

            Assert.AreEqual(DateTime.Now, token.GenerationDateTime);
        }

        [TestMethod]
        public void RemovesListenerByToken()
        {
            var token = EventBus.Subscribe<string>(this, (x) => { });
            EventBus.Subscribe<string>(this, (x) => { });
            EventBus.Unsubscribe(token);

            Assert.AreEqual(1, EventBus.Listeners.Count());
        }

        [TestMethod]
        public void RemovesAllListenersFromOwner()
        {
            EventBus.Subscribe<string>(this, (x) => { });
            EventBus.Subscribe<string>(this, (x) => { });
            EventBus.Subscribe<string>(this, (x) => { });
            EventBus.Subscribe<string>("", (x) => { });

            EventBus.Unsubscribe(this);

            Assert.AreEqual(1, EventBus.Listeners.Count());
            Assert.AreEqual("", EventBus.Listeners.ElementAt(0).Owner);
        }

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
        public void ListenersWorsWithGenericEvents()
        {
            var counter = 0;
            EventBus.Subscribe<IEvent>(this, (x) => { counter++; });

            EventBus.Push(new Event());

            Assert.AreEqual(1, counter);
        }
    }

    interface IEvent { }
    class Event : IEvent { }
}
