using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGenerator : MonoBehaviour
{
    [SerializeField]
    List<GameObject> prefabs;
    [SerializeField, Range(0f, 1f)]
    float percentage = 1;
    [SerializeField]
    Vector3 ranges;
    [SerializeField]
    Vector3 rotationBounds;

    void Start()
    {
        if (Random.Range(0f, 1f) > percentage)
            return;

        GameObject o = Instantiate(prefabs[Random.Range(0, prefabs.Count)]);
        o.transform.SetParent(transform);
        o.transform.localPosition = Vector3.zero;
        o.transform.localPosition += new Vector3(
            GetRandom(ranges.x),
            GetRandom(ranges.y),
            GetRandom(ranges.z)
        );
        o.transform.localRotation = Quaternion.Euler(new Vector3(
            GetRandom(rotationBounds.x),
            GetRandom(rotationBounds.y),
            GetRandom(rotationBounds.z)
        ));
    }

    float GetRandom(float x)
    {
        float absX = Mathf.Abs(x);
        return Random.Range(-absX, absX);
    }
}