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
        UpdateColor(color);
    }

    public void UpdateColor(Color color)
    {
        this.color = color;
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
        UpdateColor(color);
    }
}