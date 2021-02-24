using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class RandomMaterial : MonoBehaviour
{
    [SerializeField]
    List<Material> materials;
    [SerializeField]
    bool useSpecificIndex;
    [SerializeField, ShowIf("useSpecificIndex")]
    int index;

    MeshRenderer meshRenderer;
    SkinnedMeshRenderer skinnedMeshRenderer;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();

        if (meshRenderer == null && skinnedMeshRenderer == null)
            throw new UnityException("No renderer found.");

        Apply();
    }

    public void Apply()
    {
        if (meshRenderer != null)
        {
            List<Material> newMaterials = new List<Material>(meshRenderer.materials);
            newMaterials[useSpecificIndex ? index : 0] = materials.GetRandomItem();

            meshRenderer.materials = newMaterials.ToArray();
        }
        else
        {
            List<Material> newMaterials = new List<Material>(skinnedMeshRenderer.materials);
            newMaterials[useSpecificIndex ? index : 0] = materials.GetRandomItem();

            skinnedMeshRenderer.materials = newMaterials.ToArray();
        }
    }
}