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
    [SerializeField]
    Vector3 scaleOffsets = Vector3.zero;

    void Start()
    {
        if (Random.Range(0f, 1f) > percentage)
            return;

        GameObject o = Instantiate(prefabs.GetRandomItem());
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
        o.transform.localScale = new Vector3(
            GetRandomOffset(scaleOffsets.x),
            GetRandomOffset(scaleOffsets.y),
            GetRandomOffset(scaleOffsets.z)
        );
    }

    float GetRandom(float x)
    {
        float absX = Mathf.Abs(x);
        return Random.Range(-absX, absX);
    }

    float GetRandomOffset(float offset)
    {
        float absOffset = Mathf.Abs(offset);
        return Random.Range(1 - absOffset, 1 + absOffset);
    }
}