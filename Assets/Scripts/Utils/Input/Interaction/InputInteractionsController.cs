using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class InputInteractionsController : MonoBehaviour
{
    [SerializeField]
    InputVariable playerInput;
    [SerializeField]
    LayerMask interactionLayer;

    Camera cam;
    InputInteractionController currentInputInteractionController;

    bool isInteracting { get { return currentInputInteractionController != null; } }

    void Start()
    {
        cam = GetComponent<Camera>();

        playerInput.AddOnChangeCallback(OnPlayerInputChange);
        currentInputInteractionController = null;
    }

    void OnPlayerInputChange()
    {
        if (playerInput.IsTouching)
        {
            // First interaction
            bool wasInteracting = isInteracting;
            Ray ray = cam.ViewportPointToRay(new Vector3(playerInput.TouchPosition.x, playerInput.TouchPosition.y, 0));
            bool hitSomething = Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, interactionLayer);

            if (hitSomething)
                currentInputInteractionController = hit.collider.transform
                    .GetComponentOrInParent<InputInteractionController>();

            if (!wasInteracting)
                currentInputInteractionController.OnTouch(hit.point, playerInput.TouchPosition);
            else
                currentInputInteractionController.OnInputChange(
                    hitSomething ? (Vector3?)hit.point : null,
                    playerInput.TouchPosition);
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