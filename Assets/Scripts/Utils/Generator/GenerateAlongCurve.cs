using System.Collections;
using System.Collections.Generic;
using BansheeGz.BGSpline.Components;
using BansheeGz.BGSpline.Curve;
using Sirenix.OdinInspector;
using UnityEngine;

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
    Transform testParent;
    [SerializeField]
    Vector3 offset;

    void Start()
    {
        Destroy(testParent.gameObject);

        Generate(transform);
    }

    void Generate(Transform parent)
    {
        float totalDistance = curveMath.GetDistance();

        float distance = 0;
        int nbPrefabsDone = 0;
        while (distance < totalDistance)
        {
            var instance = Instantiate(prefabs[nbPrefabsDone]);
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

    [Button]
    public void Test()
    {
        Clear();
        Generate(testParent);
    }

    [Button]
    public void Clear()
    {
        var children = testParent.GetFirstChildren();
        foreach (Transform child in children)
            DestroyImmediate(child.gameObject);
    }
}