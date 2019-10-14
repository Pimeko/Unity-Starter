using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DelayedUnityEvent
{
    public BetterEvent callback;
    
    public bool useUnityEvents;
    [ShowIf("useUnityEvents")]
    public UnityEvent callbackUnity;

    public bool useFloatVariable;

    [ShowIf("useFloatVariable")]
    public FloatVariable delayVariable;
    [HideIf("useFloatVariable"), MinMaxSlider(0, 10, true)]
    public Vector2 delay;
    public bool ignoreTimeScale;

    public float GetDelay()
    {
        if (!useFloatVariable)
            return Random.Range(delay.x, delay.y);
        return delayVariable.Value;
    }

    public DelayedUnityEvent(BetterEvent callback, float delay)
    {
        this.callback = callback;
        this.delay = new Vector2(delay, delay);
    }

    public DelayedUnityEvent(BetterEvent callback, Vector2 delay)
    {
        this.callback = callback;
        this.delay = delay;
    }

    public void Invoke()
    {
        if ((delay.x > 0 && delay.y > 0) || useFloatVariable)
            DOVirtual.DelayedCall(GetDelay(), () => { callback.Invoke(); callbackUnity?.Invoke(); }, ignoreTimeScale);
        else
        {
            callback.Invoke();
            callbackUnity?.Invoke();
        }
    }
}