using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesColor : MonoBehaviour
{
    [SerializeField]
    Color color;

    ParticleSystem[] particles;

    void Start()
    {
        UpdateColor();
    }

    void UpdateColor()
    {
        if (particles == null)
            particles = GetComponentsInChildren<ParticleSystem>();

        foreach (var particle in particles)
        {
            var main = particle.main;
            main.startColor = color;
        }
    }

    void OnValidate()
    {
        UpdateColor();
    }
}