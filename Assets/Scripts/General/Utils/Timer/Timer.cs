using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField]
    bool useFloatVariable;

    [ShowIf("useFloatVariable"), SerializeField]
    FloatVariable delayVariable;
    [HideIf("useFloatVariable"), SerializeField]
    float delay;
    [SerializeField]
    float deltaMax;

    [SerializeField]
    UnityEvent action;

    void OnEnable()
    {
        float deltaMaxAbs = Mathf.Abs(deltaMax);
        float currentDelay = (useFloatVariable ? delayVariable.Value : delay) + Random.Range(-deltaMaxAbs, deltaMaxAbs);
        StartCoroutine(CoroutineUtils.DoAfterDelay(() => { action?.Invoke(); }, currentDelay));
    }
}