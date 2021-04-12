using Bus_Lite;
using Bus_Lite.Contract;
using System.Threading.Tasks;

namespace Console_Playground
{
    class Examples
    {
        /// <summary>
        /// creating an instance of an event bus
        /// </summary>
        public void CreateEventBusExample()
        {
            var eventBus = new EventBus();
        }

        /// <summary>
        /// subscribing listener to an event
        /// 
        /// using Subscribe() we can define multiple listeners for a single event
        /// listeners can not return any value
        /// 
        /// first argument is an owner (can not be of type 'ObserverToken')(almost always 'this')
        /// second argument is a callback function, where '@event' is 'string'
        /// method retuns token which is used to unsubscribing (so is owner)
        /// </summary>
        public void SubscribeListenersToEventBusExample()
        {
            var eventBus = new EventBus();
            var token = eventBus.Subscribe<string>(this, (@event) => { /* implementation */ });
            var token2 = eventBus.Subscribe(this, (string @event) => { /* implementation */ });
            var token3 = eventBus.Subscribe<string>(this, new StringEventListener()); // implementation of IEventListener<string>
            var token4 = eventBus.Subscribe(this, new StringEventListener()); // implementation of IEventListener<string>
        }

        /// <summary>
        /// unsubscribing given listener using it's token
        /// </summary>
        public void RemoveListenerFromEventBusUsingTokenExample()
        {
            var eventBus = new EventBus();
            // ...
            var token = eventBus.Subscribe<string>(this, (@event) => { /* implementation */ });
            // ...
            eventBus.Remove(token);
        }

        /// <summary>
        /// unsubscribing all owner's listeners
        /// </summary>
        public void RemoveListenerFromEventBusByOwnerExample()
        {
            var eventBus = new EventBus();
            // ...
            eventBus.Subscribe<string>(this, (@event) => { /* implementation */ });
            eventBus.Subscribe<int>(this, (@event) => { /* implementation */ });
            // ...
            eventBus.Remove(this);
        }

        /// <summary>
        /// pushing event to appropriate listeners
        /// all required listeners will be notified
        /// </summary>
        public void NotifyListenersAboutAnEventExample()
        {
            var eventBus = new EventBus();
            // ...
            eventBus.Subscribe<string>(this, (@event) => { /* implementation */ });
            eventBus.Subscribe<string>(this, (@event) => { /* implementation */ });
            // ...
            eventBus.Notify("example string");
        }

        /// <summary>
        /// 
        /// </summary>
        public void RegisterHandlersToEventBusExample()
        {
            var eventBus = new EventBus();
            var token = eventBus.Register<IEvent<string>, string>(this, async (@event) => await Task.FromResult(""));
            var token2 = eventBus.Register(this, async (IEvent<string> @event) => await Task.FromResult(""));
            var token3 = eventBus.Register<IEvent<string>, string>(this, new StringEventHandler()); // implementation of IEventHandler<IEvent<string>, string>
            var token4 = eventBus.Register(this, new StringEventHandler()); // implementation of IEventHandler<IEvent<string>, string>
        }

        /// <summary>
        /// unsubscribing given handler using it's token
        /// </summary>
        public void RemoveHandlersFromEventBusUsingTokenExample()
        {
            var eventBus = new EventBus();
            // ...
            var token = eventBus.Register(this, new StringEventHandler());
            // ...
            eventBus.Remove(token);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task HandleAnEventExample()
        {
            var eventBus = new EventBus();
            // ...
            eventBus.Register(this, new StringEventHandler());
            // ...
            var @event = new StringEvent("");
            var result = await eventBus.Handle(@event);
        }
    }

    class StringEventListener : IEventListener<string>
    {
        public void OnNotify(string @event)
        {
            /* implementation */
        }
    }

    class StringEventHandler : IEventHandler<IEvent<string>, string>
    {
        public async Task<string> Handle(IEvent<string> @event)
        {
            return await Task.FromResult("");
        }
    }

}
