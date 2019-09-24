using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationRandomSpeedController : MonoBehaviour
{
    [SerializeField, MinMaxSlider(0f, 10, true)]
    Vector2 range = new Vector2(.5f, 1.5f);

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