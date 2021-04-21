﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerControl : MonoBehaviour
{
    public PlayerAnimation playerAnimation;
    public RagdolEffect ragdolEffect;

    public Rigidbody playerRb;
    public GameObject playerBody;
    public Collider playerCollider;
    public float speedPlayerMotion;
    public float rotSens;
    private float motX, motY;

    public float punchPower;

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
                    Vector3 lookDirection = new Vector3(dirX * rotSens, playerBody.transform.position.y, dirY * rotSens);

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
                playerRb.velocity = motionDirection * speedPlayerMotion * Time.fixedDeltaTime;
            }
        }
    }

    public void PunchEnemy(Rigidbody enemyRb)
    {
        //enemyRb.gameObject.transform.position += new Vector3(0, 10f, 0);
        enemyRb.AddForce((playerBody.transform.forward + Vector3.up) * punchPower * Time.fixedDeltaTime, ForceMode.Impulse);
    }

    public void GetShot(float damage)
    {
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
        playerControlIsActive = false;
        playerRb.collisionDetectionMode = CollisionDetectionMode.Discrete;
        playerRb.isKinematic = true;
        playerRb.useGravity = false;
        playerCollider.enabled = false;
    }
}
