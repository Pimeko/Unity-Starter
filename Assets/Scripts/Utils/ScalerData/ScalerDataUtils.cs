using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CanvasScalerType
{
    IPHONE_5x5,
    IPHONE_6x5,
    IPAD,
}

public class ScalerDataUtils
{
    public static CanvasScalerType GetCurrentScalerType()
    {
        var screenRatio = (1f * Screen.height) / (1f * Screen.width);

        if (screenRatio < 1.7f)
            return CanvasScalerType.IPAD;
        if (1.7f < screenRatio && screenRatio < 1.8f) // 16:9 iPhones - models 5, SE, up to 8+
            return CanvasScalerType.IPHONE_5x5;
        else
            return CanvasScalerType.IPHONE_6x5;
    }
}