using Bus_Lite;
using Bus_Lite.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

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

            Assert.IsTrue(token is SubscriptionToken);
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
        [ExpectedException(typeof(SubscriptionTokenOwnerException))]
        public void SubscriptionTokenCanNotBeAnOwner()
        {
            var token = EventBus.Subscribe<string>(this, (x) => { });
            EventBus.Subscribe<string>(token, (x) => { });
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
        public void ListenersWorksWithGenericEvents()
        {
            var counter = 0;
            EventBus.Subscribe<IEvent>(this, (x) => { counter++; });

            EventBus.Push(new Event());

            Assert.AreEqual(1, counter);
        }

        [TestMethod]
        public void SubscribeIsThreadSafe()
        {
            var iterations = 10000;
            var thread1 = new Thread(() =>
            {
                for (var i = 0; i < iterations; i++)
                {
                    EventBus.Subscribe<string>(this, (x) => { });
                }
            });
            var thread2 = new Thread(() =>
            {
                for (var i = 0; i < iterations; i++)
                {
                    EventBus.Subscribe<string>(this, (x) => { });
                }
            });
            thread1.Start();
            thread2.Start();
            thread1.Join();
            thread2.Join();

            Assert.AreEqual(2 * iterations, EventBus.Listeners.Count());
        }

        [TestMethod]
        public void UnsubscribeByTokenIsThreadSafe()
        {
            var iterations = 1000;
            var tokens = new List<SubscriptionToken>();
            for (var i = 0; i < iterations; i++)
            {
                var token = EventBus.Subscribe<string>(this, (x) => { });
                tokens.Add(token);
            }
            var thread1 = new Thread(() =>
            {
                for (var i = 0; i < iterations / 2; i++)
                {
                    var token = tokens[i];
                    EventBus.Unsubscribe(token);
                }
            });
            var thread2 = new Thread(() =>
            {
                for (var i = iterations / 2; i < iterations; i++)
                {
                    var token = tokens[i];
                    EventBus.Unsubscribe(token);
                }
            });
            thread1.Start();
            thread2.Start();
            thread1.Join();
            thread2.Join();

            Assert.AreEqual(0, EventBus.Listeners.Count());
        }

        [TestMethod]
        public void UnsubscribeByOwnerIsThreadSafe()
        {
            var iterations = 1000;
            var owners = new List<object>();
            for (var i = 0; i < iterations; i++)
            {
                owners.Add(new Event());
            }
            foreach (var owner in owners)
            {
                EventBus.Subscribe<string>(owner, (x) => { });
            }
            var thread1 = new Thread(() =>
            {
                for (var i = 0; i < iterations / 2; i++)
                {
                    var owner = owners[i];
                    EventBus.Unsubscribe(owner);
                }
            });
            var thread2 = new Thread(() =>
            {
                for (var i = iterations / 2; i < iterations; i++)
                {
                    var owner = owners[i];
                    EventBus.Unsubscribe(owner);
                }
            });
            thread1.Start();
            thread2.Start();
            thread1.Join();
            thread2.Join();

            Assert.AreEqual(0, EventBus.Listeners.Count());
        }

        [TestMethod]
        public void PushIsThreadSafe()
        {
            var iterations = 10000;
            var counter = 0;
            for (var i = 0; i < iterations; i++)
            {
                EventBus.Subscribe<string>(this, (x) => { counter++; });
            }

            var thread1 = new Thread(() =>
            {
                EventBus.Push("");
            });
            var thread2 = new Thread(() =>
            {
                EventBus.Push("");
            });
            thread1.Start();
            thread2.Start();
            thread1.Join();
            thread2.Join();

            Assert.AreEqual(2 * iterations, counter);
        }
    }

    interface IEvent { }
    class Event : IEvent { }
}
