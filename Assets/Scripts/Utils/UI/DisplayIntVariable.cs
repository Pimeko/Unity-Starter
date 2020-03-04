using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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