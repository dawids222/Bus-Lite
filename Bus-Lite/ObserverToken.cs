using System;

namespace LibLite.Bus.Lite
{
    public class ObserverToken
    {
        public DateTime GenerationDateTime { get; } = DateTime.Now;
        public Guid Guid { get; } = Guid.NewGuid();
    }
}
