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

    TextMeshProUGUI textMesh;

    void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        textMesh.text = prefix + (variable.Value + toAdd) + suffix;
    }

    void OnEnable()
    {
        if (!onlyOnStart)
            variable.AddOnChangeCallback(UpdateText);
    }

    void OnDisable()
    {
        if (!onlyOnStart)
            variable.RemoveOnChangeCallback(UpdateText);
    }
}