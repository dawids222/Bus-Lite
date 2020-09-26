using Bus_Lite;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Unit_Tests.Models;

namespace Unit_Tests.Bus
{
    [TestClass]
    public class EventBusThreadSafeTest : EventBusBaseTest
    {
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
            var iterations = 10000;
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
            var iterations = 2000;
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
}
