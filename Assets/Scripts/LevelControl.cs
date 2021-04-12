﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControl : MonoBehaviour
{
    public VFXPlayer playerVFX;
    public PlayerAnimation playerAnimation;
    public PlayerControl playerControl;
    public float appearanceAnimationTime;
    public float delayBeforeAbleToPlay;

    static public LevelControl instance;

    private void Start()
    {
        StartCoroutine(GameStart());
    }

    private IEnumerator GameStart()
    {
        yield return new WaitForSeconds(appearanceAnimationTime);
        playerVFX.DustAppearance();
        yield return new WaitForSeconds(delayBeforeAbleToPlay);
        playerAnimation.playerAnimationIsActive = true;
        playerControl.playerControlIsActive = true;
    }

    public void LevelFailed()
    {

    }
}
