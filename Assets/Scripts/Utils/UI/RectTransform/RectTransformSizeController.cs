using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectTransformSizeController : MonoBehaviour
{
    [SerializeField]
    RectVariable rectVariable;
    
    void OnEnable()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectVariable.Value = rectTransform.ToScreenSpace();
    }
}