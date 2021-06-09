using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationListener : MonoBehaviour
{
    [SerializeField]
    AnimationReceiver animationReceiver;
    [SerializeField]
    SerializedAnimationReceiver actions;

    void Start()
    {
        animationReceiver.OnAction += OnAction;
    }

    void OnAction(string action)
    {
        (actions[action])?.onAction?.Invoke();
    }

    void OnDestroy()
    {
        animationReceiver.OnAction -= OnAction;
    }
}