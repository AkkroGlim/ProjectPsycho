using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Melee", menuName = "WeaponData/Melee")]

public class MeleeSO : ScriptableObject
{
    [Header("Info")]
    public new string name;

    [Header("Attack")]
    public float damage;
    public float attackSpeed;
}
