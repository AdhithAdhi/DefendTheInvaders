using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleReSpwan : MonoBehaviour, IPooledObject
{
    ParticleSystem particle;
    public void OnObjectSpwan()
    {
        particle = GetComponentInChildren<ParticleSystem>();
        particle.Stop();
        particle.Play();
    }

    public void ReturnToPool()
    {

    }
}
