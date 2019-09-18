using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class RandomTransform : MonoBehaviour
{
    [SerializeField, BoxGroup("Offsets")]
    Vector3 positionOffsets;
    [SerializeField, BoxGroup("Offsets")]
    Vector3 rotationOffsets;
    [SerializeField, BoxGroup("Offsets")]
    float scaleOffset;

    void Start()
    {
        transform.localPosition += new Vector3(
            GetRandom(positionOffsets.x),
            GetRandom(positionOffsets.y),
            GetRandom(positionOffsets.z)
        );
        transform.localRotation = Quaternion.Euler(new Vector3(
            GetRandom(rotationOffsets.x),
            GetRandom(rotationOffsets.y),
            GetRandom(rotationOffsets.z)
        ));

        float randomScale = 1 + GetRandom(scaleOffset);
        transform.localScale = new Vector3(
            randomScale,
            randomScale,
            randomScale
        );
    }

    float GetRandom(float x)
    {
        float absX = Mathf.Abs(x);
        return Random.Range(-absX, absX);
    }

    void MakeAbsolute(ref Vector3 v)
    {
        v = new Vector3(
            Mathf.Abs(v.x),
            Mathf.Abs(v.y),
            Mathf.Abs(v.z)
        );
    }

    void OnValidate()
    {
        MakeAbsolute(ref positionOffsets);
        MakeAbsolute(ref rotationOffsets);
    }
}