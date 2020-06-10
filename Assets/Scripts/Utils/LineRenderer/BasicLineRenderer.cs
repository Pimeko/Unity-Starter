using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BasicLineRenderer : MonoBehaviour
{
    [SerializeField]
    bool useVariableForBegin, useVariableForEnd;
    [SerializeField, HideIf("useVariableForBegin")]
    Transform begin;
    [SerializeField, HideIf("useVariableForEnd")]
    Transform end;
    [SerializeField, ShowIf("useVariableForBegin")]
    GameObjectVariable beginVariable;
    [SerializeField, ShowIf("useVariableForEnd")]
    GameObjectVariable endVariable;

    LineRenderer lineRenderer;
    LineRenderer CurrentLineRenderer => transform.CachedComponent(ref lineRenderer);

    [Button]
    public void Render()
    {
        var beginTransform = useVariableForBegin ? beginVariable.Value.transform : begin;
        var endTransform = useVariableForEnd ? endVariable.Value.transform : end;
        CurrentLineRenderer.SetPositions(new Vector3[] { beginTransform.position, endTransform.position });
    }

    void Update()
    {
        Render();
    }
}