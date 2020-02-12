using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class StateCallbacks<T_STATE_TYPE>
{
    [SerializeField]
    DelayedUnityEvent onEnterCallbackDelayed;
    public DelayedUnityEvent OnEnterCallbackDelayed { get { return onEnterCallbackDelayed; } }
    [SerializeField]
    DelayedUnityEvent onLeaveCallbackDelayed;
    public DelayedUnityEvent OnLeaveCallbackDelayed { get { return onLeaveCallbackDelayed; } }
}

public abstract class StateTypeListener<T_STATE, T_STATE_TYPE, T_STATE_CALLBACKS> : SerializedMonoBehaviour
    where T_STATE : SimpleRegisterableScriptableObject<T_STATE_TYPE>
    where T_STATE_CALLBACKS : StateCallbacks<T_STATE_TYPE>
{
    [SerializeField]
    T_STATE state;
    [SerializeField]
    Dictionary<T_STATE_TYPE, T_STATE_CALLBACKS> stateCallbacks;

    void Start()
    {
        state.AddOnChangeCallback(OnChange);
    }

    void OnChange()
    {
        if (stateCallbacks == null)
            throw new UnityException("No state callback found on " + gameObject.GetSceneFullPath());

        if (stateCallbacks.ContainsKey(state.PreviousValue))
            stateCallbacks[state.PreviousValue].OnLeaveCallbackDelayed?.Invoke();
        if (stateCallbacks.ContainsKey(state.Value))
            stateCallbacks[state.Value].OnEnterCallbackDelayed?.Invoke();
    }

    void OnDestroy()
    {
        state.RemoveOnChangeCallback(OnChange);
    }
}