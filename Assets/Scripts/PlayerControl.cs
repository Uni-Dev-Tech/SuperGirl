using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerControl : MonoBehaviour
{
    public GameConfig GameConfig;
    public PlayerAnimation playerAnimation;
    public RagdolEffect ragdolEffect;
    public VFXPlayer playerVFX;

    public Rigidbody playerRb;
    public GameObject playerBody;
    public Collider playerCollider;

    private float motX, motY;

    [HideInInspector]
    public bool moveActive = true;

    private Vector3 motionDirection;
    private Vector3 mousePosition;

    private float playerHealth = 1f;

    [HideInInspector] public bool playerControlIsActive = false;
    void Update()
    {
        if(playerControlIsActive)
        {
            float dirX = 0;
            float dirY = 0;

            if (Input.GetMouseButtonDown(0))
            {
                motX = Input.mousePosition.x;
                motY = Input.mousePosition.y;

                mousePosition = Input.mousePosition;
            }

            if (Input.GetMouseButton(0))
            {
                dirX = -(motX - Input.mousePosition.x) * 1000 / Screen.width;
                dirY = -(motY - Input.mousePosition.y) * 1000 / Screen.height;
            }

            if (Input.GetMouseButtonUp(0))
            {
                motX = 0;
                motY = 0;
            }

            motionDirection = new Vector3(dirX, 0, dirY).normalized;
            if (moveActive)
            {
                if (Input.GetMouseButton(0))
                {
                    Vector3 lookDirection = new Vector3(dirX * GameConfig.PlayerSettings.rotationSens, playerBody.transform.position.y, dirY * GameConfig.PlayerSettings.rotationSens);

                    if (Input.mousePosition != mousePosition)
                    {
                        playerBody.transform.DOLookAt(lookDirection, 0.5f);
                    }
                }
            }
        }
    }
    private void FixedUpdate()
    {
        if (playerControlIsActive)
        {
            if (moveActive)
            {
                playerRb.velocity = motionDirection * GameConfig.PlayerSettings.playerSpeed * Time.fixedDeltaTime;
            }
        }
    }

    public void PunchEnemy(Rigidbody enemyRb)
    {
        playerVFX.KillEnemy();
        enemyRb.AddForce((playerBody.transform.forward + Vector3.up) * GameConfig.PlayerSettings.punchPower * Time.fixedDeltaTime, ForceMode.Impulse);
    }

    public void GetShot(float damage)
    {
        playerVFX.Damage();
        playerHealth -= damage;
        UIManager.instance.RenewPlayerHealthInf(playerHealth);

        if (playerHealth < 0)
        {
            playerAnimation.DeactivateAnimator();
            DeactivatePlayer();
            ragdolEffect.ActivateRagdollEffect();
            LevelControl.instance.LevelFailed();
        }
    }

    private void DeactivatePlayer()
    {
        playerVFX.Lose();
        playerControlIsActive = false;
        playerRb.collisionDetectionMode = CollisionDetectionMode.Discrete;
        playerRb.isKinematic = true;
        playerRb.useGravity = false;
        playerCollider.enabled = false;
    }

    public void WinLvl()
    {
        playerVFX.Win();
        playerAnimation.Dance();
        playerControlIsActive = false;
    }
}
