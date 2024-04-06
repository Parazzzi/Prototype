using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopConfig", menuName = "ShopConfig")]
public class ShopConfig : ScriptableObject
{
    public int moveSpeed = 200;
    public int addHealth = 500;
}
