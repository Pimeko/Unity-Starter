using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnVariableChange : MonoBehaviour
{
    [SerializeField]
    RegisterableScriptableObject variable;
    [SerializeField]
    DelayedUnityEvent callback;

    void Start()
    {
        variable.AddOnChangeCallback(Invoke);
    }

    void Invoke()
    {
        callback?.Invoke();
    }

    void OnDestroy()
    {
        variable.RemoveOnChangeCallback(Invoke);
    }
}