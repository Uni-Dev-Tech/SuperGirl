using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator playerAnimator;

    private bool moveAnimationIsRunning = false;
    private Vector3 pressedPosition;

    [HideInInspector] public bool playerAnimationIsActive = false;
    private void Update()
    {
        if(playerAnimationIsActive)
        {
            if (Input.GetMouseButtonDown(0))
                pressedPosition = Input.mousePosition;

            if (Input.GetMouseButton(0))
            {
                if (!moveAnimationIsRunning)
                {
                    if (pressedPosition != Input.mousePosition)
                    {
                        moveAnimationIsRunning = true;
                        playerAnimator.SetTrigger("InMove");
                    }
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                moveAnimationIsRunning = false;
                playerAnimator.SetTrigger("StopMove");
            }
        }
    }

    public void MakePunch()
    {
        playerAnimator.SetTrigger("Punch");
    }
}
