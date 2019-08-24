using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class StateCallbacks<T_STATE_TYPE>
{
    [SerializeField]
    T_STATE_TYPE type;
    public T_STATE_TYPE Type { get { return type; } }
    [SerializeField]
    DelayedUnityEvent onEnterCallbackDelayed;
    public DelayedUnityEvent OnEnterCallbackDelayed { get { return onEnterCallbackDelayed; } }
    [SerializeField]
    DelayedUnityEvent onLeaveCallbackDelayed;
    public DelayedUnityEvent OnLeaveCallbackDelayed { get { return onLeaveCallbackDelayed; } }
}

public class StateTypeListener<T_STATE, T_STATE_TYPE, T_STATE_CALLBACKS> : MonoBehaviour
    where T_STATE : SimpleRegisterableScriptableObject<T_STATE_TYPE>
    where T_STATE_CALLBACKS : StateCallbacks<T_STATE_TYPE>
{
    [SerializeField]
    T_STATE state;
    [SerializeField]
    List<T_STATE_CALLBACKS> stateCallbacks;

    void Start()
    {
        state.AddOnChangeCallback(OnChange);
    }

    void OnChange()
    {
        stateCallbacks.Find(stateCallback => stateCallback.Type.Equals(state.PreviousValue))?.OnLeaveCallbackDelayed?.Invoke();
        stateCallbacks.Find(stateCallback => stateCallback.Type.Equals(state.Value))?.OnEnterCallbackDelayed?.Invoke();
    }

    void OnDestroy()
    {
        state.RemoveOnChangeCallback(OnChange);
    }
}