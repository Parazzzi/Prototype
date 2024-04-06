using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Bullet : MonoBehaviour, IDamageableAndAddScore
{
    [SerializeField] private float speed;
    private Tween bulletTween;

    public void MoveBullet(float finalpositionY)
    {
        if (bulletTween != null && bulletTween.IsActive())
        {
            bulletTween.Kill();
        }

        bulletTween = transform.DOMoveY(finalpositionY, 0.5f)
            .SetEase(Ease.Linear)
            .OnComplete(DestroyBullet);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageableObject = collision.gameObject.GetComponent<IDamageable>();
        if (damageableObject != null)
            DestroyBullet();

        BorderComponent borderObject = collision.gameObject.GetComponent<BorderComponent>();
        if (borderObject != null)
            DestroyBullet();
    }

    private void DestroyBullet()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        bulletTween.Kill();
    }

}
