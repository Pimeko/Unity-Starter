using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Basic class to reference transforms and have access to it in the inspector rapidly
public class TransformHolder : MonoBehaviour
{
    [SerializeField]
    List<Transform> transforms;
}