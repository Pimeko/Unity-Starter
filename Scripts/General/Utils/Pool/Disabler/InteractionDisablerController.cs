using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionDisablerController : MonoBehaviour
{
    public delegate void DisableDelegate();
    public DisableDelegate OnDisable;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Disabler"))
            OnDisable();
    }
}