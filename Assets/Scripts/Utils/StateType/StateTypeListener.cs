using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class StateCallbacks<T_STATE_TYPE>
{
    [SerializeField]
    T_STATE_TYPE type;
    public T_STATE_TYPE Type { get { return type; } set { type = value; } }
    [SerializeField]
    DelayedUnityEvent onEnterCallbackDelayed;
    public DelayedUnityEvent OnEnterCallbackDelayed
    {
        get { return onEnterCallbackDelayed; }
        set { onEnterCallbackDelayed = value; }
    }
    [SerializeField]
    DelayedUnityEvent onLeaveCallbackDelayed;
    public DelayedUnityEvent OnLeaveCallbackDelayed
    {
        get { return onLeaveCallbackDelayed; }
        set { onLeaveCallbackDelayed = value; }
    }
}

public abstract class StateTypeListener<T_STATE, T_STATE_TYPE, T_STATE_CALLBACKS> : SerializedMonoBehaviour
    where T_STATE : SimpleRegisterableScriptableObject<T_STATE_TYPE>
    where T_STATE_CALLBACKS : StateCallbacks<T_STATE_TYPE>, new()
{
    [SerializeField]
    T_STATE state;
    [SerializeField]
    Dictionary<T_STATE_TYPE, T_STATE_CALLBACKS> stateCallbacks;
    [SerializeField]
    List<T_STATE_CALLBACKS> callbacks;

    void Start()
    {
        state.AddOnChangeCallback(OnChange);
    }

    [Button]
    public void DictionaryToList()
    {
        foreach (var current in stateCallbacks.Keys)
        {
            T_STATE_CALLBACKS callback = new T_STATE_CALLBACKS();
            callback.Type = current;
            callback.OnEnterCallbackDelayed = stateCallbacks[current].OnEnterCallbackDelayed;
            callback.OnLeaveCallbackDelayed = stateCallbacks[current].OnLeaveCallbackDelayed;
            callbacks.Add(callback);
        }
    }

    void OnChange()
    {
        if (callbacks == null)
            throw new UnityException("No state callback found on " + gameObject.GetSceneFullPath());

        foreach (T_STATE_CALLBACKS currentCallbacks in callbacks)
        {
            if (currentCallbacks.Type.Equals(state.PreviousValue))
                currentCallbacks.OnLeaveCallbackDelayed?.Invoke();
            if (currentCallbacks.Type.Equals(state.Value))
                currentCallbacks.OnEnterCallbackDelayed?.Invoke();
        }

        // if (stateCallbacks.ContainsKey(state.PreviousValue))
        //     stateCallbacks[state.PreviousValue].OnLeaveCallbackDelayed?.Invoke();
        // if (stateCallbacks.ContainsKey(state.Value))
        //     stateCallbacks[state.Value].OnEnterCallbackDelayed?.Invoke();
    }

    void OnDestroy()
    {
        state.RemoveOnChangeCallback(OnChange);
    }
}