using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBonus : Bonus
{
    public static event Action OnTakeAmmoBonus;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ICanTakeBonus bonusObject = collision.gameObject.GetComponent<ICanTakeBonus>();
        if (bonusObject != null)
        {
            OnTakeAmmoBonus?.Invoke();
            DestroyBonus();
        }
    }

    private void DestroyBonus()
    {
        gameObject.SetActive(false);
    }
}
