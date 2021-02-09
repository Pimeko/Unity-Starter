using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class FlickerRenderer : MonoBehaviour
{
    [SerializeField]
    Color flickerColor = Color.white;
    [SerializeField]
    float duration = 1;
    [SerializeField]
    bool findInChildren = false;
    
    Material currentMaterial;
    Tween sequence;

    Color initialColor;
    float stepDuration;
    
    void Start()
    {
        MeshRenderer meshRenderer = findInChildren ? transform.GetComponentOrInChildren<MeshRenderer>()
            : GetComponent<MeshRenderer>();
        SkinnedMeshRenderer skinnedMeshRenderer = findInChildren ? transform.GetComponentOrInChildren<SkinnedMeshRenderer>()
            : GetComponent<SkinnedMeshRenderer>();
        
        if (meshRenderer == null && skinnedMeshRenderer == null)
            throw new UnityException("No renderer found.");

        currentMaterial = meshRenderer != null ? meshRenderer.material : skinnedMeshRenderer.material;
        initialColor = currentMaterial.color;

        sequence = null;
    }

    [Button]
    public void Flicker()
    {
        stepDuration = duration / 5f;

        DOTweenUtils.KillTween(ref sequence);
        sequence = DOTween.Sequence()
            .AppendCallback(DoFlickerColor)
            .AppendInterval(stepDuration)
            .AppendCallback(DoInitialColor)
            .AppendInterval(stepDuration)

            .AppendCallback(DoFlickerColor)
            .AppendInterval(stepDuration)
            .AppendCallback(DoInitialColor)
            .AppendInterval(stepDuration)

            .AppendCallback(DoFlickerColor)
            .AppendInterval(stepDuration)
            .AppendCallback(DoInitialColor)
            .AppendInterval(stepDuration)

            .AppendCallback(DoFlickerColor)
            .AppendInterval(stepDuration)
            .AppendCallback(DoInitialColor)
            .AppendInterval(stepDuration);
    }

    void DoFlickerColor()
    {
        currentMaterial.color = flickerColor;
    }

    void DoInitialColor()
    {
        currentMaterial.color = initialColor;
    }
}