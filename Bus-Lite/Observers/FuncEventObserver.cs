using System;

namespace Bus_Lite.Observers
{
    public class FuncEventObserver<TEvent, TResult> : BaseEventObserver<TEvent>
    {
        public Func<TEvent, TResult> Callback { get; }

        public FuncEventObserver(object owner, Func<TEvent, TResult> callback) : base(owner)
        {
            Callback = callback;
        }

        public override object Invoke(object @event)
        {
            return Callback.Invoke((TEvent)@event);
        }
    }
}
