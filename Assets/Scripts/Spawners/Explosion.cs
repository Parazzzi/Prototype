using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;
    private ObjectPool explosionPool;
    private int initianSize = 5;
    private float timeToDestroy = 0.5f;

    private void OnEnable()
    {
        Meteor.OnMeteorDie += SpawnExplosion;
        SpaceShip.OnSpaceShipDie += SpawnExplosion;
        PlayerHealth.OnPlayerTakeDamage += SpawnExplosion;
    }

    private void Awake()
    {
        explosionPool = new ObjectPool(explosionPrefab, initianSize);
    }

    private void SpawnExplosion(Transform spawnPoz)
    {
        SoundManager.instance.PlaySound(SoundNames.DEATH);
        GameObject explosion = explosionPool.GetObjectFromPool();
        explosion.transform.position = spawnPoz.position;
        StartCoroutine(WaitForTimeAndDestroy(explosion));
    }

    private IEnumerator WaitForTimeAndDestroy(GameObject explosion)
    {
        yield return new WaitForSeconds(timeToDestroy);
        explosion.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        Meteor.OnMeteorDie -= SpawnExplosion;
        SpaceShip.OnSpaceShipDie -= SpawnExplosion;
        PlayerHealth.OnPlayerTakeDamage -= SpawnExplosion;
    }
}
