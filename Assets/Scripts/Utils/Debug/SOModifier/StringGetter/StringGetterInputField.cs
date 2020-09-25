using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StringGetterInputField : StringGetter
{
    [SerializeField]
    TMP_InputField inputField;

    public override void Set(string newValue)
    {
        inputField.text = newValue;
        value = newValue;
        onValueChange?.Invoke(value);
    }

    void Update()
    {
        if (inputField.text != value)
        {
            Set(inputField.text.ToString());
        }
    }
}