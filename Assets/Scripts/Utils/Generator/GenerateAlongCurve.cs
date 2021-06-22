using System.Collections;
using System.Collections.Generic;
using BansheeGz.BGSpline.Components;
using BansheeGz.BGSpline.Curve;
using Sirenix.OdinInspector;
# if UNITY_EDITOR
using UnityEditor;
# endif
using UnityEngine;
using UnityEngine.Events;

public class GenerateAlongCurve : MonoBehaviour
{
    [SerializeField]
    BGCcMath curveMath;
    [SerializeField]
    List<GameObject> prefabs;
    [SerializeField]
    Material material;
    [SerializeField]
    float distanceBetweenEach;
    [SerializeField]
    Transform editorParent;
    [SerializeField]
    Vector3 offset;
    [SerializeField]
    bool deleteEditorOnStart = false;
    [SerializeField]
    UnityEvent onGenerated;

    void Start()
    {
        if (deleteEditorOnStart)
            Destroy(editorParent.gameObject);
        Generate(transform);
        onGenerated?.Invoke();
    }

    void Generate(Transform parent)
    {
        if (distanceBetweenEach <= 0)
            throw new UnityException("Distance between each must be > 0");

        float totalDistance = curveMath.GetDistance();

        float distance = 0;
        int nbPrefabsDone = 0;
        while (distance < totalDistance)
        {
#if UNITY_EDITOR
            var instance = PrefabUtility.InstantiatePrefab(prefabs[nbPrefabsDone]) as GameObject;
#else
            var instance = Instantiate(prefabs[nbPrefabsDone]);
#endif
            instance.transform.SetParent(parent);
            if (material != null)
                instance.GetComponentInChildren<MeshRenderer>().material = material;

            Vector3 tangent;
            var position = curveMath.CalcPositionAndTangentByDistance(distance, out tangent);
            instance.transform.rotation = QuaternionExtensions.LookRotation(tangent);
            instance.transform.position = position + instance.transform.right * offset.x + instance.transform.up * offset.y + instance.transform.forward * offset.z;

            distance += distanceBetweenEach;
            nbPrefabsDone++;
            nbPrefabsDone %= prefabs.Count;
        }
    }

    [Button("Generate")]
    public void Test()
    {
        Clear();
        Generate(editorParent);
    }

    [Button]
    public void Clear()
    {
        var children = editorParent.GetFirstChildren();
        foreach (Transform child in children)
            DestroyImmediate(child.gameObject);
    }
}