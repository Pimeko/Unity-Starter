using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
// 

public interface IPayloadedGameEventListener
{
    void Invoke(object value);
}

public abstract class PayloadedGameEventListener<T, T_GAME_EVENT, T_UNITY_EVENT> : GameEventListener<T_GAME_EVENT>, IPayloadedGameEventListener
    where T_GAME_EVENT : IGameEvent, IPayloadedGameEvent
    where T_UNITY_EVENT : UnityEvent<T>, new()
{
    [SerializeField]
    bool useFloatVariable;

    [ShowIf("useFloatVariable"), SerializeField]
    FloatVariable delayVariable;
    [HideIf("useFloatVariable"), SerializeField]
    float delayBeforeAction;

    WaitForSecondsRealtime waitForDelay;

    [SerializeField]
    bool ordered;

    [HideIf("ordered"), SerializeField]
    T_UNITY_EVENT actions;
    T_UNITY_EVENT Actions
    {
        get
        {
            if (actions == null)
                actions = new T_UNITY_EVENT();
            return actions;
        }
    }
    
    [ShowIf("ordered"), SerializeField]
    List<T_UNITY_EVENT> orderedActions;
    List<T_UNITY_EVENT> OrderedActions
    {
        get
        {
            if (orderedActions == null)
                orderedActions = new List<T_UNITY_EVENT>();
            return orderedActions;
        }
    }

    protected override void OnEnableChild()
    {
        waitForDelay = new WaitForSecondsRealtime(useFloatVariable ? delayVariable.Value : delayBeforeAction);
    }

    IEnumerator InvokeAfterDelay(T_UNITY_EVENT actions, object value)
	{
        yield return waitForDelay;
        actions.Invoke((T)value);
	}

    IEnumerator InvokeAfterDelay(List<T_UNITY_EVENT> actions, object value)
	{
		yield return waitForDelay;
        foreach (T_UNITY_EVENT action in actions)
            action?.Invoke((T)value);
	}

    public void AddCallback(UnityAction<T> callback)
    {
        if (ordered)
        {
            T_UNITY_EVENT e = new T_UNITY_EVENT();
            e.AddListener(callback);
            OrderedActions.Add(e);
        }
        else
            Actions.AddListener(callback);
    }

    public void Invoke(object value)
    {
        if (ordered)
            StartCoroutine(InvokeAfterDelay(OrderedActions, value));
        else
            StartCoroutine(InvokeAfterDelay(Actions, value));
    }
}