using DG.Tweening;
using UnityEngine.Events;

[System.Serializable]
public class DelayedUnityEvent
{
    public UnityEvent callback;
    public float delay;

    public DelayedUnityEvent(UnityEvent callback, float delay)
    {
        this.callback = callback;
        this.delay = delay;
    }

    public void Invoke()
    {
        if (delay > 0)
            DOVirtual.DelayedCall(delay, () => { callback?.Invoke(); });
        else
            callback?.Invoke();
    }
}