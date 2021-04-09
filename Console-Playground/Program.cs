using Bus_Lite;
using Bus_Lite.Contract;
using System;
using System.Threading.Tasks;

var bus = new EventBus();
bus.Register<IntEvent, int>(bus, async (e) => await Task.FromResult(5));
var stringEventHandler = new StringEventHandler();
bus.Register<StringEvent, string>(bus, stringEventHandler);
bus.Subscribe<StringEvent>(bus, stringEventHandler);
bus.Register<StringEvent, string>(bus, async (e) => await Task.FromResult(e.Value));
bus.Register<StringEvent, string>(bus, async (e) => await Task.FromResult(e.Value));

var result = await bus.Handle(new StringEvent("it works async!"));
Console.WriteLine(result);
bus.Notify(new StringEvent("it works sync!"));

record StringEvent(string Value) : IEvent<string> { }
class IntEvent : IEvent<int> { }

class StringEventHandler : IEventHandler<StringEvent, string>, IEventListener<StringEvent>
{
    public async Task<string> Handle(StringEvent @event)
    {
        await Task.Delay(2000);
        return @event.Value;
    }

    public void OnNotify(StringEvent @event)
    {
        Console.Write(@event.Value);
    }
}