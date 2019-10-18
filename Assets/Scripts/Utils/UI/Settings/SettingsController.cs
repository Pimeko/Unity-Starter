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
        var newValue = !animator.GetBool("isOpen");
        animator.SetBool("isOpen", newValue);
        isOpen = newValue;
    }

    public void Close()
    {
        animator.SetBool("isOpen", false);
        isOpen = false;
    }
}