using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostageAnimation : MonoBehaviour
{
    public Animator animatorHostage;

    public void HostageWinReaction()
    {
        animatorHostage.SetTrigger("Win");
    }
}
