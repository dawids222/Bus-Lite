using System;

namespace Bus_Lite.Listeners
{
    public class FuncEventListener<TEvent, TResult> : BaseEventListener<TEvent>
    {
        public Func<TEvent, TResult> Callback { get; }

        public FuncEventListener(object owner, Func<TEvent, TResult> callback) : base(owner)
        {
            Callback = callback;
        }

        public override object Handle(object @event)
        {
            return Callback.Invoke((TEvent)@event);
        }
    }
}
