using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPayloadedGameEvent
{
    void AddListener(object listener);
    void RemoveListener(object listener);
}

public abstract class GameEvent : ScriptableObject
{
    protected List<IGameEventListener> listeners = new List<IGameEventListener>();

    public void AddListener(object listener)
    {
        IGameEventListener castedListener = (IGameEventListener)listener;

        if (!listeners.Contains(castedListener))
            listeners.Add(castedListener);
    }

    public void RemoveListener(object listener)
    {
        IGameEventListener castedListener = (IGameEventListener)listener;
        
        if (listeners.Contains(castedListener))
            listeners.Remove(castedListener);
    }
}