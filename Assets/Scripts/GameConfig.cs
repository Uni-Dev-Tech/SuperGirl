using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "GameConfig")]
public class GameConfig : ScriptableObject
{
    public GeneralGameplaySettings GeneralGameplaySettings;
}
[System.Serializable]
public class GeneralGameplaySettings
{
    public float enemyDamage;
    public float timeForRechargeGun;
}

