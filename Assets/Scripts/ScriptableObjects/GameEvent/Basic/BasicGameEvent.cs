using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(menuName = "Event/Basic")]
public class BasicGameEvent : GameEvent
{
    [Button]
    public void Raise()
    {
        Log();
        foreach (IBasicGameEventListener listener in listeners)
            listener.Invoke();
    }
}