using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SlowMotionController : MonoBehaviour
{
    /*
        InDuration : 1
        InEaseType : OutCirc

        FreezeDuration : .1f

        OutDuration : .5f
        OutEaseType : OutCirc
    */

    Tween currentTween;

    void Start()
    {
        currentTween = null;
    }

    public void Do(SlowMotionType type)
    {
        DOTweenUtils.KillTween(ref currentTween);

        currentTween = DOTween.Sequence()
            .Append(DOVirtual.Float(Time.timeScale, .1f, type.InDuration, newTimeScale =>
            {
                Time.timeScale = newTimeScale;
                Time.fixedDeltaTime = newTimeScale * 0.02f;
            }).SetEase(type.InEaseType))
            .AppendInterval(type.FreezeDuration)
            .Append(DOVirtual.Float(.1f, 1f, type.OutDuration, newTimeScale =>
            {
                Time.timeScale = newTimeScale;
                Time.fixedDeltaTime = newTimeScale * 0.02f;
            }).SetEase(type.OutEaseType))
            .SetUpdate(true);
    }

    public void Stop()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;
        DOTweenUtils.KillTween(ref currentTween);
    }
}