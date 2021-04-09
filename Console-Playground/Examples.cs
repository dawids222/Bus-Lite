using Bus_Lite;

namespace Console_Playground
{
    class Examples
    {
        /// <summary>
        /// creting an event bus
        /// </summary>
        public static EventBus CreateEventBusExample()
        {
            var eventBus = new EventBus();
            return eventBus;
        }

        /// <summary>
        /// subscribing to an event
        /// first argument is an owner (almost always 'this')
        /// second argument is a callback function, where '@event' is 'string'
        /// method retuns token whitch is used to unsubscribing
        /// </summary>
        public ObserverToken SubscribeListenerToEventBusExample()
        {
            var eventBus = CreateEventBusExample();
            var token = eventBus.Subscribe<string>(this, (@event) => { });
            return token;
        }

        /// <summary>
        /// unsubscribing listener given by token
        /// </summary>
        public void UnsubscribeListenerFromEventBusByTokenExample()
        {
            var eventBus = CreateEventBusExample();
            var token = eventBus.Subscribe<string>(this, (@event) => { /* implementation */ });
            eventBus.Remove(token);
        }

        /// <summary>
        /// unsubscribing all owner's listeners
        /// where owner is not 'SubscriptionToken' (almost always 'this')
        /// </summary>
        public void UnsubscribeListenerFromEventBusByOwnerExample()
        {
            var eventBus = CreateEventBusExample();
            eventBus.Subscribe<string>(this, (@event) => { /* implementation */ });
            eventBus.Subscribe<int>(this, (@event) => { /* implementation */ });
            eventBus.Remove(this);
        }

        /// <summary>
        /// pushing event
        /// all required listeners will be notified
        /// </summary>
        public void PushEventToAllListenersExample()
        {
            var eventBus = CreateEventBusExample();
            eventBus.Subscribe<string>(this, (@event) => { /* implementation */ });
            eventBus.Notify("example string");
        }
    }
}
