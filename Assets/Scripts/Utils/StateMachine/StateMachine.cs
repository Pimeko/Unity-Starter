using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public abstract class StateMachine<T> : MonoBehaviour
    where T : Enum
{
    [SerializeField]
    T initialState;
    public Action<T> onStateChange;

    [SerializeField, ReadOnly]
    protected T currentState;
    public T CurrentState
    {
        get
        {
            return currentState;
        }
        set
        {
            if (!currentState.Equals(value))
            {
                onStateChange?.Invoke(value);
                currentState = value;
            }
        }
    }

    private void Start()
    {
        currentState = initialState;
    }

    public void ChangeState(T newState)
    {
        CurrentState = (T)newState;
    }

    public bool Is(T state)
    {
        return currentState.Equals(state);
    }
}