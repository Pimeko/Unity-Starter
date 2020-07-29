using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;
using System;

public class DisplayIntVariable : MonoBehaviour
{

    [Header("Meta")]
    [SerializeField]
    string prefix;
    [SerializeField]
    string suffix;
    [SerializeField]
    int toAdd = 0;
    [SerializeField]
    bool onlyOnStart = false;

    [Header("Variable")]
    [SerializeField]
    IntVariable variable;

    [Header("Scale")]
    [SerializeField]
    bool adjustScale = false;
    [SerializeField, ShowIf("adjustScale")]
    List<float> fontSizePerNbDigits;

    TMP_Text textMesh;

    void Awake()
    {
        textMesh = GetComponent<TMP_Text>();
    }

    void Start()
    {
        if (!onlyOnStart)
            variable.AddOnChangeCallback(UpdateText);

        UpdateText();
    }

    public void UpdateText()
    {
        textMesh.text = prefix + (variable.Value + toAdd) + suffix;

        if (adjustScale)
        {
            int nbDigits = variable.Value.ToString().Length;
            if (nbDigits < fontSizePerNbDigits.Count - 1)
                textMesh.fontSize = fontSizePerNbDigits[nbDigits - 1];
            else
                textMesh.fontSize = fontSizePerNbDigits.Last();
        }
    }

    public void SetVariable(IntVariable variable)
    {
        this.variable = variable;
    }

    void OnEnable()
    {
        UpdateText();
    }

    void OnDestroy()
    {
        if (!onlyOnStart)
            variable.RemoveOnChangeCallback(UpdateText);
    }
}