using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PopElementController : MonoBehaviour
{
    [SerializeField]
    bool showOnEnable = true, autoHide = false;
    [SerializeField, ShowIf("autoHide")]
    float durationBeforeAutoHide = 0;

    Animator animator;
    Animator CurrentAnimator => transform.CachedComponent(ref animator);

    Tween currentTween;

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

    public void Rebind()
    {
        CurrentAnimator.Rebind();
    }

    public void Show()
    {
        CurrentAnimator.SetBool("isShowing", true);
    }

    public void Hide()
    {
        CurrentAnimator.SetBool("isShowing", false);
        DOTweenUtils.KillTween(ref currentTween);
    }

    public void ShowFor(float duration)
    {
        Show();
        currentTween = DOVirtual.DelayedCall(duration, Hide);
    }

    public void HideIn(float duration)
    {
        DOTweenUtils.KillTween(ref currentTween);
        currentTween = DOVirtual.DelayedCall(duration, Hide);
    }

    void OnDisable()
    {
        transform.localScale = Vector3.zero;
    }
}