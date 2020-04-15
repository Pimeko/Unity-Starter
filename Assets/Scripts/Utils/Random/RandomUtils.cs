using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RandomUtils
{
    public static bool Two()
    {
        return Random.Range(0f, 1f) < .5f;
    }

    public static Vector3 RandomPositionOnQuad(Transform quad)
    {
        var halfWidth = quad.lossyScale.x / 2;
        var halfHeight = quad.lossyScale.y / 2;

        return new Vector3(
            quad.position.x + Random.Range(-halfWidth, halfWidth),
            quad.position.y,
            quad.position.z + Random.Range(-halfHeight, halfHeight)
        );
    }
}