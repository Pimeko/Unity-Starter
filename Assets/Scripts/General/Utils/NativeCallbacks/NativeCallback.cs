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

    protected void DoActions()
    {
        StartCoroutine(DoActionsAfterDuration());
    }
    
    IEnumerator DoActionsAfterDuration()
    {
        yield return new WaitForSeconds(delayBeforeActions);
        actions?.Invoke();
    }
}