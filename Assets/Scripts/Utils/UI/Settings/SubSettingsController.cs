using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class SubSettingsController : MonoBehaviour
{
    [SerializeField]
    CanvasScalerDataVariable distanceBetweenEach;
    
    List<RectTransform> children;
    bool isOpen;

    void Start()
    {
        isOpen = false;

        children = GetComponent<RectTransform>().GetFirstChildren(true).Where(t => {
            ISubSettingConditionController condition = t.GetComponent<ISubSettingConditionController>();
            return condition == null || condition.ShowSetting();
        }).ToList();
    }

    [Button]
    public void Open()
    {
        for (int i = 0; i < children.Count; i++)
        {
            var child = children[i];
            child.gameObject.SetActive(true);
            // child.DOMove(
            //     new Vector3(child.position.x, child.position.y - (i + 1) * distanceBetweenEach.GetCurrentValue(), child.position.z),
            //     0.3f)
            //     .SetEase(Ease.OutCirc);
            
            child.DOLocalMove(
                new Vector3(0, -(i + 1) * distanceBetweenEach.GetCurrentValue(), 0),
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
            // child.DOMove(
            //     new Vector3(child.position.x, child.position.y + (i + 1) * distanceBetweenEach.GetCurrentValue(), child.position.z),
            //     0.3f)
            //     .SetEase(Ease.OutCirc)
            //     .OnComplete(() => child.gameObject.SetActive(false));
            
            child.DOLocalMove(
                new Vector3(0, 0, 0),
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
