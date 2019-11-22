using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubSettingConditionBoolVariableController : MonoBehaviour, ISubSettingConditionController
{
    [SerializeField]
    BoolVariable condition;
    [SerializeField]
    bool negate;

    public bool ShowSetting()
    {
        return negate ? !condition.Value : condition.Value;
    }
}