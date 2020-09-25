using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StringGetterToggle : StringGetter
{
    [SerializeField]
    Toggle inputField;

    void Start()
    {
        inputField.onValueChanged.AddListener(OnInputFieldChange);
    }

    void OnInputFieldChange(bool value)
    {
        Set(value.ToString());
    }

    public override void Set(string newValue)
    {
        inputField.isOn = newValue.ToLower() == "true";
        value = newValue;
        onValueChange?.Invoke(value);
    }
    
    void OnDestroy()
    {
        inputField.onValueChanged.RemoveListener(OnInputFieldChange);
    }
}