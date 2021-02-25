using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[System.Serializable]
public class CameraScalerData
{
    public bool useFov = true, usePosition = true;
    [ShowIf("useFov")]
    public float fov;
    [ShowIf("usePosition")]
    public Vector3 offsetPosition;
}

[CreateAssetMenu(menuName = "Variable/Scaler Data/Camera")]
public class CameraScalerDataVariable : SerializedScriptableObject
{
    [SerializeField]
    Dictionary<CanvasScalerType, CameraScalerData> value;
    public Dictionary<CanvasScalerType, CameraScalerData> Value { get { return value; } }
}
