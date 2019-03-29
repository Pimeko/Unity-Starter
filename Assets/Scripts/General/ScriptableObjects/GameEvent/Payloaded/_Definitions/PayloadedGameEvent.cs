using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPayloadedGameEvent { }

public abstract class PayloadedGameEvent<T> : GameEvent, IPayloadedGameEvent
{
    public void Raise(T value)
    {
        Log(value);
        foreach (IPayloadedGameEventListener listener in listeners)
            listener.Invoke(value);
    }
}