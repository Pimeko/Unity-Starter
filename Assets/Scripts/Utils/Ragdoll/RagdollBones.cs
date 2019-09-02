using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class RagdollBones : SerializedMonoBehaviour
{
    [SerializeField]
    List<Transform> bones = new List<Transform>();

    Animator animator;
    Animator CurrentAnimator => transform.CachedComponent(ref animator);

    HumanBodyBones[] ragdollBones = new HumanBodyBones[] {
        HumanBodyBones.Hips,
        HumanBodyBones.LeftUpperLeg,
        HumanBodyBones.LeftLowerLeg,
        HumanBodyBones.LeftFoot,
        HumanBodyBones.RightUpperLeg,
        HumanBodyBones.RightLowerLeg,
        HumanBodyBones.RightFoot,
        HumanBodyBones.LeftUpperArm,
        HumanBodyBones.LeftLowerArm,
        HumanBodyBones.RightUpperArm,
        HumanBodyBones.RightLowerArm,
        HumanBodyBones.Spine,
        HumanBodyBones.Head
    };

    [Button]
    public void ParseBones()
    {
        bones.Clear();
        foreach (HumanBodyBones bone in ragdollBones)
            bones.Add(CurrentAnimator.GetBoneTransform(bone));
    }
}