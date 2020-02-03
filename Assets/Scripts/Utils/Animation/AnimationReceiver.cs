using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationReceiver : MonoBehaviour
{
    public Action<string> OnAction;

    public void Invoke(string a)
    {
        OnAction?.Invoke(a);
    }

    private void OnDestroy()
    {
        OnAction = null;
    }
}