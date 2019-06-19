using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Variable/Input variable")]
public class InputVariable : RegisterableScriptableObject
{
    bool isTouchingDefault;
    public bool PreviousIsTouching { get; private set; }

    [SerializeField]
    bool isTouching;
    public bool IsTouching
    {
        get { return isTouching; }
        set { PreviousIsTouching = isTouching; isTouching = value; TriggerChange(); }
    }

    Vector3 touchPositionDefault;
    public Vector3 PreviousTouchPosition { get; private set; }
    [SerializeField]
    Vector3 touchPosition;
    public Vector3 TouchPosition
    {
        get { return touchPosition; }
        set { PreviousTouchPosition = touchPosition; touchPosition = value; TriggerChange(); }
    }

    protected override void OnInit()
    {
        isTouching = isTouchingDefault;
        touchPosition = touchPositionDefault;
    }
}