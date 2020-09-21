using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Variable/Spring Values")]
public class SpringValuesVariable : ScriptableObject
{
    [SerializeField]
    float frequencyHz;
    public float FrequencyHz => frequencyHz;

    [SerializeField]
    float halfLife;
    public float HalfLife => halfLife;

    [SerializeField]
    float deltaTimeMultiplier;
    public float DeltaTimeMultiplier => deltaTimeMultiplier;
}