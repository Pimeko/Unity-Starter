using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoolEvaluator : MonoBehaviour
{
    public enum ComparatorType
    {
        AND,
        OR
    }

    [SerializeField]
    List<BoolVariable> variables;
    [SerializeField]
    ComparatorType comparatorType;
    [SerializeField]
    DelayedUnityEvent onTrue, onFalse;
    [SerializeField]
    bool evaluateOnStart = false;

    void Start()
    {
        foreach (var variable in variables)
            variable.AddOnChangeCallback(Evaluate);
        if (evaluateOnStart)
            Evaluate();
    }
       
    public void Evaluate()
    {
        if (comparatorType == ComparatorType.AND ? EvaluateAnd() : EvaluateOr())
            onTrue.Invoke();
        else
            onFalse.Invoke();
    }

    bool EvaluateAnd()
    {
        bool res = true;

        foreach (var variable in variables)
            res &= variable.Value;

        return res;
    }

    bool EvaluateOr()
    {
        bool res = false;

        foreach (var variable in variables)
            res |= variable.Value;
            
        return res;
    }

    void OnDestroy()
    {
        foreach (var variable in variables)
            variable.RemoveOnChangeCallback(Evaluate);
    }
}