using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnConfiguration", menuName = "SpawnConfiguration")]
public class SpawnConfiguration : ScriptableObject
{
    [Header("Bonus Spawn Interval")]
    public int ammoBonusSpawnInterval;
    public int healthBonusSpawnInterval;
   
    [Header("Enemy Spawn Interval")]
    public float meteorSpawnInterval;
    public float spaceShipSpawnInterval;

    [Header("PoolSize")]
    public int meteorInitialSize;
    public int spaceShipInitialSize;
    public int playerBulletInitialSize;
    public int enemyBulletInitialSize;
    public int healthBonusInitialSize;
    public int ammoBonusInitialSize;

}
