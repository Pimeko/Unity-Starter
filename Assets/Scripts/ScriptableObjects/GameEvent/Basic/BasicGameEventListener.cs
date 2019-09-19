using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public interface IBasicGameEventListener
{
    void Invoke();
    void Invoke(GameObject from, GameEvent e);
}

public class BasicGameEventListener: GameEventListener<BasicGameEvent>, IBasicGameEventListener
{
    [SerializeField]
    DelayedUnityEvent callbacks;
    
    void IBasicGameEventListener.Invoke()
    {
        callbacks.Invoke();
    }

    public void AddCallback(UnityAction callback, float delay = 0)
    {
        if (callbacks == null)
            callbacks = new DelayedUnityEvent(new BetterEvent(), delay);
        callbacks.callback.AddCallback(callback);
    }

    public void Invoke(GameObject from, GameEvent e)
    {
    }
}