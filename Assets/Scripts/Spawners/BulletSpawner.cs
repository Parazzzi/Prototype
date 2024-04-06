using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] private GameObject enemyBulletPrefab;
    private ObjectPool enemyBulletPool;

    [Header("Player")]
    [SerializeField] private GameObject playerBulletPrefab;
    private ObjectPool playerBulletPool;

    [Header("TripleShootGun")]
    [SerializeField] private List<Transform> playerFirePoints;
    private int playerTripleBulletCount = 3;

    [SerializeField] private SpawnConfiguration spawnConfig;
    private float playerBulletFinalPos = 17;
    private float enemyBulletFinalPos = -19;

    private void OnEnable()
    {
        PlayerShoot.OnPlayerTripleShoot += PlayeTRIPLEShoot;
        PlayerShoot.OnPlayerSoloShoot += PlayerSoloShoot;
        SpaceShip.OnEnemyShoot += EnemyShoot;
    }

    private void Awake()
    {
        playerBulletPool = new ObjectPool(playerBulletPrefab, spawnConfig.playerBulletInitialSize);
        enemyBulletPool = new ObjectPool(enemyBulletPrefab, spawnConfig.enemyBulletInitialSize);
    }

    private void PlayerSoloShoot()
    {
        SoundManager.instance.PlaySound(SoundNames.PLAYER_SHOOT);
        SpawnBullet(playerBulletPool, playerFirePoints[0].position, playerBulletFinalPos);
    }

    private void PlayeTRIPLEShoot()
    {
        for (int i = 0; i < Mathf.Min(playerFirePoints.Count, playerTripleBulletCount); i++)
        {
            SoundManager.instance.PlaySound(SoundNames.PLAYER_SHOOT);
            SpawnBullet(playerBulletPool, playerFirePoints[i].position, playerBulletFinalPos);
        }
    }

    private void EnemyShoot(Transform firePoint)
    {
        SoundManager.instance.PlaySound(SoundNames.ENEMY_SHOOT);
        SpawnBullet(enemyBulletPool, firePoint.position, enemyBulletFinalPos);
    }

    private void SpawnBullet(ObjectPool bulletPool, Vector3 position, float finalPosition)
    {
        GameObject bulletObject = bulletPool.GetObjectFromPool();
        bulletObject.transform.position = position;
        Bullet bullet = bulletObject.gameObject.GetComponent<Bullet>();
        bullet.MoveBullet(finalPosition);
    }

    private void OnDisable()
    {
        PlayerShoot.OnPlayerSoloShoot -= PlayerSoloShoot;
        PlayerShoot.OnPlayerTripleShoot -= PlayeTRIPLEShoot;
        SpaceShip.OnEnemyShoot -= EnemyShoot;
    }

}
