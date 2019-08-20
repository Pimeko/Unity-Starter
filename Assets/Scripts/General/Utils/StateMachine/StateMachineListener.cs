using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;

[System.Serializable]
public class StateCallback<T>
    where T : Enum
{
    [SerializeField]
    List<T> states;
    public List<T> States { get { return states; } }
    [SerializeField]
    UnityEvent onEnterCallback;
    public UnityEvent OnEnterCallback { get { return onEnterCallback; } }
    [SerializeField]
    float delayBeforeEnter;
    public float DelayBeforeEnter { get { return delayBeforeEnter; } }
    [SerializeField]
    UnityEvent onLeaveCallback;
    public UnityEvent OnLeaveCallback { get { return onLeaveCallback; } }
    [SerializeField]
    float delayBeforeLeave;
    public float DelayBeforeLeave { get { return delayBeforeLeave; } }
}

class EnterLeaveCallbacks
{
    public List<DelayedUnityEvent> onEnterCallbacks, onLeaveCallbacks;

    public EnterLeaveCallbacks(DelayedUnityEvent onEnterCallbacks, DelayedUnityEvent onLeaveCallbacks)
    {
        this.onEnterCallbacks =  new List<DelayedUnityEvent> { onEnterCallbacks };
        this.onLeaveCallbacks =  new List<DelayedUnityEvent> { onLeaveCallbacks };
    }
}

public class StateMachineListener<T, T_STATE_MACHINE, T_STATE_CALLBACK> : MonoBehaviour
    where T : Enum
    where T_STATE_MACHINE : StateMachine<T>
    where T_STATE_CALLBACK : StateCallback<T>
{
    [SerializeField]
    T_STATE_MACHINE stateMachine;
    [SerializeField, ReorderableList]
    List<T_STATE_CALLBACK> stateCallbacks;

    Dictionary<T, EnterLeaveCallbacks> innerStateCallbacks;

    private void Awake()
    {
        innerStateCallbacks = new Dictionary<T, EnterLeaveCallbacks>();
        foreach (T_STATE_CALLBACK stateCallback in stateCallbacks)
        {
            foreach (T state in stateCallback.States)
            {
                DelayedUnityEvent onEnterCallback = new DelayedUnityEvent(stateCallback.OnEnterCallback, stateCallback.DelayBeforeEnter);
                DelayedUnityEvent onLeaveCallback = new DelayedUnityEvent(stateCallback.OnLeaveCallback, stateCallback.DelayBeforeLeave);
                if (innerStateCallbacks.ContainsKey(state))
                {
                    innerStateCallbacks[state].onEnterCallbacks.Add(onEnterCallback);
                    innerStateCallbacks[state].onLeaveCallbacks.Add(onLeaveCallback);
                }
                else
                    innerStateCallbacks.Add(state, new EnterLeaveCallbacks(onEnterCallback, onLeaveCallback));
            }
        }

        stateMachine.onStateChange += OnStateChange;
    }

    void OnStateChange(T newState)
    {
        // Call onLeave
        if (innerStateCallbacks.ContainsKey(stateMachine.CurrentState))
        {
            foreach (var delayedEvent in innerStateCallbacks[stateMachine.CurrentState].onLeaveCallbacks)
                StartCoroutine(CoroutineUtils.DoAfterDelay(() => { delayedEvent.callback?.Invoke(); }, delayedEvent.delay));
        }

        // Call onEnter
        if (innerStateCallbacks.ContainsKey(newState))
        {
            foreach (var delayedEvent in innerStateCallbacks[newState].onEnterCallbacks)
                StartCoroutine(CoroutineUtils.DoAfterDelay(() => { delayedEvent.callback?.Invoke(); }, delayedEvent.delay));
        }
    }

    void OnDestroy()
    {
        stateMachine.onStateChange -= OnStateChange;
    }
}