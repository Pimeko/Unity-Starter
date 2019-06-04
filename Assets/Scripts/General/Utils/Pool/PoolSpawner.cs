using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;

[RequireComponent(typeof(DistributionPool))]
public class PoolSpawner : MonoBehaviour
{
    [Header("Pool")]
    [SerializeField]
    ObjectPoolController objectPoolController;
    
    [Header("Initial")]
    [SerializeField]
    int nbSpawnedInitial;
    [SerializeField]
    int zDistanceBetweenInitials;
    [SerializeField]
    GameObject initialSurfacesContainer;

    [Header("Surface")]
    [SerializeField]
    bool randomPositionInsideSurface = false;
    [SerializeField]
    GameObject surfacesContainer;
    
    [Header("Time")]
    [SerializeField]
    bool beginOnStart = true;
    [SerializeField]
    bool spawnPeriodically = true;
    [SerializeField]
    float delayBeforeStart;
    [SerializeField]
    float averageTime;
    [SerializeField]
    float variationTime;
    
    Coroutine currentCoroutine;
    public Action<GameObject> OnSpawn;

    DistributionPool currentDistribution;
    DistributionPool CurrentDistribution
    {
        get
        {
            if (currentDistribution == null)
                currentDistribution = GetComponent<DistributionPool>();
            return currentDistribution;
        }
    }

    void Start()
    {
        if (beginOnStart)
            Begin();
    }

    public void Begin()
    {
        InstantiateInitialOnes();
        if (spawnPeriodically)
            currentCoroutine = StartCoroutine(StartAfterDelay());
        else
            currentCoroutine = null;
    }

    IEnumerator StartAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeStart);
        currentCoroutine = StartCoroutine(InstantiateAfterDuration());
    }

    void InstantiateInitialOnes()
    {
        for (int i = 0; i < nbSpawnedInitial; i++)
            Instantiate(initialSurfacesContainer, null, zDistanceBetweenInitials * i);
    }

    IEnumerator InstantiateAfterDuration()
    {
        float randomDuration = averageTime + UnityEngine.Random.Range(-variationTime, variationTime);
        yield return new WaitForSeconds(randomDuration);

        Instantiate(surfacesContainer);

        currentCoroutine = StartCoroutine(InstantiateAfterDuration());
    }

    void Instantiate(GameObject surfaces, ObjectPoolTypeVariable typeVariable = null, float zOffset = 0)
    {
        ObjectPoolTypeVariable currentTypeVariable = typeVariable == null ? GetRandomObjectPoolTypeVariable() : typeVariable;
        GameObject pooledObject = objectPoolController.GetPooledObject(currentTypeVariable);
        if (randomPositionInsideSurface)
            pooledObject.transform.position = GetRandomPosition(surfaces);
        else
            pooledObject.transform.position = transform.position;
        pooledObject.transform.position += new Vector3(0, 0, zOffset);
        InvokeOnSpawn(pooledObject);
        pooledObject.SetActive(true);
    }

    public void InstantiateOne(ObjectPoolTypeVariable typeVariable)
    {
        Instantiate(surfacesContainer, typeVariable);
    }

    public void Restart(bool afterDelay)
    {
        Stop();
        currentCoroutine = afterDelay ? StartCoroutine(StartAfterDelay()) :StartCoroutine(InstantiateAfterDuration());
    }

    public void Stop()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
            currentCoroutine = null;
        }
    }

    ObjectPoolTypeVariable GetRandomObjectPoolTypeVariable()
    {
        return CurrentDistribution.Draw();
    }

    Vector3 GetRandomPosition(GameObject surfaces)
    {
        Transform randomSurface = surfaces.transform.GetChild(UnityEngine.Random.Range(0, surfaces.transform.childCount));

        Mesh planeMesh = randomSurface.GetComponent<MeshFilter>().mesh;
        Bounds bounds = planeMesh.bounds;

        float halfX = randomSurface.localScale.x * bounds.size.x / 2;
        float halfY = randomSurface.localScale.y * bounds.size.y / 2;

        return new Vector3(
            UnityEngine.Random.Range(randomSurface.position.x - halfX, randomSurface.position.x + halfX), 
            UnityEngine.Random.Range(randomSurface.position.y - halfY, randomSurface.position.y + halfY),
            randomSurface.position.z
        );
    }
    
    public void InvokeOnSpawn(GameObject spawnedObject)
    {
        if (OnSpawn != null)
            OnSpawn(spawnedObject);
    }
}