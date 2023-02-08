using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WeaponData/Range/DamageConfig", fileName = "DamageConf")]

public class DamageConfigSO : ScriptableObject
{
    public int Damage;

    public int GetDamage()
    {
        return Damage; 
    }
}
