using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXEnemy : MonoBehaviour
{
    public ParticleSystem noticed, shoot, death, angry;

    public void NoticePlayer()
    {
        noticed.Play();
    }

    public void Shooting()
    {
        noticed.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        angry.Play();
        shoot.Play();
    }

    public void Death()
    {
        noticed.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        angry.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        shoot.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        death.Play();
    }
}
