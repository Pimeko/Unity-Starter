using DG.Tweening;

public static class DOTweenUtils
{
    public static void KillTween(ref Tween tween)
    {
        tween?.Kill();
        tween = null;
    }
}