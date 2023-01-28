using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShootConf", menuName = "WeaponData/Range/ShootConf")]

public class ShootConfigSO : ScriptableObject
{
    [Header("Shooting")]
    public float MaxDistance;
    public float FireRate = 0.25f;
    public LayerMask HitMask;
    public Vector3 Spread = new Vector3(0.1f, 0.1f, 0.1f);

    
}
