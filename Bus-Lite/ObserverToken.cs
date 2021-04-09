using System;

namespace Bus_Lite
{
    public class ObserverToken
    {
        public DateTime GenerationDateTime { get; } = DateTime.Now;
        public Guid Guid { get; } = Guid.NewGuid();
    }
}
