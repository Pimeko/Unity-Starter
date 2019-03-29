using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGameEvent : GameEvent
{
    public void Raise()
    {
        foreach (IGameEventListener listener in listeners)
            listener.Invoke();
    }
}