using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PointerInteractable))]
public class InputController : MonoBehaviour
{
    [SerializeField]
    InputVariable playerInput;
    [SerializeField]
    CameraVariable currentCamera;

    void Start()
    {
        playerInput.IsTouching = false;
    }

    public void Touch()
    {
        playerInput.TouchPosition = GetCurrentTouchPosition();
        playerInput.IsTouching = true;
    }

    public void StopTouch()
    {
        playerInput.IsTouching = false;
    }

    Vector2 GetCurrentTouchPosition()
    {
        Vector2 inputPosition;

        #if !UNITY_EDITOR
        inputPosition = ToVector2(currentCamera.Value.ScreenToViewportPoint(Input.GetTouch(0).position));
        #else
        inputPosition = ToVector2(currentCamera.Value.ScreenToViewportPoint(Input.mousePosition));
        #endif

        return inputPosition;
    }

    Vector3 ToVector2(Vector3 v)
    {
        return new Vector2(v.x, v.y);
    }

    void Update()
    {
        bool checkCount = true;
        #if !UNITY_EDITOR
        checkCount = Input.touchCount > 0;
        #endif
        if (playerInput.IsTouching && checkCount && Vector3.Distance(GetCurrentTouchPosition(), playerInput.TouchPosition) > 0)
            playerInput.TouchPosition = GetCurrentTouchPosition();
    }

    void OnDisable()
    {
        playerInput.IsTouching = false;
    }
}