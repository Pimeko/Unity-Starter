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
        set { if (value == isTouching) return; PreviousIsTouching = isTouching; isTouching = value; TriggerChange(); }
    }

    Vector2 touchPositionDefault;
    public Vector2 PreviousTouchPosition { get; private set; }
    [SerializeField]
    Vector2 touchPosition;
    public Vector2 TouchPosition
    {
        get { return touchPosition; }
        set { if (value == touchPosition) return; PreviousTouchPosition = touchPosition; touchPosition = value; TriggerChange(); }
    }

    protected override void OnInit()
    {
        isTouching = isTouchingDefault;
        touchPosition = touchPositionDefault;
    }
}