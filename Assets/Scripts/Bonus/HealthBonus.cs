using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

public class HealthBonus : Bonus
{
    public static event Action OnTakeHealthBonus;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ICanTakeBonus bonusObject = collision.gameObject.GetComponent<ICanTakeBonus>();
        if (bonusObject != null)
        {
            OnTakeHealthBonus?.Invoke();
            DestroyBonus();
        }
    }

    private void DestroyBonus()
    {
        gameObject.SetActive(false);
    }
}