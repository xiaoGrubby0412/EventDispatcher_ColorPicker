using System;

public interface IDestroy
{
    public void DestroySelf();
}

public interface IEvent : IDestroy
{
    public Enum EventType { get; }
}

public interface IEventCenter : IDestroy
{
    bool AddEventListener(Enum EventType, EventHandler handler);

    bool RemoveEventListener(Enum EventType, EventHandler handler);

    //触发事件
    void TriggerEvent(IEvent e);

    //广播事件
    void BroadCastEvent();
}

public delegate void EventHandler(IEvent evt);

public interface IEventHandlerManger
{
    bool AddHandler(EventHandler eventHandler);

    bool RemoveHandler(EventHandler eventHandler);

    void BroadCastEvent(IEvent evt);

    void Clear();
}