using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public abstract class DebugSOModifier<T, T_SO> : MonoBehaviour
    where T_SO : SimpleRegisterableScriptableObject<T>
{
    [SerializeField, FoldoutGroup("Texts")]
    TMP_Text labelText;
    [SerializeField, FoldoutGroup("Texts")]
    TMP_InputField valueText;
    [SerializeField, FoldoutGroup("Properties")]
    string label;
    [SerializeField, FoldoutGroup("Properties")]
    protected T_SO variable;

    void OnEnable()
    {
        valueText.text = variable.Value.ToString();
    }

    void Start()
    {
        ApplyLabel();
    }

    [Button]
    public void ApplyLabel()
    {
        labelText.text = label;
    }

    void Update()
    {
        if (valueText.text != "")
            ChangeValue(valueText.text);
    }
    
    // public void OnValueChanged(string value)
    // {
    //     ChangeValue(value);
    // }

    protected abstract void ChangeValue(string value);
}