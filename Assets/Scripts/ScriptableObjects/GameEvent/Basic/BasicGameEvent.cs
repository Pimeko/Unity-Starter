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
        for (int i = 0; i < listeners.Count; i++)
        {
            IBasicGameEventListener listener = listeners[i] as IBasicGameEventListener;
            listener.Invoke();
        }
    }
}