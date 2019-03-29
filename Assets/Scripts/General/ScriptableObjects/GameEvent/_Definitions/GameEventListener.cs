using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameEventListener<T_GAME_EVENT, T_UNITY_EVENT> : MonoBehaviour, IGameEventListener
    where T_GAME_EVENT : IPayloadedGameEvent
    where T_UNITY_EVENT : UnityEvent<T>
{
    [SerializeField]
    protected List<T_GAME_EVENT> gameEvents;
    [SerializeField]
    float delayBeforeAction;

    void OnEnable()
    {
        if (gameEvents == null)
            gameEvents = new List<T_GAME_EVENT>();
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

    protected void Invoke(T_UNITY_EVENT actions, object value)
    {
        StartCoroutine(InvokeAfterDelay(actions, value));
    }

    IEnumerator InvokeAfterDelay(T_UNITY_EVENT actions, object value)
	{
		yield return new WaitForSeconds(delayBeforeAction);
        actions.Invoke((T)value);
	}

    public abstract void AddCallback(UnityAction<T> callback);

    public abstract void Invoke(object value);
}