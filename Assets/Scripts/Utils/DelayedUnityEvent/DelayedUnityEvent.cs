using DG.Tweening;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DelayedUnityEvent
{
    [OdinSerialize]
    public UnityEvent callbackUnity;

    [OdinSerialize]
    public bool useFloatVariable;

    [OdinSerialize, ShowIf("useFloatVariable")]
    public FloatVariable delayVariable;
    [OdinSerialize, HideIf("useFloatVariable"), MinMaxSlider(0, 10, true)]
    public Vector2 delay;
    [OdinSerialize]
    public bool ignoreTimeScale;

    Tween currentTween;

    public float GetDelay()
    {
        if (!useFloatVariable)
            return Random.Range(delay.x, delay.y);
        return delayVariable.Value;
    }

    public DelayedUnityEvent(UnityEvent callback, float delay)
    {
        this.callbackUnity = callback;
        this.delay = new Vector2(delay, delay);
    }

    public DelayedUnityEvent(UnityEvent callback, Vector2 delay)
    {
        this.callbackUnity = callback;
        this.delay = delay;
    }

    void InternalInvoke()
    {
        callbackUnity?.Invoke();
    }

    public void Invoke()
    {
        if ((delay.x > 0 && delay.y > 0) || useFloatVariable)
            currentTween = DOVirtual.DelayedCall(GetDelay(), InternalInvoke, ignoreTimeScale);
        else
            InternalInvoke();
    }

    public void Stop()
    {
        DOTweenUtils.KillTween(ref currentTween);
    }
}