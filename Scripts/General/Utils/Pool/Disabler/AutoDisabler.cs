using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDisabler : MonoBehaviour
{
    [SerializeField]
    float duration;

    Coroutine currentCoroutine;

    void OnEnable()
    {
        currentCoroutine = StartCoroutine(DoActionAfterDuration());
    }

    void OnDestroy()
    {
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);
    }

    IEnumerator DoActionAfterDuration()
    {
        yield return new WaitForSecondsRealtime(duration);
        gameObject.SetActive(false);

        currentCoroutine = null;
    }
}