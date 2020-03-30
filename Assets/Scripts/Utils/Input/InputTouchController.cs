using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTouchController : MonoBehaviour
{
    [SerializeField]
    InputVariable playerInput;
    [SerializeField]
    DelayedUnityEvent onTouch, onRelease;

    bool isTouching;
    
    void Start()
    {
        isTouching = false;
        playerInput.AddOnChangeCallback(OnInput);
    }

    void OnInput()
    {
        if (playerInput.IsTouching)
        {
            if (!isTouching)
            {
                isTouching = true;
                onTouch.Invoke();
            }
        }
        else if (isTouching)
        {
            onRelease?.Invoke();
            isTouching = false;
        }
    }

    void OnDestroy()
    {
        playerInput.RemoveOnChangeCallback(OnInput);
    }
}