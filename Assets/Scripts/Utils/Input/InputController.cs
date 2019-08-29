using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PointerInteractable))]
public class InputController : MonoBehaviour
{
    [SerializeField]
    InputVariable playerInput;

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

        #if !UNITY_EDITOR && !UNITY_ANDROID && (UNITY_ANDROID || UNITY_IOS)
        inputPosition = ToVector2(Input.GetTouch(0).position);
        #else
        inputPosition = ToVector2(Input.mousePosition);
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