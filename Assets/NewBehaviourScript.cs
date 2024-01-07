using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public Button btnSend;

    public Button btnRemove;

    public class DataShowHello : EventBase
    {
        public DataShowHello(Enum eventType) : base(eventType)
        {
        }
    }

    public class DataShowError : EventBase
    {
        public DataShowError(Enum eventType) : base(eventType)
        {
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        btnSend.onClick.AddListener(BtnOnClick);
        btnRemove.onClick.AddListener(BtnRemoveOnClick);
        EventCenter.Instance.AddEventListener(EventType.ShowHello, ShowHello);
        EventCenter.Instance.AddEventListener(EventType.ShowHello, ShowHello);
        EventCenter.Instance.AddEventListener(EventType.ShowHello, ShowHello1);
        EventCenter.Instance.AddEventListener(EventType.ShoeError, ShowError);
    }

    private void BtnOnClick()
    {
        DataShowHello dataHello = new DataShowHello(EventType.ShowHello);
        EventCenter.Instance.TriggerEvent(EventType.ShowHello, dataHello);

        DataShowError dataError = new DataShowError(EventType.ShoeError);
        EventCenter.Instance.TriggerEvent(EventType.ShoeError, dataError);
        
        DataShowError dataError1 = new DataShowError(EventType.ShoeError);
        EventCenter.Instance.TriggerEvent(EventType.ShoeError, dataError, true);
    }

    private void BtnRemoveOnClick()
    {
        EventCenter.Instance.RemoveEventListener(EventType.ShowHello, ShowHello);
    }

    private void ShowError(IEvent evt)
    {
        Debug.Log("in ShowError");
    }

    private void ShowHello(IEvent evt)
    {
        Debug.Log("in ShowHello");
    }

    private void ShowHello1(IEvent evt)
    {
        Debug.Log("in ShowHello1");
    }

    // Update is called once per frame
    void Update()
    {
    }
}