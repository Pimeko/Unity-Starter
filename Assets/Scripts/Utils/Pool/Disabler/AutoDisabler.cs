using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDisabler : MonoBehaviour
{
    [SerializeField]
    float duration;

    Coroutine currentCoroutine;
    WaitForSecondsRealtime waitForDelay;

    void OnEnable()
    {
        waitForDelay = new WaitForSecondsRealtime(duration);
        currentCoroutine = StartCoroutine(DoActionAfterDuration());
    }

    void OnDestroy()
    {
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);
    }

    IEnumerator DoActionAfterDuration()
    {
        yield return waitForDelay;
        gameObject.SetActive(false);

        currentCoroutine = null;
    }
}