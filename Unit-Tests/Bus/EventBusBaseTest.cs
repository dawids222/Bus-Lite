using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibLite.Bus.Lite.Tests.Bus
{
    [TestClass]
    public class EventBusBaseTest
    {
        protected EventBus EventBus { get; set; }

        [TestInitialize]
        public virtual void Before()
        {
            EventBus = new EventBus();
        }
    }
}
