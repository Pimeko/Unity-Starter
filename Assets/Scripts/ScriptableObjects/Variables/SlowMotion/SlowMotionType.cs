using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Variable/Slow Motion Type")]
public class SlowMotionType : ScriptableObject
{
    [SerializeField, BoxGroup("In")]
    public float InDuration;
    [SerializeField, BoxGroup("In")]
    public Ease InEaseType;
    
    [SerializeField, BoxGroup("Freeze")]
    public float FreezeDuration;
    
    [SerializeField, BoxGroup("Out")]
    public float OutDuration;
    [SerializeField, BoxGroup("Out")]
    public Ease OutEaseType;
}