using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomListEnabler : MonoBehaviour
{
    [SerializeField]
    List<GameObject> objects;

    GameObject currentActivated;
    
    void Start()
    {
        objects.ForEach(o => o.SetActive(false));

        currentActivated = objects.GetRandomItem();
        currentActivated.SetActive(true);
    }

    public void Change()
    {
        currentActivated.SetActive(false);
        currentActivated = objects.GetRandomItem();
        currentActivated.SetActive(true);
    }
}