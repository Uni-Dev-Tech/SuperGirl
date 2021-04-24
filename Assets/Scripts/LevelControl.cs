using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControl : MonoBehaviour
{
    public VFXPlayer playerVFX;
    public PlayerAnimation playerAnimation;
    public PlayerControl playerControl;
    public float appearanceAnimationTime;
    public float delayBeforeAbleToPlay;

    public int enemyNeedToKill;
    private int enemyKilled = 0;

    private bool levelComplete = false;

    static public LevelControl instance;

    private void Awake()
    {
        if(LevelControl.instance != null)
        {
            Destroy(gameObject);
            return;
        }
        LevelControl.instance = this;
    }

    private void Start()
    {
        StartCoroutine(GameStart());
    }

    private void Update()
    {
        if(!levelComplete)
            if (enemyNeedToKill <= enemyKilled)
            {
                levelComplete = true;
                LevelWin();
            }
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
        FingerDirection.instance.useFingerDirection = false;
        StartCoroutine(CompleteUI.instance.DelayBeforeComplete(false, 1f));
    }

    private void LevelWin()
    {
        FingerDirection.instance.useFingerDirection = false;
        StartCoroutine(CompleteUI.instance.DelayBeforeComplete(true, 1f));
        playerControl.WinLvl();
    }

    public void EnemyKilled()
    {
        enemyKilled++;
    }
}
