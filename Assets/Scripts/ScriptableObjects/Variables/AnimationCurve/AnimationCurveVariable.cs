using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Variable/Animation curve")]
public class AnimationCurveVariable : ScriptableObject
{
    [SerializeField]
    AnimationCurve curve;
    public AnimationCurve Value { get { return curve; } }

    public IEnumerator EvaluateOverTime(float duration, Action<float> onChange = null, Action onOver = null)
    {
        float t = 0;
        while (t < duration)
        {
            onChange?.Invoke(curve.Evaluate(t / duration));
            t += Time.deltaTime;
            yield return null;
        }
        onChange?.Invoke(curve.Evaluate(1));
        onOver?.Invoke();
    }
}