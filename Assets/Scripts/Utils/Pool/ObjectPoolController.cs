using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPoolController : MonoBehaviour
{
    [System.Serializable]
    public class ObjectPoolItem
    {
        [SerializeField]
        GameObject objectToPool;
        public GameObject ObjectToPool { get { return objectToPool; } }

        [SerializeField]
        int amountToPool;
        public int AmountToPool { get { return amountToPool; } }

        [SerializeField]
        ObjectPoolTypeVariable type;
        public ObjectPoolTypeVariable Type { get { return type; } }
    }

    [SerializeField]
    List<ObjectPoolItem> objectPoolItems;
    [SerializeField]
    ObjectPoolControllerContainerVariable currentPool;
    [SerializeField]
    bool dontDestroyOnLoad;
    [SerializeField]
    BasicGameEvent onPoolStarted;

    Dictionary<ObjectPoolTypeVariable, List<GameObject>> pooledObjects;
    bool isUI;

    void Awake()
    {
        if (dontDestroyOnLoad)
            GameObject.DontDestroyOnLoad(gameObject);
        isUI = GetComponent<RectTransform>() != null;
    }

    void Start()
    {
        currentPool.Value = this;

        pooledObjects = new Dictionary<ObjectPoolTypeVariable, List<GameObject>>();

        foreach (ObjectPoolItem objectPoolItem in objectPoolItems)
        {
            GameObject parent = null;
            if (isUI)
                parent = new GameObject(objectPoolItem.Type.ToString(), typeof(RectTransform));
            else
                parent = new GameObject(objectPoolItem.Type.ToString());
            parent.transform.SetParent(transform);

            pooledObjects.Add(objectPoolItem.Type, new List<GameObject>());

            if (isUI)
            {
                RectTransform rectTransform = parent.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = rectTransform.transform.position;
                rectTransform.localPosition = Vector3.zero;
            }

            for (int i = 0; i < objectPoolItem.AmountToPool; i++)
                InstantiatePooledObject(objectPoolItem, parent.transform);
        }

        if (onPoolStarted != null)
            onPoolStarted.Raise();
    }

    GameObject InstantiatePooledObject(ObjectPoolItem objectPoolItem, Transform parent)
    {
        GameObject pooledObject = Instantiate(objectPoolItem.ObjectToPool);
        pooledObject.transform.SetParent(parent.transform);
        if (isUI)
        {
            RectTransform rectTransform = pooledObject.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = rectTransform.transform.position;
            rectTransform.localPosition = objectPoolItem.ObjectToPool.transform.localPosition;
            rectTransform.localScale = objectPoolItem.ObjectToPool.transform.localScale;
        }
        pooledObject.SetActive(false);
        pooledObject.name = objectPoolItem.Type.ToString();
        pooledObjects[objectPoolItem.Type].Add(pooledObject);
        return pooledObject;
    }

    public GameObject GetPooledObject(ObjectPoolTypeVariable type = null)
    {
        if (type == null)
            type = pooledObjects.Keys.First();

        foreach (GameObject pooledObject in pooledObjects[type])
        {
            if (!pooledObject.activeInHierarchy)
                return pooledObject;
        }

        // In case of expand
        ObjectPoolItem objectPoolItemToUse = null;
        foreach (ObjectPoolItem objectPoolItem in objectPoolItems)
        {
            if (type == null || objectPoolItem.Type.ToString() == type.ToString())
            {
                objectPoolItemToUse = objectPoolItem;
                break;
            }
        }
        foreach (Transform child in transform)
        {
            if (type == null || child.name == type.ToString())
                return InstantiatePooledObject(objectPoolItemToUse, child);
        }
        return null;
    }

    public List<GameObject> GetPooledObjects(int n, ObjectPoolTypeVariable type = null)
    {
        if (type == null)
            type = pooledObjects.Keys.First();

        List<GameObject> result = new List<GameObject>();
        int length = 0;

        foreach (GameObject pooledObject in pooledObjects[type])
        {
            if (!pooledObject.activeInHierarchy)
            {
                result.Add(pooledObject);
                length++;

                if (length == n)
                    return result;
            }
        }

        // In case of expand
        ObjectPoolItem objectPoolItemToUse = null;
        foreach (ObjectPoolItem objectPoolItem in objectPoolItems)
        {
            if (type == null || objectPoolItem.Type.ToString() == type.ToString())
            {
                objectPoolItemToUse = objectPoolItem;
                break;
            }
        }
        foreach (Transform child in transform)
        {
            if (type == null || child.name == type.ToString())
            {
                while (length < n)
                {
                    result.Add(InstantiatePooledObject(objectPoolItemToUse, child));
                    length++;
                }
            }
        }
        return null;
    }

    public GameObject GenerateAt(ObjectPoolTypeVariable type, Vector3 position)
    {
        if (type == null)
            throw new UnityException("No type provided for object pool's generation.");
            
        var pooledObject = GetPooledObject(type);
        pooledObject.transform.position = position;
        pooledObject.SetActive(true);
        return pooledObject;
    }
}
