using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
[RequireComponent(typeof(Text))]

public class AmmoDisplay : MonoBehaviour
{
    [SerializeField] private GunSelector GunSelector;
    private Text AmmoText;

    private void Awake()
    {
        AmmoText = GetComponent<Text>();
        if(GunSelector.ActiveWeapon.WeaponType != WeaponType.Range) //Доработать
        {
            AmmoText.gameObject.SetActive(false);
        }
    }
    void Update()
    {
        AmmoText.text = $"{GunSelector.ActiveWeapon.RangeScrObj.AmmoConfig.CurrentClipAmmo} / {GunSelector.ActiveWeapon.RangeScrObj.AmmoConfig.CurrentAmmo}";
    }
}
