using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public interface IGameEventListener { }

public abstract class GameEventListener<T_GAME_EVENT> : SerializedMonoBehaviour, IGameEventListener
    where T_GAME_EVENT : IGameEvent
{
    [SerializeField]
    protected List<T_GAME_EVENT> gameEvents;

    void OnEnable()
    {
        OnEnableChild();
        if (gameEvents == null)
            gameEvents = new List<T_GAME_EVENT>();
        Subscribe();
    }
    
    void OnDisable()
    {
        Unsubscribe();
    }

    protected virtual void OnEnableChild() {}

    protected void Subscribe()
    {
		foreach (T_GAME_EVENT gameEvent in gameEvents)
            gameEvent.AddListener(this);
    }

    protected void Unsubscribe()
    {
		foreach (T_GAME_EVENT gameEvent in gameEvents)
            gameEvent.RemoveListener(this);
    }

    public void AddGameEvent(T_GAME_EVENT gameEvent)
    {
        gameEvents.Add(gameEvent);
        gameEvent.AddListener(this);
    }

    public void RemoveGameEvent(T_GAME_EVENT gameEvent)
    {
        gameEvents.Remove(gameEvent);
        gameEvent.RemoveListener(this);
    }
}