using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnabler : MonoBehaviour
{
    [SerializeField]
    float percentageDisabling;

    void OnEnable()
    {
        if (Random.Range(0f, 1f) < percentageDisabling)
            gameObject.SetActive(false);
    }
}