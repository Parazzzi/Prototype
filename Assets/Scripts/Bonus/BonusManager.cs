using System;
using UnityEngine;

public class BonusManager : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerShoot playerShoot;
    [SerializeField] private BonusConfig bonusConfig;

    private void OnEnable()
    {
        HealthBonus.OnTakeHealthBonus += TakeHealthBonus;
        AmmoBonus.OnTakeAmmoBonus += TakeAmmoBonus;
    }

    public void TakeAmmoBonus()
    {
        playerShoot.AddAmmo(bonusConfig.addAmmoAmount);
    }

    public void TakeHealthBonus()
    {
        playerHealth.AddHealth(bonusConfig.addHealthAmount);
    }

    private void OnDisable()
    {
        HealthBonus.OnTakeHealthBonus -= TakeHealthBonus;
        AmmoBonus.OnTakeAmmoBonus -= TakeAmmoBonus;
    }
}
