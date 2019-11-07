using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class QuaternionExtensions
{
    public static Quaternion Substract(this Quaternion a, Quaternion b)
    {
        return a * Quaternion.Inverse(b);
    }
}