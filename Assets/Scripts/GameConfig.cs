using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "GameConfig")]
public class GameConfig : ScriptableObject
{
    public EnemySettings EnemySettings;
    public HostageSettings HostageSettings;
    public PlayerSettings PlayerSettings;
}
[System.Serializable]
public class EnemySettings
{
    public float enemyDamage;
    public float timeForRechargeGun;
    public float enemySpeed;
    public float radiusOfSightZone;
    public float rotationSpeedToPlayer;
    public float aimLengthPossibility;
}
[System.Serializable]
public class HostageSettings
{
    public float radiusOfSightZone;
}
[System.Serializable]
public class PlayerSettings
{
    public float playerSpeed;
    public float rotationSens;
    public float punchPower;
}
