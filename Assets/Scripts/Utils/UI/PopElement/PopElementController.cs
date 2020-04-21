using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class PopElementController : MonoBehaviour
{
    [SerializeField]
    bool showOnEnable = true, autoHide = false;
    [SerializeField, ShowIf("autoHide")]
    float durationBeforeAutoHide = 0;

    Animator animator;
    Animator CurrentAnimator => transform.CachedComponent(ref animator);

    void OnEnable()
    {
        if (showOnEnable)
        {
            if (autoHide)
                ShowFor(durationBeforeAutoHide);
            else
                Show();
        }
    }

    public void Show()
    {
        CurrentAnimator.SetTrigger("show");
    }

    public void Hide()
    {
        CurrentAnimator.SetTrigger("hide");
    }

    public void ShowFor(float duration)
    {
        Show();
        DOVirtual.DelayedCall(duration, Hide);
    }

    void OnDisable()
    {
        transform.localScale = Vector3.zero;
    }
}