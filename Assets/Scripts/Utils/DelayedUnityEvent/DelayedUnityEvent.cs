using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine.Events;

[System.Serializable]
public class DelayedUnityEvent
{
    public BetterEvent callback;
    public bool useFloatVariable;

    [ShowIf("useFloatVariable")]
    public FloatVariable delayVariable;
    [HideIf("useFloatVariable")]
    public float delay;
    public bool ignoreTimeScale;

    public float GetDelay()
    {
        if (!useFloatVariable)
            return delay;
        return delayVariable.Value;
    }

    public DelayedUnityEvent(BetterEvent callback, float delay)
    {
        this.callback = callback;
        this.delay = delay;
    }

    public void Invoke()
    {
        if (delay > 0 || useFloatVariable)
            DOVirtual.DelayedCall(GetDelay(), callback.Invoke, ignoreTimeScale);
        else
            callback.Invoke();
    }
}