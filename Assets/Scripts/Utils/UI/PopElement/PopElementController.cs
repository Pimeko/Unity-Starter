using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PopElementController : MonoBehaviour
{
    [SerializeField]
    float delayShow = 0, delayHide = 0;
    [SerializeField]
    bool showOnEnable = true, autoHide = false;

    Animator animator;
    Animator CurrentAnimator => transform.CachedComponent(ref animator);

    void OnEnable()
    {
        if (showOnEnable)
        {
            Show();
            if (autoHide)
                Hide();
        }
    }

    public void Show()
    {
        if (delayShow == 0)
        {
            CurrentAnimator.SetTrigger("show");
            if (autoHide)
                Hide();
        }
        else
        {
            DOVirtual.DelayedCall(delayShow, () =>
            {
                CurrentAnimator.SetTrigger("show");
                if (autoHide)
                    Hide();
            });
        }
    }

    public void Hide()
    {
        DOVirtual.DelayedCall(delayHide, () => CurrentAnimator.SetTrigger("hide"));
    }

    void OnDisable()
    {
        transform.localScale = Vector3.zero;
    }
}