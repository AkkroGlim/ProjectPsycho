using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WeaponData/Weapon", fileName = "WeaponSO")]

public class WeaponSO : ScriptableObject
{
    public RangeSO RangeScrObj;
    public MeleeSO MeleeScrObj;
    public WeaponType WeaponType;
}
