using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayloadedGameEvent<T> : ScriptableObject, IPayloadedGameEvent
{
    List<IGameEventListener> listeners = new List<IGameEventListener>();

    public void Raise(T value)
    {
        foreach (IGameEventListener listener in listeners)
            listener.Invoke(value);
    }

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