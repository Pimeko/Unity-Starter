using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionDisablerController : MonoBehaviour
{
    public Action OnDisable;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Disabler"))
            OnDisable();
    }
}