using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicInputInteractionController : InputInteractionController
{
    [SerializeField]
    DelayedUnityEvent onTouchAction, onStopTouchAction;

    public override void OnTouch()
    {
        onTouchAction.Invoke();
    }
    
    public override void OnStopTouch()
    {
        onStopTouchAction.Invoke();
    }
}