using System;

namespace Bus_Lite.Listeners
{
    public class ActionEventListener<TEvent> : BaseEventListener<TEvent>
    {
        public Action<TEvent> Callback { get; }

        public ActionEventListener(object owner, Action<TEvent> callback) : base(owner)
        {
            Callback = callback;
        }

        public override object Handle(object @event)
        {
            Callback.Invoke((TEvent)@event);
            return null;
        }
    }
}
