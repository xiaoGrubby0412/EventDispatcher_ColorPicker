using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EventCenter : MonoBehaviour, IEventCenter
{
    private static EventCenter instance;
    private Dictionary<Enum, IEventHandlerManger> DictHandler = new Dictionary<Enum, IEventHandlerManger>();

    private Queue<IEvent> eventQuene = new Queue<IEvent>();

    public static EventCenter Instance
    {
        get
        {
            return instance;
        }
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

    public void TriggerEvent(IEvent e)
    {
        this.eventQuene.Enqueue(e);
    }


    #region 广播事件相关

    public void Update()
    {
        BroadCastEvent();
    }

    public void BroadCastEvent()
    {
        if (eventQuene.Count < 1)
        {
            return;
        }

        IEvent e = eventQuene.Dequeue();

        BroadCastEvent(e);
    }

    public void BroadCastEvent(IEvent e)
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

    #endregion

    #region 清除数据相关

    public void DestroySelf()
    {
        ClearEvenQueneAndDictHandler();
    }

    public void OnDestroy()
    {
        ClearEvenQueneAndDictHandler();
        instance = null;
    }

    public void ClearEventQuene()
    {
        eventQuene.Clear();
    }

    public void ClearEvenQueneAndDictHandler()
    {
        DictHandler.Clear();
        ClearEventQuene();
    }

    #endregion
}