using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyController : MonoBehaviour
{
    public EnemyAnimation enemyAnimation;
    public RagdolEffect ragdolEffect;
    public VFXEnemy enemyVFX;
    public GameConfig GameConfig;

    public Collider enemyCollider;

    public Rigidbody enemyRb;
    public float enemySpeedMotion;

    public float radiusOfSightZone;
    public LayerMask layerMask;

    public float rotationSpeedToPlayer;
    public float aimLengthPossibility;

    private bool playerWasNoticed = false;
    private bool followThePlayer = false;

    private int moveDirection = 0;

    [HideInInspector]
    public bool enemyDeactivated = false;

    private bool chargingGun = false;

    private void Update()
    {
        if(!enemyDeactivated)
        {
            if (!playerWasNoticed)
                SightZone(enemyRb.position, radiusOfSightZone, layerMask);
            else
                SightZoneDanger(enemyRb.position, radiusOfSightZone, layerMask);
        }
    }

    private void FixedUpdate()
    {
        if(!enemyDeactivated)
        {
            if (playerWasNoticed)
            {
                if (Input.GetMouseButton(0))
                {
                    enemyAnimation.EnemyGoBack();
                    enemyRb.AddForce((enemyRb.transform.forward * enemySpeedMotion * Time.fixedDeltaTime) * moveDirection, ForceMode.VelocityChange);
                }
                else
                {
                    enemyAnimation.EnemyStayStill();
                    enemyRb.velocity = Vector3.zero;
                }
            }
        }
    }

    private void SightZone(Vector3 enemyPos, float radiusZone, LayerMask layerMask)
    {
        Collider[] colliders = Physics.OverlapSphere(enemyPos, radiusZone, layerMask);

        foreach(Collider collider in colliders)
            if (collider.gameObject.CompareTag("Player"))
            {
                VFXPlayer.instance.Anger();
                playerWasNoticed = true;
                SmoothRotationToTarget(enemyRb.transform, collider.transform.position, rotationSpeedToPlayer);
                enemyVFX.NoticePlayer();
                //Logs("Player in sight zone!");
            }
    }

    private void SmoothRotationToTarget(Transform enemyTr, Vector3 playerPos, float rotationSpeed)
    {
        enemyTr.DOLookAt(playerPos, rotationSpeed).OnComplete(FollowThePlayer);
    }

    private void FollowThePlayer()
    {
        followThePlayer = true;
    }

    private void SightZoneDanger(Vector3 enemyPos, float radiusZone, LayerMask layerMask)
    {
        Collider[] colliders = Physics.OverlapSphere(enemyPos, radiusZone, layerMask);

        bool playerInZone = false;

        foreach (Collider collider in colliders)
            if (collider.gameObject.CompareTag("Player"))
            {
                if(followThePlayer)
                    enemyRb.transform.LookAt(collider.transform);

                AimOnPlayer(enemyPos, aimLengthPossibility);
                playerInZone = true;
            }

        if(!playerInZone)
        {
            EnemyToDeffaultCondition();
        }
    }

    private void AimOnPlayer(Vector3 enemyPos, float aimLength)
    {
        RaycastHit raycastHit = new RaycastHit();
        Ray ray = new Ray(enemyRb.transform.position, enemyRb.transform.forward * aimLengthPossibility);

        if (Physics.Raycast(ray, out raycastHit, aimLength/*, layerMask*/))
        {
            if(raycastHit.transform.CompareTag("Player"))
            {
                MoveFromPlayer(enemyRb.position, enemyPos);
                if(!chargingGun)
                {
                    ShootPlayer(raycastHit.transform.gameObject);
                    enemyVFX.Shooting();
                    StartCoroutine(RechargeGun());
                }
                Debug.DrawRay(enemyRb.transform.position, enemyRb.transform.forward * aimLengthPossibility, Color.red);
                //Logs("CatchedInAim");
            }
        }
    }

    private void MoveFromPlayer(Vector3 playerPos, Vector3 enemyPos)
    {
        float distanceBetween = Vector3.Distance(playerPos, enemyPos);

        if(Input.GetMouseButton(0))
        {
            if (distanceBetween <= 3)
            {
                moveDirection = -1;
                //enemyAnimation.EnemyGoBack();
            }
            if (distanceBetween > 8)
            {
                moveDirection = 1;
            }
        }
        else
            moveDirection = 0;
    }

    private void ShootPlayer(GameObject player)
    {
        if(player.TryGetComponent(out PlayerControl playerControl))
        {
            playerControl.GetShot(GameConfig.GeneralGameplaySettings.enemyDamage);
            chargingGun = true;
        }
    }

    private void EnemyToDeffaultCondition()
    {
        playerWasNoticed = false;
        followThePlayer = false;

        moveDirection = 0;
        enemyRb.velocity = Vector3.zero;

        enemyAnimation.EnemyStayStill();
    }

    public void DeactivateCurrentEnemy()
    {
        LevelControl.instance.EnemyKilled();
        enemyVFX.Death();
        EnemyToDeffaultCondition();
        enemyDeactivated = true;
        enemyRb.collisionDetectionMode = CollisionDetectionMode.Discrete;
        enemyRb.isKinematic = true;
        enemyRb.useGravity = false;
        enemyCollider.enabled = false;
    }

    private IEnumerator RechargeGun()
    {
        yield return new WaitForSeconds(GameConfig.GeneralGameplaySettings.timeForRechargeGun);
        chargingGun = false;
    }

    private void Logs(string message)
    {
        Debug.Log($"Enemy: {enemyRb.gameObject.name}, Message: {message}");
    }
}
