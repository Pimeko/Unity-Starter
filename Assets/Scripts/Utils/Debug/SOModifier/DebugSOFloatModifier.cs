using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class DebugSOFloatModifier : DebugSOModifier<float, FloatVariable>
{
    protected override void OnValueChange(string value)
    {
        CultureInfo ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
        ci.NumberFormat.CurrencyDecimalSeparator = ".";
        print("yeah " + (float)double.Parse(value, NumberStyles.Any, ci));
        variable.Value = (float)double.Parse(value, NumberStyles.Any, ci);
    }
}