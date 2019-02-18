using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolController : MonoBehaviour
{
    [System.Serializable]
    public class ObjectPoolItem
    {
        [SerializeField]
        GameObject objectToPool;
        public GameObject ObjectToPool { get { return objectToPool ; } }
        
        [SerializeField]
        int amountToPool;
        public int AmountToPool { get { return amountToPool ; } }
        
        [SerializeField]
        ObjectPoolTypeVariable type;
        public ObjectPoolTypeVariable Type { get { return type ; } }
    }
    
    [SerializeField]
    List<ObjectPoolItem> objectPoolItems;

    List<GameObject> pooledObjects;

    void OnEnable()
    {
        pooledObjects = new List<GameObject>();

        foreach (ObjectPoolItem objectPoolItem in objectPoolItems)
        {
            GameObject parent = new GameObject();
            parent.name = objectPoolItem.Type.ToString();
            parent.transform.SetParent(transform);

            for (int i = 0; i < objectPoolItem.AmountToPool; i++)
                InstantiatePooledObject(objectPoolItem, parent.transform);
        }
    }

    GameObject InstantiatePooledObject(ObjectPoolItem objectPoolItem, Transform parent)
    {
        GameObject pooledObject = Instantiate(objectPoolItem.ObjectToPool);
        pooledObject.transform.SetParent(parent.transform);
        pooledObject.SetActive(false);
        pooledObject.name = objectPoolItem.Type.ToString();
        pooledObjects.Add(pooledObject);
        return pooledObject;
    }

    public GameObject GetPooledObject(ObjectPoolTypeVariable type)
    {
        foreach (GameObject pooledObject in pooledObjects)
        {
            if (!pooledObject.activeInHierarchy && pooledObject.name == type.ToString())
                return pooledObject;
        }
        
        // In case of expand
        ObjectPoolItem objectPoolItemToUse = null;
        foreach (ObjectPoolItem objectPoolItem in objectPoolItems)
        {
            if (objectPoolItem.Type.ToString() == type.ToString())
            {
                objectPoolItemToUse = objectPoolItem;
                break;
            }
        }
        foreach (Transform child in transform)
        {
            if (child.name == type.ToString())
                return InstantiatePooledObject(objectPoolItemToUse, child);
        }
        return null;
    }
}
