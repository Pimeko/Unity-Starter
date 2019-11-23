using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PointerInteractable))]
public class InputController : MonoBehaviour
{
    [SerializeField]
    InputVariable playerInput;
    [SerializeField]
    GameObjectVariable currentCameraGO;
    
    Camera cam;

    void Start()
    {
        playerInput.IsTouching = false;
        cam = currentCameraGO.Value.GetComponent<Camera>();
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
        inputPosition = ToVector2(cam.ScreenToViewportPoint(Input.GetTouch(0).position));
        #else
        inputPosition = ToVector2(cam.ScreenToViewportPoint(Input.mousePosition));
        #endif

        return inputPosition;
    }

    Vector3 ToVector2(Vector3 v)
    {
        return new Vector2(v.x, v.y);
    }

    void Update()
    {
        if (playerInput.IsTouching && Vector3.Distance(GetCurrentTouchPosition(), playerInput.TouchPosition) > 0)
            playerInput.TouchPosition = GetCurrentTouchPosition();
    }

    void OnDisable()
    {
        playerInput.IsTouching = false;
    }
}