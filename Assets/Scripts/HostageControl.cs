using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostageControl : MonoBehaviour
{
    public VFXHostage hostageVFX;
    public HostageAnimation hostageAnimation;

    public EnemyController[] enemyStates;

    public float radiusZone;

    private bool hostageInDanger = true;
    private bool playerWasNoticed = false;
    private void Update()
    {
        if (hostageInDanger)
        {
            if(!playerWasNoticed)
                ReactionOnPlayer();
            CheckForEnemyState();
        }
    }

    private void CheckForEnemyState()
    {
        byte enemyDead = 0;
        for(int i = 0; i < enemyStates.Length; i++)
        {
            if (enemyStates[i].enemyDeactivated)
                enemyDead++;
        }
        if (enemyDead == enemyStates.Length)
            EnemiesDead();
    }

    private void EnemiesDead()
    {
        hostageInDanger = false;
        hostageVFX.Happy();
        hostageAnimation.HostageWinReaction();
    }

    private void ReactionOnPlayer()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radiusZone);

        foreach (Collider collider in colliders)
            if (collider.gameObject.CompareTag("Player"))
            {
                hostageVFX.Scary();
                playerWasNoticed = true;
            }
    }
}
