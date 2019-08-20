using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[RequireComponent(typeof(Animator))]
public class AnimationRandomSpeedController : MonoBehaviour
{
    [SerializeField, MinMaxSlider(0f, 10)]
    Vector2 range;

    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        SetRandomSpeed();
    }

    void SetRandomSpeed()
    {
        if (animator.enabled)
            animator.SetFloat("speed", Random.Range(range.x, range.y));
    }
}