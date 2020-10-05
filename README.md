# Bus-Lite

Bus-Lite is a small size, high performance tool for objects communication using events. Library is thread safe, written in C# and has no external dependencies.

# Table of contents
- [Features](#Features)
- [Examples](#Examples)
- [Todos](#Todos)

## Features

  - Subscribing a concrete event
  - Subscribing an abstract event
  - Unsubscribing given listener
  - Unsubscribing all owner's listeners
  - Pushing events
  - Thread safety

## Examples

 ```CSharp
// creating an event bus
var eventBus = new EventBus();
```

 ```CSharp
// subscribing to an event
// first argument is an owner (almost always 'this')
// second argument is a callback function, where 'event' is 'IEvent'
// method retuns token whitch is used to unsubscribing
var token = eventBus.Subscribe<IEvent>(this, (event) => { });
```

 ```CSharp
// unsubscribe listener given by token
var token = eventBus.Subscribe<IEvent>(this, (event) => { });
// ...
eventBus.Unsubscribe(token);
```

 ```CSharp
// unsubscribe all owner's listeners
// where owner is not 'SubscriptionToken' (almost always 'this')
var token = eventBus.Subscribe<IEvent>(this, (event) => { });
// ...
eventBus.Unsubscribe(owner);
```

```CSharp
// pushing event
// all required listeners will be notified
var event = new EventImp();
eventBus.Push(event);
```

### Todos

 - Write MORE Tests
 - Method to temporarily stop listening without unsubscribing