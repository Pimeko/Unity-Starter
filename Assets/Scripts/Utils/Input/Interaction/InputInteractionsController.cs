using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputInteractionsController : MonoBehaviour
{
    [SerializeField]
    CameraVariable currentCamera;
    [SerializeField]
    InputVariable playerInput;
    [SerializeField]
    LayerMask interactionLayer;

    InputInteractionController currentInputInteractionController;

    bool isInteracting { get { return currentInputInteractionController != null; } }

    void Start()
    {
        playerInput.AddOnChangeCallback(OnPlayerInputChange);
        currentInputInteractionController = null;
    }

    void OnPlayerInputChange()
    {
        if (playerInput.IsTouching)
        {
            // First interaction
            bool wasInteracting = isInteracting;
            Ray ray = currentCamera.Value
                .ViewportPointToRay(new Vector3(playerInput.TouchPosition.x, playerInput.TouchPosition.y, 0));
            bool hitSomething = Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, interactionLayer);

            if (!wasInteracting)
            {
                if (hitSomething)
                {
                    currentInputInteractionController = hit.collider.transform
                        .GetComponentOrInParent<InputInteractionController>();
                    currentInputInteractionController?.OnTouch(hit.point, playerInput.TouchPosition);
                }
            }
            else
            {
                currentInputInteractionController.OnInputChange(
                    hitSomething ? (Vector3?)hit.point : null,
                    playerInput.TouchPosition);
            }
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