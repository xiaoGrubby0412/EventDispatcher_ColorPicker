using System;
using System.Collections.Generic;
using UnityEngine;

public enum EventType
{
    ShowHello,
    ShoeError,
}

public class EventCenter : MonoBehaviour, IEventCenter
{
    private Dictionary<Enum, IEventHandlerManger> DictHandler = new Dictionary<Enum, IEventHandlerManger>();
    private Queue<IEvent> eventQuene = new Queue<IEvent>();

    private static EventCenter instance;

    public static EventCenter Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
    }

    public bool AddEventListener(Enum EventType, EventHandler handler)
    {
        bool isSuccessed; //如果不包含EventType，就将其加入到字典中，同时新建一个EventHandlerManager()
        if (!DictHandler.ContainsKey(EventType))
        {
            DictHandler[EventType] = new EventHandlerManager();
        } //然后将handler加入到新创建的EventHandlerManager中去。

        isSuccessed = DictHandler[EventType].AddHandler(handler);

        return isSuccessed;
    }

    public bool RemoveEventListener(Enum EventType, EventHandler handler)
    {
        if (DictHandler.ContainsKey(EventType))
        {
            DictHandler[EventType].RemoveHandler(handler);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void TriggerEvent(Enum EventType, IEvent e, bool ifNow = false)
    {
        if (ifNow)
        {
            BroadCastEvent(e);
            return;
        }

        this.eventQuene.Enqueue(e);
    }

    private void Update()
    {
        if (eventQuene.Count < 1)
        {
            return;
        }

        IEvent e = eventQuene.Dequeue();

        BroadCastEvent(e);
    }

    private void BroadCastEvent(IEvent e)
    {
        if (e == null)
        {
            return;
        }

        Enum type = e.EventType;

        if (!DictHandler.ContainsKey(type))
        {
            e.DestroySelf();
        }

        DictHandler[type].BroadCastEvent(e);
        e.DestroySelf();
    }

    public void DestroySelf()
    {
        DictHandler.Clear();
        eventQuene.Clear();
        instance = null;
    }

    private void OnDestroy()
    {
        DestroySelf();
    }
}