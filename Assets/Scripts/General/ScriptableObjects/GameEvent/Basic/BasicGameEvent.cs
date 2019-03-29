using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Event/Basic")]
public class BasicGameEvent : GameEvent
{
    public void Raise()
    {
        Log();
        foreach (IBasicGameEventListener listener in listeners)
            listener.Invoke();
    }
}