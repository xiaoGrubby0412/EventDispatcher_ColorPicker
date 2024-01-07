using System;

public interface IDestroy
{
    public void DestroySelf();
}

public interface IEvent : IDestroy
{
    public Enum EventType { get; set; }
}

public class EventBase : IEvent
{
    public EventBase(Enum eventType)
    {
        EventType = eventType;
    }

    public virtual Enum EventType { get; set; }

    public virtual void DestroySelf()
    {
        
    }
}

public interface IEventCenter : IDestroy
{
    bool AddEventListener(Enum EventType, EventHandler handler);

    bool RemoveEventListener(Enum EventType, EventHandler handler);

    //触发事件
    void TriggerEvent(Enum EventType, IEvent e, bool ifNow = false);
}

public delegate void EventHandler(IEvent evt);

public interface IEventHandlerManger
{
    bool AddHandler(EventHandler eventHandler);

    bool RemoveHandler(EventHandler eventHandler);

    void BroadCastEvent(IEvent evt);

    void Clear();
}