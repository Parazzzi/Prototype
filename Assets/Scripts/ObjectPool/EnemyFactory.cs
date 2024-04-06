using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private SpawnConfiguration spawnConfig;
    [SerializeField] private MeteorConfiguration meteorConfiguration;
    [SerializeField] private BorderComponent border;

    [SerializeField] private GameObject meteorPrefab;
    [SerializeField] private GameObject spaceShipPrefab;

    private ObjectPool meteorPool;
    private ObjectPool spaceShipPool;

    private void Awake()
    {
        meteorPool = new ObjectPool(meteorPrefab, spawnConfig.meteorInitialSize);
        spaceShipPool = new ObjectPool(spaceShipPrefab, spawnConfig.spaceShipInitialSize);
    }

    public void CreateDefaultMeteor()
    {
        GameObject meteor = meteorPool.GetObjectFromPool();
        SpawnInPosition(meteor);
        Meteor meteorComponent = meteor.gameObject.GetComponent<Meteor>();
        meteor.gameObject.GetComponent<SpriteRenderer>();
        ChangeMeteorConfiguration(meteor, meteorComponent);
    }

    public void CreateEnemyShip()
    {
        GameObject spaceShip = spaceShipPool.GetObjectFromPool();
        SpawnInPosition(spaceShip);
        SpaceShip spaceShipComponent = spaceShip.gameObject.GetComponent<SpaceShip>();
        ChangeSpaceShipConfiguration(spaceShip, spaceShipComponent);
    }

    private void SpawnInPosition(GameObject obj)
    {
        float t = Random.value;
        Vector3 randomPosition = Vector3.Lerp(border.minXBonusSpawnPos.position, border.maxXBonusSpawnPos.position, t);
        obj.transform.position = randomPosition;
    }

    private void ChangeSpaceShipConfiguration(GameObject spaceShip, SpaceShip spaceShipComponent)
    {
        spaceShipComponent.ActivateSpaceShip();
    }

    private void ChangeMeteorConfiguration(GameObject meteor, Meteor meteorComponent)
    {
        float randomScale = Random.Range(meteorConfiguration.MinScale, meteorConfiguration.MaxScale);
        meteor.transform.localScale = new Vector3(randomScale, randomScale, randomScale);

        float randomSpeed = Random.Range(meteorConfiguration.MinSpeed, meteorConfiguration.MaxSpeed);

        meteorComponent.speed = randomSpeed;
        meteorComponent.MoveMeteor();

        int randomIndex = Random.Range(0, meteorConfiguration.sprites.Length);
        meteorComponent.spriteRenderer.sprite = meteorConfiguration.sprites[randomIndex];

        float randomRotation = Random.Range(meteorConfiguration.MinRotation, meteorConfiguration.MaxRotation);
        meteor.transform.rotation = Quaternion.Euler(0f, 0f, randomRotation);
    }
}
