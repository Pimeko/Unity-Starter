using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RandomStringController : MonoBehaviour
{
    [SerializeField]
    StringListVariable values;
    
    TextMeshProUGUI textMesh;

    void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        textMesh.text = values.Value.GetRandomItem();
    }
}