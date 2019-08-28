using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CanvasScalerType
{
    IPHONE_5x5,
    IPHONE_6x5,
    IPAD
}

public class ScalerDataUtils
{
    public static CanvasScalerType GetCurrentScalerType()
    {
        return Screen.width > 1242 ? CanvasScalerType.IPAD : (Screen.height > 2208 ? CanvasScalerType.IPHONE_6x5 : CanvasScalerType.IPHONE_5x5);
    }
}