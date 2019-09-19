using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameEvent
{
    void AddListener(object listener);
    void RemoveListener(object listener);
}

public abstract class GameEvent : ScriptableObject, IGameEvent
{
    [SerializeField]
    bool logOnRaise;

    protected string logMessage
    {
        get
        {
            return "[!] Game Event '" + name + "' ";
        }
    }

    protected void Log(object value = null)
    {
        if (logOnRaise)
            Debug.Log(logMessage + (value == null ? "" : "| value = " + value.ToString()));
    }

    protected List<IGameEventListener> listeners = new List<IGameEventListener>();
    protected List<IGameEventListenerWithInfo> listenersWithInfo = new List<IGameEventListenerWithInfo>();

    public void AddListener(object listener)
    {
        {
            IGameEventListener castedListener = listener as IGameEventListener;

            if (castedListener != null && !listeners.Contains(castedListener))
                listeners.Add(castedListener);
        }

        {
            IGameEventListenerWithInfo castedListener = listener as IGameEventListenerWithInfo;

            if (castedListener != null && !listenersWithInfo.Contains(castedListener))
                listenersWithInfo.Add(castedListener);
        }
    }

    public void RemoveListener(object listener)
    {
        {
            IGameEventListener castedListener = listener as IGameEventListener;

            if (castedListener != null && listeners.Contains(castedListener))
                listeners.Remove(castedListener);
        }
        {
            IGameEventListenerWithInfo castedListener = listener as IGameEventListenerWithInfo;

            if (castedListener != null && listenersWithInfo.Contains(castedListener))
                listenersWithInfo.Remove(castedListener);
        }
    }
}