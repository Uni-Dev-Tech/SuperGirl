using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXPlayer : MonoBehaviour
{
    public ParticleSystem dustBlow;
    public ParticleSystem anger, win, lose, killEnemy, gotDamage;

    static public VFXPlayer instance;

    private void Awake()
    {
        if(VFXPlayer.instance != null)
        {
            Destroy(gameObject);
            return;
        }
        VFXPlayer.instance = this;
    }

    public void DustAppearance()
    {
        dustBlow.Play();
    }

    public void Anger()
    {
        gotDamage.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        anger.Play();
    }

    public void KillEnemy()
    {
        anger.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        gotDamage.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        killEnemy.Play();
    }

    public void Win()
    {
        anger.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        killEnemy.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        gotDamage.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        win.Play();
    }

    public void Lose()
    {
        anger.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        killEnemy.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        gotDamage.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        lose.Play();
    }

    public void Damage()
    {
        anger.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        killEnemy.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        gotDamage.Play();
    }
}
