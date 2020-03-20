using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PopElementController : MonoBehaviour
{
    [SerializeField]
    float delayShow = 0, delayHide = 0;
    
    Animator animator;
    Animator CurrentAnimator => transform.CachedComponent(ref animator);

    void OnEnable()
    {
        Show();
    }

    public void Show()
    {
        DOVirtual.DelayedCall(delayShow, () => CurrentAnimator.SetTrigger("show"));
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