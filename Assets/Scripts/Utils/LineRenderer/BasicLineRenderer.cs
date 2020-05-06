using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BasicLineRenderer : MonoBehaviour
{
    [SerializeField]
    Transform begin, end;

    LineRenderer lineRenderer;
    LineRenderer CurrentLineRenderer => transform.CachedComponent(ref lineRenderer);

    [Button]
    public void Render()
    {
        CurrentLineRenderer.SetPositions(new Vector3[] { begin.position, end.position });
    }

    void Update()
    {
        Render();
    }
}