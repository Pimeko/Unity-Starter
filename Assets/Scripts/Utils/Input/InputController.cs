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

    Vector3 GetCurrentTouchPosition()
    {
        Vector3 inputPosition;

        #if !UNITY_EDITOR && !UNITY_ANDROID && (UNITY_ANDROID || UNITY_IOS)
        inputPosition = ToXZVector3(Input.GetTouch(0).position);
        #else
        inputPosition = ToXZVector3(Input.mousePosition);
        #endif

        return inputPosition;
    }

    Vector3 ToXZVector3(Vector3 v)
    {
        return new Vector3(v.x, 0, v.y);
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