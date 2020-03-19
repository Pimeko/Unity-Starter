using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopElementController : MonoBehaviour
{
    Animator animator;
    Animator CurrentAnimator => transform.CachedComponent(ref animator);

    void OnEnable()
    {
        Show();
    }

    public void Show()
    {
        CurrentAnimator.SetTrigger("show");
    }

    public void Hide()
    {
        CurrentAnimator.SetTrigger("hide");
    }
}