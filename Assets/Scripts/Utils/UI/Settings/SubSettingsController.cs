using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class SubSettingsController : MonoBehaviour
{
    [SerializeField]
    CanvasScalerDataVariable distanceBetweenEach;
    [SerializeField]
    List<RectTransform> children;
    bool isOpen;

    void Start()
    {
        isOpen = false;
    }

    [Button]
    public void Open()
    {
        for (int i = 0; i < children.Count; i++)
        {
            var child = children[i];
            child.gameObject.SetActive(true);
            child.DOMove(
                new Vector3(child.position.x, child.position.y - (i + 1) * distanceBetweenEach.GetCurrentValue(), child.position.z),
                0.3f)
                .SetEase(Ease.OutCirc);
        }
        isOpen = true;
    }

    [Button]
    public void Close()
    {
        for (int i = 0; i < children.Count; i++)
        {
            var child = children[i];
            child.DOMove(
                new Vector3(child.position.x, child.position.y + (i + 1) * distanceBetweenEach.GetCurrentValue(), child.position.z),
                0.3f)
                .SetEase(Ease.OutCirc)
                .OnComplete(() => child.gameObject.SetActive(false));
        }
        isOpen = false;
    }

    public void OpenOrClose()
    {
        if (isOpen)
            Close();
        else
            Open();
    }
}
