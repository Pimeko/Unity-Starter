using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DistributionGameObject))]
public class BasicSpawner : MonoBehaviour
{
    DistributionGameObject distributionGameObject;
    DistributionGameObject CurrentDistributionGameObject => transform.CachedComponent(ref distributionGameObject);
    
    public void Spawn()
    {
        GameObject instance = Instantiate(CurrentDistributionGameObject.Draw());
        instance.transform.SetParent(transform);
        instance.transform.Reset();
    }
}