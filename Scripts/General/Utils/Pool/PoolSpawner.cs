using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnityEventGameObject : UnityEvent<GameObject>
{
    GameObject value;
}

public class PoolSpawner : MonoBehaviour
{
    [Header("Pool")]
    [SerializeField]
    ObjectPoolController objectPoolController;
    [SerializeField]
    List<ObjectPoolTypeVariable> typesToPool;
    
    [Header("Time")]
    [SerializeField]
    float averageTime;
    [SerializeField]
    float variationTime;
    
    [Header("Others")]
    [SerializeField]
    int nbSpawnedInitial;
    [SerializeField]
    GameObject surfacesContainer;
    
    IEnumerator currentCoroutine;

    UnityEventGameObject onSpawn;

    void Start()
    {
        InstantiateInitialOnes();

        currentCoroutine = InstantiateAfterDuration();
        StartCoroutine(currentCoroutine);
    }

    void InstantiateInitialOnes()
    {
        for (int i = 0; i < nbSpawnedInitial; i++)
            Instantiate();
    }

    IEnumerator InstantiateAfterDuration()
    {
        float randomDuration = averageTime + Random.Range(-variationTime, variationTime);
        yield return new WaitForSeconds(randomDuration);

        Instantiate();

        currentCoroutine = InstantiateAfterDuration();
        StartCoroutine(currentCoroutine);
    }

    void Instantiate()
    {
        ObjectPoolTypeVariable objectPoolTypeVariable = typesToPool[Random.Range(0, typesToPool.Count)];
        GameObject pooledObject = objectPoolController.GetPooledObject(objectPoolTypeVariable);
        pooledObject.transform.position = GetRandomPosition();
        InvokeOnSpawn(pooledObject);
        pooledObject.SetActive(true);
    }

    Vector3 GetRandomPosition()
    {
        Transform randomSurface = surfacesContainer.transform.GetChild(Random.Range(0, surfacesContainer.transform.childCount));

        Mesh planeMesh = randomSurface.GetComponent<MeshFilter>().mesh;
        Bounds bounds = planeMesh.bounds;

        float halfX = randomSurface.localScale.x * bounds.size.x / 2;
        float halfZ = randomSurface.localScale.z * bounds.size.z / 2;

        return new Vector3(
            Random.Range(randomSurface.position.x - halfX, randomSurface.position.x + halfX), 
            0,
            Random.Range(randomSurface.position.z - halfZ, randomSurface.position.z + halfZ)
        );
    }
    
    public void AddListenerOnSpawn(UnityAction<GameObject> callback)
    {
        if (onSpawn == null)
            onSpawn = new UnityEventGameObject();
        onSpawn.AddListener(callback);
    }
    
    public void InvokeOnSpawn(GameObject spawnedObject)
    {
        if (onSpawn != null)
            onSpawn.Invoke(spawnedObject);
    }
}