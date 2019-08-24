using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputInteractionController : MonoBehaviour
{
    protected Vector3 initialTouchPosition;

    void Start()
    {
        OnStart();
    }

    protected virtual void OnStart() { }

    public virtual void Init(Vector3 initialTouchPosition)
    {
        this.initialTouchPosition = initialTouchPosition;
    }

    public abstract void OnInputChange(Vector3 touchPositionPrevious, Vector3 touchPosition);

    public abstract void OnStopTouch();
}