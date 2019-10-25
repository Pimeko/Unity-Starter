using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoAdsController : MonoBehaviour
{
    [SerializeField]
    BoolVariable boughtNoAds;
    
    public void OnBuyNoAds()
    {
        if (!boughtNoAds.Value)
            boughtNoAds.Value = true;
    }
}