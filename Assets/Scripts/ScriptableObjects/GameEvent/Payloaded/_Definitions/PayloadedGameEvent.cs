using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public interface IPayloadedGameEvent { }

public abstract class PayloadedGameEvent<T> : GameEvent, IPayloadedGameEvent
{
    [SerializeField]
    T testValue;

    [Button]
    public void Raise()
    {
        Raise(testValue);
    }

    public void Raise(T value)
    {
        Log(value);
        foreach (IPayloadedGameEventListener listener in listeners)
            listener.Invoke(value);
    }
}