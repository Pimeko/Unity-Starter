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
    
    public void Do(SlowMotionType type)
    {
        DOTween.Sequence()
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
}