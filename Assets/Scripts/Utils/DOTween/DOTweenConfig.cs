using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DOTweenConfig : MonoBehaviour
{
    [SerializeField]
    int tweenersCapacity = 200;
    [SerializeField]
    int sequencesCapacity = 50;

    void Start()
    {
        DOTween.SetTweensCapacity(tweenersCapacity, sequencesCapacity);
    }
}