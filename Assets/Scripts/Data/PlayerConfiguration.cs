using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfiguration", menuName = "PlayerConfiguration")]
public class PlayerConfiguration : ScriptableObject
{
    public int maxHealth;
    public int currentHealthInStart;
    public int currentHealth;

    public float maxMoveSpeed;
    public float currentMoveSpeed;

    public float currentShootInterval;

    public int maxBulletCount = 60;
    public int currentBulletCount;

}


