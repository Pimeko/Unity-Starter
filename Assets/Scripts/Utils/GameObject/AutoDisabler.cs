using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AutoDisabler : MonoBehaviour
{
    [SerializeField, Min(0)]
    float duration;

    void OnEnable()
    {
        DOVirtual.DelayedCall(duration, () => gameObject.SetActive(false));
    }
}