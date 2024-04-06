using DG.Tweening;
using System;
using UnityEngine;


public class Meteor : MonoBehaviour, IDamageable
{
    [SerializeField] private MeteorConfiguration meteorConfig;
    public float speed;
    public SpriteRenderer spriteRenderer;
    public static event Action<int> OnMeteorTakeDamage;
    public static event Action<Transform> OnMeteorDie;

    private Tween meteorTween;

    public void MoveMeteor()
    {
        float positoinY = -18;

        if (meteorTween != null && meteorTween.IsActive())  
        {
            meteorTween.Kill();
        }

        meteorTween = transform.DOMoveY(positoinY, 1f / speed)
            .SetEase(Ease.Linear)
            .OnComplete(DestroyMeteor);
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
        {
            TakeDamage();
        }
    }

    public void TakeDamage() 
    {
        OnMeteorDie?.Invoke(gameObject.transform);
        DestroyMeteor();
    }

    public void AddScore()
    {
        OnMeteorTakeDamage?.Invoke(meteorConfig.addScoreCount);
    }

    private void DestroyMeteor()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        if (meteorTween != null)
        {
            meteorTween.Kill();
        }
    }

}
