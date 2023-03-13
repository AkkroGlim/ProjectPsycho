using System.Collections;
using System.Collections.Generic;

using System.Linq;
using UnityEngine;

[DisallowMultipleComponent]

public class GunSelector : MonoBehaviour
{   
    [SerializeField] private Transform WeaponParent;
    [SerializeField] private PlayerIK InverseKinematics;


    [Space]
    [Header("Runtime Filled")]
    public WeaponSO ActiveWeapon;

    void Start()
    {       
        if(ActiveWeapon == null)
        {
            return;
        }

        switch (ActiveWeapon.WeaponType)
        {
            case WeaponType.Range:
                ActiveWeapon.MeleeScrObj = null;
                ActiveWeapon.RangeScrObj.Spawn(this, WeaponParent);
                break;
            case WeaponType.Melee:
                ActiveWeapon.RangeScrObj = null;
                ActiveWeapon.MeleeScrObj.Spawn(WeaponParent);
                break;
            case WeaponType.Empty:
                ActiveWeapon.RangeScrObj = null;
                ActiveWeapon.MeleeScrObj = null;
                break;
        }
       

        Transform[] allChildren = WeaponParent.GetComponentsInChildren<Transform>();
        InverseKinematics.LeftElbowIKTarget = allChildren.FirstOrDefault(child => child.name == "LeftElbow");
        InverseKinematics.RightElbowIKTarget = allChildren.FirstOrDefault(child => child.name == "RightElbow");
        InverseKinematics.LeftHandIKTarget = allChildren.FirstOrDefault(child => child.name == "LeftHand");
        InverseKinematics.RightHandIKTarget = allChildren.FirstOrDefault(child => child.name == "RightHand");
    }
}
