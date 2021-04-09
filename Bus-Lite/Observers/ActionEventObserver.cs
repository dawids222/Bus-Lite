using System;

namespace Bus_Lite.Observers
{
    public class ActionEventObserver<TEvent> : BaseEventObserver<TEvent>
    {
        public Action<TEvent> Callback { get; }

        public ActionEventObserver(object owner, Action<TEvent> callback) : base(owner)
        {
            Callback = callback;
        }

        public override object Invoke(object @event)
        {
            Callback.Invoke((TEvent)@event);
            return null;
        }
    }
}
