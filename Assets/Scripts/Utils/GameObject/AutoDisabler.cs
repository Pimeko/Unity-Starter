using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AutoDisabler : MonoBehaviour
{
    [SerializeField, Min(0)]
    float duration;

    Tween tween;

    void OnEnable()
    {
        DOTweenUtils.KillTween(ref tween);
        tween = DOVirtual.DelayedCall(duration, () => gameObject.SetActive(false));
    }

    void OnDisable()
    {
        DOTweenUtils.KillTween(ref tween);
        gameObject.SetActive(false);
    }
}