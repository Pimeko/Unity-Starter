using System.Collections;
using System.Collections.Generic;
using BansheeGz.BGSpline.Components;
using BansheeGz.BGSpline.Curve;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class UICurveFollower : MonoBehaviour
{
    [SerializeField]
    BGCurve curve;
    [SerializeField]
    Ease ease = Ease.OutFlash;
    [SerializeField]
    float duration = 1.5f, delayBetweenLoops = 1f;
    [SerializeField, ShowIf("boomerang")]
    float delayBetweenBoomerangs = .5f;
    [SerializeField]
    bool boomerang;

    [SerializeField]
    GameObjectVariable currentCameraGO;

    BGCcMath curveMath;
    float distanceDone, distanceTotal;

    RectTransform rectTransform;
    RectTransform CurrentRectTransform => transform.CachedComponent(ref rectTransform);

    Camera cam;

    void Start()
    {
        cam = currentCameraGO.Value.GetComponent<Camera>();

        curveMath = curve.GetComponent<BGCcMath>();
        distanceDone = 0;
        distanceTotal = curveMath.GetDistance();
        Compute();
    }

    void Compute()
    {
        if (boomerang)
        {
            DOTween.Sequence()
               .Append(DOVirtual.Float(0, distanceTotal, duration / 2, newDistance => distanceDone = newDistance).SetEase(ease))
               .AppendInterval(delayBetweenBoomerangs)
               .Append(DOVirtual.Float(distanceTotal, 0, duration / 2, newDistance => distanceDone = newDistance).SetEase(ease))
               .AppendInterval(delayBetweenLoops)
               .AppendCallback(Compute);
        }
        else
        {
            DOTween.Sequence()
               .Append(DOVirtual.Float(0, distanceTotal, duration, newDistance => distanceDone = newDistance).SetEase(ease))
               .AppendInterval(delayBetweenLoops)
               .AppendCallback(Compute);
        }
    }

    void Update()
    {
        CurrentRectTransform.position = cam.WorldToScreenPoint(curveMath.CalcPositionByDistance(distanceDone));
    }
}