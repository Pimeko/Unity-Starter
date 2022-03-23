using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameObjectVariableAssigner : MonoBehaviour
{
    [SerializeField]
    GameObjectVariable variable;
    [SerializeField, InfoBox("On Enable or On Start")]
    bool assignOnEnable = false;

    void OnEnable()
    {
        if (assignOnEnable)
            variable.Value = gameObject;
    }

    void Start()
    {
        if (!assignOnEnable)
            variable.Value = gameObject;
    }
}