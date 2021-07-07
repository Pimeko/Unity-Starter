using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class RandomTransform : MonoBehaviour
{
    [SerializeField, BoxGroup("Offsets")]
    bool randomPosition = true, randomRotation = true, randomScale = true;
    [SerializeField, BoxGroup("Offsets"), ShowIf("randomPosition")]
    Vector3 positionOffsets;
    [SerializeField, BoxGroup("Offsets"), ShowIf("randomRotation")]
    Vector3 rotationOffsets;
    [SerializeField, BoxGroup("Offsets"), ShowIf("randomScale")]
    bool uniformScale = true;
    [SerializeField, BoxGroup("Offsets"), ShowIf("uniformScale"), ShowIf("randomScale")]
    float minScale = 1, maxScale = 1;
    [SerializeField, BoxGroup("Offsets"), HideIf("uniformScale"), ShowIf("randomScale")]
    Vector3 minScaleVec = Vector3.one, maxScaleVec = Vector3.one;

    void Start()
    {
        Apply();
    }

    [Button]
    public void Apply()
    {
        if (randomPosition)
        {
            transform.localPosition += new Vector3(
                GetRandom(positionOffsets.x),
                GetRandom(positionOffsets.y),
                GetRandom(positionOffsets.z)
            );
        }

        if (randomRotation)
        {
            transform.localRotation = Quaternion.Euler(new Vector3(
                GetRandom(rotationOffsets.x),
                GetRandom(rotationOffsets.y),
                GetRandom(rotationOffsets.z)
            ));
        }

        if (randomScale)
        {
            if (uniformScale)
            {
                float randomScale = Random.Range(minScale, maxScale);
                transform.localScale = new Vector3(
                    randomScale,
                    randomScale,
                    randomScale
                );
            }
            else
            {
                transform.localScale = new Vector3(
                    Random.Range(minScaleVec.x, maxScaleVec.x),
                    Random.Range(minScaleVec.y, maxScaleVec.y),
                    Random.Range(minScaleVec.z, maxScaleVec.z)
                );
            }
        }
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