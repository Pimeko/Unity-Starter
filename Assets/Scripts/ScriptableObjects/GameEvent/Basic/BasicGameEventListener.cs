using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public interface IBasicGameEventListener
{
    void Invoke();
}

public class BasicGameEventListener: GameEventListener<BasicGameEvent>, IBasicGameEventListener
{
    [SerializeField]
    DelayedUnityEvent callbacks;
    
    void IBasicGameEventListener.Invoke()
    {
        callbacks.Invoke();
    }

    public void AddCallback(UnityAction callback)
    {
        AddCallback(callback, new Vector2(0, 0));
    }

    public void AddCallback(UnityAction callback, Vector2 delay)
    {
        if (callbacks == null)
            callbacks = new DelayedUnityEvent(new BetterEvent(), delay);
        callbacks.callback.AddCallback(callback);
    }
}