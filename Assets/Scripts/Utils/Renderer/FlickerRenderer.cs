using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class FlickerRenderer : MonoBehaviour
{
    [SerializeField]
    Color flickerColor = Color.white;
    [SerializeField]
    float nbFlickers, delayBetweenFlickers;

    Material[] currentMaterials;
    Color[] initialColors;

    int nbFlickersDone;
    float elapsedTime;

    bool isFlickering, isInitialColor;

    void Start()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        SkinnedMeshRenderer skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();

        if (meshRenderer == null && skinnedMeshRenderer == null)
            throw new UnityException("No renderer found.");

        if (meshRenderer != null)
        {
            currentMaterials = meshRenderer.materials;
            initialColors = meshRenderer.materials.Select(e => e.color).ToArray();
        }
        else
        {
            currentMaterials = skinnedMeshRenderer.materials;
            initialColors = skinnedMeshRenderer.materials.Select(e => e.color).ToArray();
        }

        isFlickering = false;
    }

    [Button]
    public void Flicker()
    {
        elapsedTime = 0;
        nbFlickersDone = 0;
        isInitialColor = false;

        isFlickering = true;

        ApplyFlickerColor();
    }

    void ApplyInitialColor()
    {
        for (int i = 0; i < currentMaterials.Length; i++)
            currentMaterials[i].color = initialColors[i];
    }

    void ApplyFlickerColor()
    {
        for (int i = 0; i < currentMaterials.Length; i++)
            currentMaterials[i].color = flickerColor;
    }

    void Update()
    {
        if (!isFlickering)
            return;

        if (elapsedTime > delayBetweenFlickers)
        {
            if (isInitialColor)
                ApplyFlickerColor();
            else
                ApplyInitialColor();
            isInitialColor = !isInitialColor;

            nbFlickersDone++;
            if (nbFlickersDone == nbFlickers * 2)
            {
                isFlickering = false;
                ApplyInitialColor();
            }
            else
                elapsedTime = 0;
        }
        else
        {
            elapsedTime += Time.deltaTime;
        }
    }
}