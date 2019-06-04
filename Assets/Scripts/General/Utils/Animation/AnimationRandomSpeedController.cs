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

    void Start()
    {
        animator = GetComponent<Animator>();
        SetRandomSpeed();
    }

    void SetRandomSpeed()
    {
        animator.SetFloat("speed", Random.Range(range.x, range.y));
    }
}