using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class SimpleRegisterableScriptableObject<T> : RegisterableScriptableObject
{
    [SerializeField, TabGroup("Basic")]
    protected T initialValue;

    [SerializeField, TabGroup("Basic")]
    protected T value;
    [SerializeField, TabGroup("Basic"), ReadOnly]
    protected T previousValue;
    [SerializeField, TabGroup("Basic")]
    bool triggerIfSameValue = false;
    [SerializeField, TabGroup("Basic")]
    bool logOnChange;

    public T Value
    {
        get
        {
            return value;
        }
        set
        {
            previousValue = this.value;
            this.value = value;

            bool equals = (previousValue != null && value != null) ? value.Equals(previousValue) : false;

            if (!equals || triggerIfSameValue)
            {
                TriggerChange();
                InternalOnChange(value);
            }
        }
    }
    public T PreviousValue { get { return previousValue; } }
    
    [Button]
    public void UpdateValue(T newValue)
    {
        Value = newValue;       
    }

    protected override void OnInit()
    {
        Value = initialValue;
    }

    protected virtual void InternalOnChange(T newValue)
    {
        if (logOnChange)
            Debug.Log(name + " changed to " + newValue);
    }
}