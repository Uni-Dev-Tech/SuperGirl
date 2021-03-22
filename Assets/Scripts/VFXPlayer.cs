using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXPlayer : MonoBehaviour
{
    public ParticleSystem dustBlow;
    public void DustAppearance()
    {
        dustBlow.Play();
    }
}
