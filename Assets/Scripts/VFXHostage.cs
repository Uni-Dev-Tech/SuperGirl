using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXHostage : MonoBehaviour
{
    public ParticleSystem scary, happy;

    public void Happy()
    {
        scary.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        happy.Play();
    }

    public void Scary()
    {
        happy.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        scary.Play();
    }
}
