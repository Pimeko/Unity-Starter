using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsController : MonoBehaviour
{
    Animator animator;

    bool isOpen;

    void Start()
    {
        animator = GetComponent<Animator>();
        isOpen = false;
    }

    public void OpenOrClose()
    {
        animator.SetTrigger(isOpen ? "close" : "open");
        isOpen = !isOpen;
    }
}