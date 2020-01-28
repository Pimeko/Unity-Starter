using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreGameController : MonoBehaviour
{
    [SerializeField]
    int nbModulesToLoad;
    [SerializeField]
    DelayedUnityEvent onAllModulesLoaded;

    int nbModulesLoaded;

    public void OnModuleLoaded()
    {
        nbModulesLoaded++;
        if (nbModulesLoaded == nbModulesToLoad)
            onAllModulesLoaded.Invoke();
    }
}