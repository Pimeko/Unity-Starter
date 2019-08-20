using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class InputInteractionsController : MonoBehaviour
{
    [SerializeField]
    InputVariable playerInput;

    Camera cam;
	int layerHitbox;
    InputInteractionController currentInputInteractionController;

    bool isInteracting { get { return currentInputInteractionController != null; } }

    void Start()
    {
        cam = GetComponent<Camera>();

		layerHitbox = LayerMask.GetMask("Interaction");
        playerInput.AddOnChangeCallback(OnPlayerInputChange);
        currentInputInteractionController = null;
    }

    void OnPlayerInputChange()
    {
        if (playerInput.IsTouching)
        {
            if (!isInteracting)
            {
                RaycastHit hit;
                Ray ray = cam.ScreenPointToRay(new Vector3(playerInput.TouchPosition.x, playerInput.TouchPosition.z, 0));
                bool hitSomething = Physics.Raycast(ray, out hit, Mathf.Infinity, layerHitbox);
                
                if (hitSomething)
                    currentInputInteractionController = hit.collider.transform.GetComponentOrInParent<InputInteractionController>();
                    
                if (isInteracting)
                    currentInputInteractionController.Init(playerInput.TouchPosition);
            }
            if (isInteracting)
                currentInputInteractionController.OnInputChange(playerInput.PreviousTouchPosition, playerInput.TouchPosition);
        }
        else if (isInteracting)
        {
            if (currentInputInteractionController != null)
            {
                currentInputInteractionController.OnStopTouch();
                currentInputInteractionController = null;
            }
        }
    }

    private void OnDestroy()
    {
        playerInput.RemoveOnChangeCallback(OnPlayerInputChange);
    }
}