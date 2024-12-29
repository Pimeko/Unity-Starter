using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AutoEnabler : MonoBehaviour
{
    [SerializeField, Min(0)]
    float delay;
    [SerializeField]
    GameObject toEnable;

    Tween tween;

    void OnEnable()
    {
        DOTweenUtils.KillTween(ref tween);
        tween = DOVirtual.DelayedCall(delay, () => toEnable.SetActive(true));
    }
}