using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialOffsetter : MonoBehaviour
{
    [SerializeField]
    Vector2 speed;

    MeshRenderer meshRenderer;
    MeshRenderer CurrentMeshRenderer => transform.CachedComponent(ref meshRenderer);

    LineRenderer lineRenderer;
    LineRenderer CurrentLineRenderer => transform.CachedComponent(ref lineRenderer);

    Material CurrentMaterial
    {
        get
        {
            if (CurrentMeshRenderer != null)
                return CurrentMeshRenderer.materials[0];
            
            if (CurrentLineRenderer != null)
                return CurrentLineRenderer.materials[0];
            
            return null;
        }
    }

    bool isOffsetting;

    public void Begin()
    {
        isOffsetting = true;
    }

    public void Stop()
    {
        isOffsetting = false;
    }

    void Update()
    {
        if (!isOffsetting)
            return;

        Vector2 offset = CurrentMaterial.GetTextureOffset("_MainTex");
        offset += speed * Time.deltaTime;
        CurrentMaterial.SetTextureOffset("_MainTex", offset);
    }
}