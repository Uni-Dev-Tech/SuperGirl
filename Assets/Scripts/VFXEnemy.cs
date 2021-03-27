using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXEnemy : MonoBehaviour
{
    public ParticleSystem noticed, shoot, death;

    public void NoticePlayer()
    {
        noticed.Play();
    }

    public void Shooting()
    {
        shoot.Play();
    }

    public void Death()
    {
        death.Play();
    }
}
