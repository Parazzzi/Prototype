using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BonusSpawner : MonoBehaviour
{
    [Header("Ammo Bonus")]
    [SerializeField] private GameObject bonusAmmoPrefab;
    private ObjectPool bonusAmmoPool;

    [Header("Health Bonus")]
    [SerializeField] private GameObject bonusHealthPrefab;
    private ObjectPool bonusHealthPool;

    [SerializeField] private BorderComponent border;
    [SerializeField] private SpawnConfiguration spawnConfig;
    private Coroutine _spawnHelathBonusCorotine;
    private Coroutine _spawnAmmoBonusCorotine;

    private void Awake()
    {
        bonusAmmoPool = new ObjectPool(bonusAmmoPrefab, spawnConfig.ammoBonusInitialSize);
        bonusHealthPool = new ObjectPool(bonusHealthPrefab, spawnConfig.healthBonusInitialSize);
    }

    private void Start()
    {
        _spawnHelathBonusCorotine = StartCoroutine(SpawnAmmoBonus());
        _spawnAmmoBonusCorotine = StartCoroutine(SpawnHealthBonus());
    }

    private void CreateBonus(ObjectPool bonusPool, GameObject prefab)
    {
        float t = Random.value;
        Vector3 randomPosition = Vector3.Lerp(border.minXBonusSpawnPos.position, border.maxXBonusSpawnPos.position, t);
        GameObject bonus = bonusPool.GetObjectFromPool();
        bonus.transform.position = randomPosition;
    }

    private IEnumerator SpawnAmmoBonus()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnConfig.ammoBonusSpawnInterval);
            CreateBonus(bonusAmmoPool, bonusAmmoPrefab);
        }
    }

    private IEnumerator SpawnHealthBonus()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnConfig.healthBonusSpawnInterval);
            CreateBonus(bonusHealthPool, bonusHealthPrefab);
        }
    }

    private void OnDisable()
    {
        if (_spawnAmmoBonusCorotine != null)
            StopCoroutine(_spawnAmmoBonusCorotine);
        if (_spawnHelathBonusCorotine != null)
            StopCoroutine(_spawnHelathBonusCorotine);
    }

}
