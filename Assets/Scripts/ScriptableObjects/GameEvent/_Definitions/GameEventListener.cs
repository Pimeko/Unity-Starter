using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public interface IGameEventListener { }

public abstract class GameEventListener<T_GAME_EVENT> : MonoBehaviour, IGameEventListener
    where T_GAME_EVENT : IGameEvent
{
    [SerializeField]
    protected List<T_GAME_EVENT> gameEvents;
    
    [SerializeField]
    bool useFloatVariable;

    [ShowIf("useFloatVariable"), SerializeField]
    protected FloatVariable delayVariable;
    [HideIf("useFloatVariable"), SerializeField]
    protected float delayBeforeAction;

    protected WaitForSecondsRealtime waitForDelay;

    void OnEnable()
    {
        if (gameEvents == null)
            gameEvents = new List<T_GAME_EVENT>();
        waitForDelay = new WaitForSecondsRealtime(useFloatVariable ? delayVariable.Value : delayBeforeAction);
        Subscribe();
    }
    
    void OnDisable()
    {
        Unsubscribe();
    }

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