using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShootConf", menuName = "WeaponData/Range/ShootConf")]

public class ShootConfigSO : ScriptableObject
{
    public float FireRate = 0.25f;
    public LayerMask HitMask;
    public Vector3 Spread = new Vector3(0.1f, 0.1f, 0.1f);
    delegate bool Chipipiu(int i);
    Chipipiu chipipiu;
    private void OnEnable()
    {
        chipipiu = Input.GetMouseButton;
        
    }

}
