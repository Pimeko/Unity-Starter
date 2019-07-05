using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

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
    [SerializeField, ReorderableList]
    List<MaterialColor> colorProperties;
    [SerializeField, ReorderableList]
    List<MaterialInt> intProperties;
    [SerializeField, ReorderableList]
    List<MaterialFloat> floatProperties;

    Material currentMaterial;

    void Start()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer == null || materialIndex < 0 || materialIndex > meshRenderer.materials.Length)
            throw new UnityException("No mesh renderer found.");

        currentMaterial = meshRenderer.materials[materialIndex];

        ApplyColorProperties();
    }

    void ApplyColorProperties()
    {
        foreach (var property in colorProperties)
        {
            Color color = property.fullRandom ? new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f))
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