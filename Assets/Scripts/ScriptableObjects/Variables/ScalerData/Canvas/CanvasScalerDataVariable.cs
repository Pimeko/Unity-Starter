using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Variable/Scaler Data/Canvas")]
public class CanvasScalerDataVariable : SerializedScriptableObject
{
    [SerializeField]
    Dictionary<CanvasScalerType, float> value;
    public Dictionary<CanvasScalerType, float> Value { get { return value; } }

    public float GetCurrentValue()
    {
        return Value[ScalerDataUtils.GetCurrentScalerType()];
    }
}
