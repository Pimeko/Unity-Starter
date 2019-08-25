using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnOffIconController : MonoBehaviour
{
    [SerializeField]
    BoolVariable value;
    [SerializeField]
    Sprite onTexture, offTexture;
    
    Image image;

    void Start()
    {
        image = GetComponent<Image>();
        UpdateImage();
    }

    public void InvertValue()
    {
        value.Value = !value.Value;
        UpdateImage();
    }

    void UpdateImage()
    {
        image.sprite = value.Value ? onTexture : offTexture;
    }
}