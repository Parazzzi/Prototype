using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private PlayerConfiguration config;
    [SerializeField] private GameUI gameUI;

    public static event Action OnPlayerSoloShoot;
    public static event Action OnPlayerTripleShoot;

    private float lastShootTime = 0f;
    private float lastTripleShootTime = 0f;
    
    private void Start()
    {
        config.currentBulletCount = config.maxBulletCount;
        gameUI.UpdateAmmoText();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Q) && IsReloadSoloGun() && IsEnoughBullets(1))
        {
            lastShootTime = Time.time;
            OnPlayerSoloShoot.Invoke();
            MinusAmmo(1);
            return;
        }

        if (Input.GetKey(KeyCode.T) && IsReloadTrippleGun() && IsEnoughBullets(3))
        {
            lastTripleShootTime = Time.time;
            OnPlayerTripleShoot?.Invoke();
            MinusAmmo(3);
            return;
        }
    }

    private bool IsReloadSoloGun()
    {
        return (Time.time - lastShootTime) >= config.currentShootInterval;
    }

    private bool IsReloadTrippleGun()
    {
        return (Time.time - lastTripleShootTime) >= config.currentShootInterval;
    }

    private bool IsEnoughBullets(int bullet)
    {
        return config.currentBulletCount >= bullet;
    }

    public void AddAmmo(int ammo)
    {
        SoundManager.instance.PlaySound(SoundNames.RELOAD);
        config.currentBulletCount = Mathf.Clamp(config.currentBulletCount + ammo, 0, config.maxBulletCount);
        gameUI.UpdateAmmoText();
    }

    private void MinusAmmo(int ammo)
    {
        config.currentBulletCount = Mathf.Clamp(config.currentBulletCount - ammo, 0, config.maxBulletCount);
        gameUI.UpdateAmmoText();
    }

}
