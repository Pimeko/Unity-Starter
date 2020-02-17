using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MaterialProperty<T>
{
    public string name;
    public List<T> values;
}

[System.Serializable]
public class MaterialFloat : MaterialProperty<float> {}

[System.Serializable]
public class MaterialInt : MaterialProperty<int> {}

[System.Serializable]
public class MaterialColor : MaterialProperty<Color>
{
    public bool fullRandom;
}

public class MaterialRandomProperties : MonoBehaviour
{
    [SerializeField]
    int materialIndex;
    [SerializeField]
    List<MaterialColor> colorProperties;
    [SerializeField]
    List<MaterialInt> intProperties;
    [SerializeField]
    List<MaterialFloat> floatProperties;

    Material currentMaterial;

    void Start()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        SkinnedMeshRenderer skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        
        if ((meshRenderer == null && skinnedMeshRenderer == null)
            || materialIndex < 0
            || (meshRenderer != null && materialIndex > meshRenderer.materials.Length)
            || (skinnedMeshRenderer != null && materialIndex > skinnedMeshRenderer.materials.Length))
            throw new UnityException("Error while initializing material properties.");

        currentMaterial = meshRenderer != null ? meshRenderer.materials[materialIndex]
            : skinnedMeshRenderer.materials[materialIndex];

        ApplyColorProperties();
        ApplyIntProperties();
        ApplyFloatProperties();
    }

    public void Reapply()
    {
        ApplyColorProperties();
        ApplyIntProperties();
        ApplyFloatProperties();
    }

    void ApplyColorProperties()
    {
        foreach (var property in colorProperties)
        {
            Color color = property.fullRandom ? new Color(Random.value, Random.value, Random.value)
                : property.values.GetRandomItem();
            currentMaterial.SetColor(property.name, color);
        }
    }

    void ApplyIntProperties()
    {
        foreach (var property in intProperties)
            currentMaterial.SetInt(property.name, property.values.GetRandomItem());
    }

    void ApplyFloatProperties()
    {
        foreach (var property in floatProperties)
            currentMaterial.SetFloat(property.name, property.values.GetRandomItem());
    }
}