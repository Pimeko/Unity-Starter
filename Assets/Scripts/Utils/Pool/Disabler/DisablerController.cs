using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisablerController : MonoBehaviour
{
    List<InteractionDisablerController> interactionDisablerControllers;

    void OnEnable()
    {
        InteractionDisablerController[] interactionDisablerControllersArray = GetComponentsInChildren<InteractionDisablerController>();
        if (interactionDisablerControllers == null)
            interactionDisablerControllers = new List<InteractionDisablerController>();
        foreach (InteractionDisablerController interactionDisablerController in interactionDisablerControllersArray)
        {
            interactionDisablerControllers.Add(interactionDisablerController);
            interactionDisablerController.OnDisable += Disable;
        }
    }

    void OnDisable()
    {
        foreach (InteractionDisablerController interactionDisablerController in interactionDisablerControllers)
            interactionDisablerController.OnDisable -= Disable;
        interactionDisablerControllers.Clear();
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }
}