using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineUtils
{
    public static IEnumerator DoAfterDelay(Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }
}