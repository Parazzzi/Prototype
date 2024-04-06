using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyFactory meteorFactory;
    [SerializeField] private SpawnConfiguration spawnConfig;

    private Coroutine _meteorSpawnerCorotine;
    private Coroutine _spaceShipSpawnerCorotine;

    private void Start()
    {
        _meteorSpawnerCorotine = StartCoroutine(SpawnMeteors());
        _spaceShipSpawnerCorotine = StartCoroutine(SpawnSpaceShip());
    }

    private IEnumerator SpawnMeteors()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnConfig.meteorSpawnInterval);
            meteorFactory.CreateDefaultMeteor();
        }
    }

    private IEnumerator SpawnSpaceShip()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnConfig.spaceShipSpawnInterval);
            meteorFactory.CreateEnemyShip();
        }
    }

    private void OnDisable()
    {
        StopCoroutine(_meteorSpawnerCorotine);
        StopCoroutine(_spaceShipSpawnerCorotine);
    }
  
}
