using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesStopper : MonoBehaviour
{
    [SerializeField]
    List<ParticleSystem> particles;

    public void StartParticles()
    {
        foreach (ParticleSystem particle in particles)
        {
            var emission = particle.emission;
            emission.enabled = true;
        }
    }
    
    public void StopParticles()
    {
        foreach (ParticleSystem particle in particles)
        {
            var emission = particle.emission;
            emission.enabled = false;
        }
    }
}