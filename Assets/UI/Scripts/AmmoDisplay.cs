using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
[RequireComponent(typeof(Text))]

public class AmmoDisplay : MonoBehaviour
{
    [SerializeField] private PlayerGunSelector GunSelector;
    private Text AmmoText;

    private void Awake()
    {
        AmmoText = GetComponent<Text>();
    }
    void Update()
    {
        AmmoText.text = $"{GunSelector.ActiveWeapon.AmmoConfig.CurrentClipAmmo} / {GunSelector.ActiveWeapon.AmmoConfig.CurrentAmmo}";
    }
}
