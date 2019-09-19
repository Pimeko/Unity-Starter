using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Event/Basic")]
public class BasicGameEvent : GameEvent
{
    [Button]
    public void Raise()
    {
        Log();
        
        foreach (IGameEventListener listener in listeners.ToList())
        {
            IBasicGameEventListener listenerCasted = listener as IBasicGameEventListener;
            listenerCasted.Invoke();
        }
    }

    [Button]
    public void RaiseWithInfo(GameObject from)
    {
        Log();
        
        foreach (IGameEventListenerWithInfo listener in listenersWithInfo.ToList())
        {
            listener.Invoke(from, this);
        }
    }
}