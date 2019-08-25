using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationOverReceiver : MonoBehaviour
{
    [SerializeField]
    DelayedUnityEvent action;

    public Action OnOver;
    
    public void InvokeOver()
    {
        action?.Invoke();
        OnOver?.Invoke();
    }
}