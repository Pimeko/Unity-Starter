using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSOIntModifier : DebugSOModifier<int, IntVariable>
{
    protected override void OnValueChange(string value)
    {
        variable.Value = int.Parse(value);
    }
}