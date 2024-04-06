using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable , ICanTakeBonus
{
    [SerializeField] private PlayerConfiguration config;
    [SerializeField] private GameUI gameUI;

    public static event Action<Transform> OnPlayerTakeDamage;
    public static event Action OnPlayerDied;

    private void OnEnable()
    {
        config.currentHealth = config.currentHealthInStart;
    }

    private void Start()
    {
        gameUI.UpdateHealthText();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageableObject = collision.gameObject.GetComponent<IDamageable>();
        if (damageableObject != null)
            TakeDamage();

        IDamageableAndAddScore damageableScoreObject = collision.gameObject.GetComponent<IDamageableAndAddScore>();
        if (damageableScoreObject != null)
            TakeDamage();
    }

    public void AddHealth(int health)
    {
        SoundManager.instance.PlaySound(SoundNames.DRINK);
        config.currentHealth += health;
        gameUI.UpdateHealthText();
    }

    public void TakeDamage()
    {
        config.currentHealth--;
        if (config.currentHealth == 0)
        {
            OnPlayerDied?.Invoke();
        }
        gameUI.UpdateHealthText();
        OnPlayerTakeDamage?.Invoke(gameObject.transform);
    }

}
