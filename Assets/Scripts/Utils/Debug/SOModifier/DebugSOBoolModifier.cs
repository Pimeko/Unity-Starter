using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class DebugSOBoolModifier : DebugSOModifier<bool, BoolVariable>
{
    protected override void OnValueChange(string value)
    {
        variable.Value = value.ToLower() == "true";
    }
}