using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DistributionPool))]
public class BasicPoolSpawner : MonoBehaviour
{
    [SerializeField]
    ObjectPoolControllerContainerVariable currentPool;

    DistributionPool distributionPool;

    void Start()
    {
        distributionPool = GetComponent<DistributionPool>();
    }

    public GameObject Spawn()
    {
        return SpawnAt(transform);
    }

    public GameObject SpawnButNotParent()
    {
        return SpawnAt(transform, false);
    }

    public GameObject SpawnAt(Transform t, bool keepParent = true)
    {
        GameObject o = currentPool.Value.GetPooledObject(distributionPool.Draw());
        RectTransform rectTransform = o.GetComponent<RectTransform>();
        Vector3 scale = rectTransform != null ? rectTransform.localScale : transform.localScale;
        o.transform.SetParent(t);
        if (rectTransform != null)
        {
            rectTransform.localPosition = Vector3.zero;
            rectTransform.localScale = scale;
            rectTransform.localRotation = Quaternion.identity;
        }
        else
        {
            o.transform.localPosition = Vector3.zero;
            o.transform.localScale = scale;
            o.transform.localRotation = Quaternion.identity;
        }
        if (!keepParent)
            o.transform.parent = null;
        o.SetActive(true);

        return o;
    }
}