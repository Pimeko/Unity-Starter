using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NativeCallback : MonoBehaviour
{
    [SerializeField]
    UnityEvent actions;
    [SerializeField, Range(0, 50)]
    float delayBeforeActions;
    WaitForSeconds waitForDelay;

    private void Awake()
    {
        waitForDelay = new WaitForSeconds(delayBeforeActions);
    }

    protected void DoActions()
    {
        StartCoroutine(DoActionsAfterDuration());
    }
    
    IEnumerator DoActionsAfterDuration()
    {
        yield return waitForDelay;
        actions?.Invoke();
    }
}