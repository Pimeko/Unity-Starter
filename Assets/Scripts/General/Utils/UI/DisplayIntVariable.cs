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

    void UpdateText()
    {
        textMesh.text = prefix + variable.Value + suffix;
    }

    void OnEnable()
    {
        variable.AddOnChangeCallback(UpdateText);
    }

    void OnDisable()
    {
        variable.RemoveOnChangeCallback(UpdateText);
    }
}