using System;
using System.Collections.Generic;

public class EventHandlerManager : IEventHandlerManger, IDestroy
{
    private Enum _EventType;

    private List<EventHandler> lstHandler = new List<EventHandler>();

    public Enum EventType
    {
        get { return _EventType; }
    }

    public bool AddHandler(EventHandler eventHandler)
    {
        if (lstHandler.Contains(eventHandler))
        {
            return false;
        }

        lstHandler.Add(eventHandler);
        return true;
    }

    public bool RemoveHandler(EventHandler eventHandler)
    {
        if (!lstHandler.Contains(eventHandler))
        {
            return false;
        }

        lstHandler.Remove(eventHandler);
        return true;
    }

    public void BroadCastEvent(IEvent evt)
    {
        List<EventHandler> list = new List<EventHandler>();
        list.AddRange(lstHandler);

        foreach (EventHandler item in list)
        {
            item(evt);
        }
    }


    public void Clear()
    {
        lstHandler.Clear();
    }

    public void DestroySelf()
    {
        if (lstHandler != null)
        {
            Clear();
            lstHandler = null;
        }
    }
}