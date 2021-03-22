using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyController : MonoBehaviour
{
    public EnemyAnimation enemyAnimation;

    public Rigidbody enemyRb;
    public float enemySpeedMotion;

    public float radiusOfSightZone;
    public LayerMask layerMask;

    public float rotationSpeedToPlayer;
    public float aimLengthPossibility;

    private bool playerWasNoticed = false;
    private bool followThePlayer = false;

    private int moveDirection = 0;

    private void Update()
    {
        if (!playerWasNoticed)
            SightZone(enemyRb.position, radiusOfSightZone, layerMask);
        else
            SightZoneDanger(enemyRb.position, radiusOfSightZone, layerMask);
    }

    private void FixedUpdate()
    {
        if(playerWasNoticed)
        {
            if(Input.GetMouseButton(0))
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

    private void SightZone(Vector3 enemyPos, float radiusZone, LayerMask layerMask)
    {
        Collider[] colliders = Physics.OverlapSphere(enemyPos, radiusZone, layerMask);

        foreach(Collider collider in colliders)
            if (collider.gameObject.CompareTag("Player"))
            {
                playerWasNoticed = true;
                SmoothRotationToTarget(enemyRb.transform, collider.transform.position, rotationSpeedToPlayer);
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

        if (Physics.Raycast(ray, out raycastHit, aimLength, layerMask))
        {
            if(raycastHit.transform.CompareTag("Player"))
            {
                MoveFromPlayer(enemyRb.position, enemyPos);
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

    private void EnemyToDeffaultCondition()
    {
        playerWasNoticed = false;
        followThePlayer = false;

        moveDirection = 0;
        enemyRb.velocity = Vector3.zero;

        enemyAnimation.EnemyStayStill();
    }

    private void Logs(string message)
    {
        Debug.Log($"Enemy: {enemyRb.gameObject.name}, Message: {message}");
    }
}
