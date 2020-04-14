using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RandomUtils
{
    public static bool Two()
    {
        return Random.Range(0f, 1f) < .5f;
    }
}