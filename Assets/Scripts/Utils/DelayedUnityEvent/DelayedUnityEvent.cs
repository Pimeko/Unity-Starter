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

    public float GetDelay()
    {
        if (delay > 0)
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
        if (delay > 0)
            DOVirtual.DelayedCall(delay, callback.Invoke);
        else
            callback.Invoke();
    }
}