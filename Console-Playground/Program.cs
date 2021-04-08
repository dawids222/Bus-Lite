﻿using Bus_Lite;
using Bus_Lite.Events;
using Bus_Lite.Handlers;
using System;
using System.Threading.Tasks;

var bus = new EventBus();
bus.Subscribe<IntEvent, int>(bus, async (e) => await Task.FromResult(5));
var stringEventHandler = new StringEventHandler();
bus.Subscribe<StringEvent, string>(bus, stringEventHandler);
bus.Subscribe<StringEvent>(bus, stringEventHandler);
bus.Subscribe<StringEvent, string>(bus, async (e) => await Task.FromResult(e.Value));
bus.Subscribe<StringEvent, string>(bus, async (e) => await Task.FromResult(e.Value));

var result = await bus.Send(new StringEvent("it works async!"));
Console.WriteLine(result);
bus.Push(new StringEvent("it works sync!"));

record StringEvent(string Value) : IEvent<string> { }
class IntEvent : IEvent<int> { }

class StringEventHandler : IEventHandler<StringEvent, string>, IEventHandler<StringEvent>
{
    public async Task<string> Handle(StringEvent @event)
    {
        await Task.Delay(2000);
        return @event.Value;
    }

    void IEventHandler<StringEvent>.Handle(StringEvent @event)
    {
        Console.Write(@event.Value);
    }
}