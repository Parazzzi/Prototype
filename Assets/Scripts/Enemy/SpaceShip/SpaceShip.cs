using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour, IDamageable
{
    [SerializeField] private SpaceShipConfiguration shipConfig;
    [SerializeField] private Transform firePoint;

    private Tween spaceShipTween;

    public static event Action<int> OnSpaceShipTakeDamage;
    public static event Action<Transform> OnEnemyShoot;
    public static event Action<Transform> OnSpaceShipDie;

    public void ActivateSpaceShip()
    {
        Shoot();
        Move();
    }

    public void Shoot()
    {
        StartCoroutine(EnemyShoot());
    }

    private IEnumerator EnemyShoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            OnEnemyShoot?.Invoke(firePoint);
        }
    }

    private void Move()
    {
        float positoinY = -18;

        if (spaceShipTween != null && spaceShipTween.IsActive())
            spaceShipTween.Kill();

        spaceShipTween = transform.DOMoveY(positoinY, 1f / shipConfig.speed)
            .SetEase(Ease.Linear)
            .OnComplete(TakeDamage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageableAndAddScore addScoreObj = collision.gameObject.GetComponent<IDamageableAndAddScore>();
        if (addScoreObj != null)
        {
            AddScore();
            TakeDamage();
        }

        IDamageable damageableObject = collision.gameObject.GetComponent<IDamageable>();
        if (damageableObject != null)
            TakeDamage();
    }

    public void AddScore()
    {
        OnSpaceShipTakeDamage?.Invoke(shipConfig.addScoreCount);
    }

    public void TakeDamage()
    {
        OnSpaceShipDie?.Invoke(gameObject.transform);
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        if (spaceShipTween != null)
        {
            spaceShipTween.Kill();
        }
    }

}
