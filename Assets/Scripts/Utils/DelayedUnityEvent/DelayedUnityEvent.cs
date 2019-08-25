using DG.Tweening;
using UnityEngine.Events;

[System.Serializable]
public class DelayedUnityEvent
{
    public BetterEvent callback;
    public float delay;

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