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
        rectTransform.localScale = new Vector3(progress.Value, rectTransform.localScale.y, rectTransform.localScale.z);
    }

    void OnDestroy()
    {
        progress.RemoveOnChangeCallback(UpdateSizeParent);
    }
}