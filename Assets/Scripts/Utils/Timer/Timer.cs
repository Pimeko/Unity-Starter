using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
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
    DelayedUnityEvent action;

    public void DoActionAfterDelay()
    {
        float deltaMaxAbs = Mathf.Abs(deltaMax);
        
        float currentDelay = (useFloatVariable ? delayVariable.Value : delay) + Random.Range(-deltaMaxAbs, deltaMaxAbs);
        if (currentDelay < 0)
            currentDelay = 0;

        DOVirtual.DelayedCall(currentDelay, action.Invoke);
    }
}