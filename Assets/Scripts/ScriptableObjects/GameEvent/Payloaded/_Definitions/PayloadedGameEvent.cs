using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


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