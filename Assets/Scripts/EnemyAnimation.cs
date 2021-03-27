using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    public Animator enemyAnimator;
    private bool enemyConditionStay = true;
    public void EnemyGoBack()
    {
        if(enemyConditionStay)
        {
            enemyAnimator.SetTrigger("Back");
            enemyConditionStay = false;
        }
    }

    public void EnemyStayStill()
    {
        if(!enemyConditionStay)
        {
            enemyAnimator.SetTrigger("Stay");
            enemyConditionStay = true;
        }
    }

    public void DeactivateAnimator()
    {
        enemyAnimator.enabled = false;
    }
}
