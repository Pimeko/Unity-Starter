using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class SimpleRegisterableScriptableObject<T> : RegisterableScriptableObject
{
    [SerializeField]
    protected T initialValue;

    [SerializeField]
    protected T value;
    [SerializeField, ReadOnly]
    protected T previousValue;
    [SerializeField]
    bool triggerIfSameValue = false;
    [SerializeField]
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