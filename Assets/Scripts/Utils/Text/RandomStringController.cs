using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RandomStringController : MonoBehaviour
{
    [SerializeField]
    StringListVariable values;

    TMP_Text textMesh;

    void OnEnable()
    {
        if (textMesh == null)
            textMesh = GetComponent<TMP_Text>();

        textMesh.text = values.Value.GetRandomItem();
    }
}