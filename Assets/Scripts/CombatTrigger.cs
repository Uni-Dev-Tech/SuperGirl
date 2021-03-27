using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTrigger : MonoBehaviour
{
    public PlayerAnimation playerAnimation;
    public PlayerControl playerControl;
    public EventReceiver eventReceiver;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            playerAnimation.MakePunch();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            if (other.TryGetComponent(out EnemyController enemyController))
            {
                if(eventReceiver.punchPossible)
                {
                    enemyController.enemyAnimation.DeactivateAnimator();
                    enemyController.DeactivateCurrentEnemy();
                    enemyController.ragdolEffect.ActivateRagdollEffect();
                    playerControl.PunchEnemy(enemyController.ragdolEffect.bodyRb);
                    //playerAnimation.MakePunch();
                }

                //enemyController.enemyAnimation.DeactivateAnimator();
                //enemyController.DeactivateCurrentEnemy();
                //enemyController.ragdolEffect.ActivateRagdollEffect();
                //enemyController.enemyAnimation.DeactivateAnimator();
                //enemyController.DeactivateCurrentEnemy();
                //enemyController.ragdolEffect.ActivateRagdollEffect();
                //playerControl.PunchEnemy(enemyController.ragdolEffect.bodyRb);
                //playerAnimation.MakePunch();
            }
        }
    }
}
