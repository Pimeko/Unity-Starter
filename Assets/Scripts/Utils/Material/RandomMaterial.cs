using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMaterial : MonoBehaviour
{
    [SerializeField]
    List<Material> materials;

    void Start()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        SkinnedMeshRenderer skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        
        if (meshRenderer == null && skinnedMeshRenderer == null)
            throw new UnityException("No renderer found.");

        var material = meshRenderer != null ? meshRenderer.material : skinnedMeshRenderer.material;
        material = materials.GetRandomItem();
    }
}