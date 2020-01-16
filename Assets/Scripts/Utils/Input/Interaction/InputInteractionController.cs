using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputInteractionController : MonoBehaviour
{
    protected Vector3 initialTouchPosition;

    public virtual void Init(Vector3 initialTouchPosition)
    {
        this.initialTouchPosition = initialTouchPosition;
    }

    public virtual void OnTouch() { }
    
    public virtual void OnStopTouch() {}

    public virtual void OnInputChange(Vector3 touchPositionPrevious, Vector3 touchPosition) { }
}