using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoolVariableEvaluator : MonoBehaviour
{
    [SerializeField]
    BoolVariable variable;
    [SerializeField]
    UnityEvent onTrue, onFalse;
       
    public void Evaluate()
    {
        if (variable.Value)
            onTrue?.Invoke();
        else
            onFalse?.Invoke();
    }
}