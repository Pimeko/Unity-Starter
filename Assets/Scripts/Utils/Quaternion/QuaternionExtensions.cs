using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class QuaternionExtensions
{
    public static Quaternion LookRotation(Vector3 to)
    {
        return to != Vector3.zero ? Quaternion.LookRotation(to) : Quaternion.identity;
    }
    
    public static Quaternion Substract(this Quaternion a, Quaternion b)
    {
        return a * Quaternion.Inverse(b);
    }
}