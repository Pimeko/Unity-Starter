using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformUtils
{
    public static float Distance(float a, float b)
    {
        return Mathf.Abs(a - b);
    }
    

    public static Vector3 IntersectionVectorPlane(Vector3 origin, Vector3 direction, Vector3 planeNormal, Vector3 planePoint)
    {
        if (planeNormal.x * direction.x + planeNormal.y * direction.y + planeNormal.z * direction.z == 0)
            throw new UnityException("No point found, vector is parallel to plane");

        float t = (planeNormal.x * (-origin.x + planePoint.x) + planeNormal.y * (-origin.y + planePoint.y) + planeNormal.z * (-origin.z + planePoint.z))
            / (planeNormal.x * direction.x + planeNormal.y * direction.y + planeNormal.z * direction.z);
        return new Vector3(origin.x + direction.x * t, origin.y + direction.y * t, origin.z + direction.z * t);
    }
}