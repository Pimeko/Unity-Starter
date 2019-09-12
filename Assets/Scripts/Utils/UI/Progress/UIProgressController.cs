using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UIProgressController : MonoBehaviour
{
    [SerializeField]
    FloatVariable progress;

    RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        progress.AddOnChangeCallback(UpdateSizeParent);
    }

    void UpdateSizeParent()
    {
        DOTween.To(
            () => rectTransform.localScale,
            newScale => rectTransform.localScale = newScale,
            new Vector3(progress.Value, rectTransform.localScale.y, rectTransform.localScale.z),
            0.2f)
            .SetEase(Ease.OutCirc);
    }

    void OnDestroy()
    {
        progress.RemoveOnChangeCallback(UpdateSizeParent);
    }
}