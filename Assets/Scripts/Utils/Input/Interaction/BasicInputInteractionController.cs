using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicInputInteractionController : InputInteractionController
{
    [SerializeField]
    DelayedUnityEvent onTouchAction, onStopTouchAction;

    public override void OnTouch(Vector3 hitPosition, Vector3 touchPosition)
    {
        onTouchAction.Invoke();
    }
    
    public override void OnStopTouch()
    {
        onStopTouchAction.Invoke();
    }
}