using System;

namespace Bus_Lite
{
    public class SubscriptionToken
    {
        public DateTime GenerationDateTime { get; } = DateTime.Now;
        public Guid Guid { get; } = Guid.NewGuid();
    }
}
