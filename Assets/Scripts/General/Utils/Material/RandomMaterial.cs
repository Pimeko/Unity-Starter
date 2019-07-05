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
        if (meshRenderer == null)
            throw new UnityException("No mesh renderer found.");

        meshRenderer.material = materials.GetRandomItem();
    }
}