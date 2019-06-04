using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Variable/Scaler Data")]
public class ScalerDataVariable : ScriptableObject
{
    public enum CanvasScalerType
    {
        IPHONE,
        IPAD
    }

    [System.Serializable]
    public class CanvasScalerData
    {
        public CanvasScalerType type;
        public float value = 0f;
    }
    [SerializeField]
	List<CanvasScalerData> data;

    public float GetCurrentMatch()
    {
        CanvasScalerType type = Screen.width > 1440 ? CanvasScalerType.IPAD : CanvasScalerType.IPHONE;
        return data.Find((CanvasScalerData data) => data.type == type).value;
    }
}
