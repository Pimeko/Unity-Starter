using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputInteractionController : MonoBehaviour
{
    protected Vector3 initialTouchPosition;

    // First touch
    public virtual void OnTouch(Vector3 hitPosition, Vector3 touchPosition) { }

    // Touches after first
    public virtual void OnInputChange(Vector3? hitPosition, Vector3 touchPosition) { }

    // Stop touching
    public virtual void OnStopTouch() {}
}