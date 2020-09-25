using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public abstract class DebugSOModifier<T, T_SO> : MonoBehaviour
    where T_SO : SimpleRegisterableScriptableObject<T>
{
    [SerializeField]
    TMP_Text labelText;
    [SerializeField]
    protected StringGetter stringGetter;
    [SerializeField]
    protected T_SO variable;

    void OnEnable()
    {
        stringGetter.Set(variable.Value.ToString());
        stringGetter.onValueChange += OnValueChange;
    }

    void Start()
    {
        labelText.text = variable.name;
    }

    protected abstract void OnValueChange(string value);

    void OnDestroy()
    {
        stringGetter.onValueChange -= OnValueChange;
    }
}