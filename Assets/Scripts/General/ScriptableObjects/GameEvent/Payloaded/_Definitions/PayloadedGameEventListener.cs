using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IPayloadedGameEventListener
{
    void Invoke(object value);
}

public abstract class PayloadedGameEventListener<T, T_GAME_EVENT, T_UNITY_EVENT> : GameEventListener<T_GAME_EVENT>, IPayloadedGameEventListener
    where T_GAME_EVENT : IGameEvent, IPayloadedGameEvent
    where T_UNITY_EVENT : UnityEvent<T>
{
    [SerializeField]
    T_UNITY_EVENT actions;
    T_UNITY_EVENT Actions
    {
        get
        {
            if (actions == null)
                actions = (T_UNITY_EVENT)Activator.CreateInstance(typeof(T_UNITY_EVENT));
            return actions;
        }
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

    public void AddCallback(UnityAction<T> callback)
    {
        Actions.AddListener(callback);
    }

    public void Invoke(object value)
    {
        Invoke(Actions, value);
    }
}