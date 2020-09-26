using Bus_Lite;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unit_Tests.Bus
{
    [TestClass]
    public class EventBusBaseTest
    {
        protected EventBus EventBus { get; set; }

        [TestInitialize]
        public void Before()
        {
            EventBus = new EventBus();
        }
    }
}
