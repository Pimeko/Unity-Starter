using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationRandomSpeedController : MonoBehaviour
{
    [System.Serializable]
    public class AnimationSpeed
    {
        public string parameter;
        [SerializeField, MinMaxSlider(0f, 10, true)]
        public Vector2 range; 
    }

    [SerializeField]
    List<AnimationSpeed> animationSpeeds;

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
        {
            animationSpeeds.ForEach(animationSpeed => {
                animator.SetFloat(
                    animationSpeed.parameter,
                    Random.Range(animationSpeed.range.x, animationSpeed.range.y));
            });
        }
    }
}