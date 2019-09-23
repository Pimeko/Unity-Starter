using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnBoolVariableChange : MonoBehaviour
{
    [SerializeField]
    BoolVariable variable;
    [SerializeField]
    DelayedUnityEvent onTrue, onFalse;

    void Start()
    {
        variable.AddOnChangeCallback(Evaluate);
    }
       
    public void Evaluate()
    {
        if (variable.Value)
            onTrue.Invoke();
        else
            onFalse.Invoke();
    }

    void OnDestroy()
    {
        variable.RemoveOnChangeCallback(Evaluate);
    }
}