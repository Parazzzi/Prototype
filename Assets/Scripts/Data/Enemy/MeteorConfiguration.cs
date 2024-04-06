using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[CreateAssetMenu(fileName = "MeteorConfiguration", menuName = "MeteorConfiguration")]

public class MeteorConfiguration : ScriptableObject
{
    public int addScoreCount;

    public Sprite[] sprites;

    public float MinRotation = 90f;
    public float MaxRotation = 180f;

    public float MinScale = 0.4f;
    public float MaxScale = 1f;

    public float MinSpeed = 0.5f;
    public float MaxSpeed = 3f;

}
