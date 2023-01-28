using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[DisallowMultipleComponent]

public class PlayerGunSelector : MonoBehaviour
{
    [SerializeField] private RangeType Gun;
    [SerializeField] private Transform GunParent;
    [SerializeField] private List<RangeSO> Weapons;
    [SerializeField] private PlayerIK InverseKinematics;

    [Space]
    [Header("Runtime Filled")]
    public RangeSO ActiveWeapon;
    void Start()
    {
        RangeSO weapon = Weapons.Find(weapon => weapon.Type == Gun);

        if (weapon == null)
        {
            Debug.LogError($"Не найден RangeSO для GunType: {weapon}");
            return;
        }

        ActiveWeapon = weapon;
        weapon.Spawn(this, GunParent);

        Transform[] allChildren = GunParent.GetComponentsInChildren<Transform>();
        InverseKinematics.LeftElbowIKTarget = allChildren.FirstOrDefault(child => child.name == "LeftElbow");
        InverseKinematics.RightElbowIKTarget = allChildren.FirstOrDefault(child => child.name == "RightElbow");
        InverseKinematics.LeftHandIKTarget = allChildren.FirstOrDefault(child => child.name == "LeftHand");
        InverseKinematics.RightHandIKTarget = allChildren.FirstOrDefault(child => child.name == "RightHand");
    }
}
