# Bus-Lite

Bus-Lite is a small size, high performance tool for objects communication using events. Library is thread safe, written in C# and has no external dependencies.

It supports 2 different workflows:
1. Notifying multiple listeners without returning any value
2. Notifying single handler which returns a value

# Table of contents
- [Features](#Features)
- [Examples](#Examples)
- [Todos](#Todos)

## Features

  - Sending an event to multiple listeners (synchronous, without return value)
  - Sending an event to a specyfic handler (asynchronous, with return value)
  - Subscribing listeners
  - Removing listeners using tokens
  - Registering handlers
  - Removing handlers using tokens
  - Removing all owner's observers at once
  - Multi-thread  safety

## Examples

 ```CSharp
// creating an instance of an event bus
var eventBus = new EventBus();
```

 ```CSharp
// subscribing listener to an event
//
// using Subscribe() we can define multiple listeners for a single event
// listeners can not return any value
//
// first argument is an owner (can not be of type 'ObserverToken')(almost always  'this')
// second argument is a callback function
// method retuns token which is used to unsubscribing (so is owner)
var eventBus = new EventBus();
var token = eventBus.Subscribe<string>(this, (@event) => { /* implementation */ });
var token2 = eventBus.Subscribe(this, (string @event) => { /* implementation */ });
var token3 = eventBus.Subscribe<string>(this, new StringEventListener()); // implementation of IEventListener<string>
var token4 = eventBus.Subscribe(this, new StringEventListener()); // implementation of IEventListener<string>
```

 ```CSharp
// unsubscribing given listener using it's token
var eventBus = new EventBus();
// ...
var token = eventBus.Subscribe<string>(this, (@event) => { /* implementation */ });
// ...
eventBus.Remove(token);
```

 ```CSharp
// unsubscribing all owner's listeners
var eventBus = new EventBus();
// ...
eventBus.Subscribe<string>(this, (@event) => { /* implementation */ });
eventBus.Subscribe<int>(this, (@event) => { /* implementation */ });
// ...
eventBus.Remove(this);
```

```CSharp
// pushing event to appropriate listeners
// all required listeners will be notified
var eventBus = new EventBus();
 // ...
eventBus.Subscribe<string>(this, (@event) => { /* implementation */ });
eventBus.Subscribe<string>(this, (@event) => { /* implementation */ });
// ...
eventBus.Notify("example string");
```
```CSharp
//registering handler to an event
//
// using Register() we can define single handler for a single event
// handlers can return value
// 
// first argument is an owner (can not be of type 'ObserverToken')(almost always 'this')
// second argument is a callback function
// method retuns token which is used to unsubscribing (so is owner)
var eventBus = new EventBus();
var token = eventBus.Register<IEvent<string>, string>(this, async (@event) => await Task.FromResult(""));
var token2 = eventBus.Register(this, async (IEvent<string> @event) => await Task.FromResult(""));
 var token3 = eventBus.Register<IEvent<string>, string>(this, new StringEventHandler()); // implementation of IEventHandler<IEvent<string>, string>
var token4 = eventBus.Register(this, new StringEventHandler()); // implementation of IEventHandler<IEvent<string>, string>
```

```CSharp
// unsubscribing given handler using it's token
var eventBus = new EventBus();
// ...
var token = eventBus.Register(this, new StringEventHandler());
// ...
eventBus.Remove(token);
```

```CSharp
// pushing event to appropriate handler
// only one handler will be notified
// call can be awaited and return a value
// if no handler was registed for en event an exception will be thrown
var eventBus = new EventBus();
// ...
eventBus.Register(this, new StringEventHandler());
// ...
var @event = new StringEvent("");
var result = await eventBus.Handle(@event);
```

### Todos

 - Write MORE Tests
 - Method to temporarily stop listening without unsubscribing