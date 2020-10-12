using DG.Tweening;
using UnityEngine;

public static class DOTweenUtils
{
    public static void KillTween(ref Tween tween)
    {
        tween?.Kill();
        tween = null;
    }

    public static Tween DOMoveXZ(this Transform t, Vector2 end, float duration)
    {
        Vector2 current = new Vector2(t.position.x, t.position.z);
        return DOTween.To(() => current, value => t.position = new Vector3(value.x, t.position.y, value.y), end, duration);
    }

    public static Tween DOMoveXY(this Transform t, Vector2 end, float duration)
    {
        Vector2 current = new Vector2(t.position.x, t.position.y);
        return DOTween.To(() => current, value => t.position = new Vector3(value.x, value.y, t.position.z), end, duration);
    }

    public static Tween DOMoveYZ(this Transform t, Vector2 end, float duration)
    {
        Vector2 current = new Vector2(t.position.y, t.position.z);
        return DOTween.To(() => current, value => t.position = new Vector3(t.position.x, value.x, value.y), end, duration);
    }
}