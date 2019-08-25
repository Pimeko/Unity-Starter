using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NativeCallback : MonoBehaviour
{
    [SerializeField]
    DelayedUnityEvent actions;

    protected void DoActions()
    {
        actions?.Invoke();
    }
}