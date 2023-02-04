using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WeaponData/Range/AmmoConf", fileName = "AmmoConfig")]

public class AmmoConfigSO : ScriptableObject
{
    public int MaxAmmo = 120;
    public int ClipSize = 30;

    public int CurrentAmmo = 120;
    public int CurrentClipAmmo = 30;

    public void Reload()
    {
        int maxReloadAmount = Mathf.Min(ClipSize, CurrentAmmo);
        int availableBulletsInCurrentClip = ClipSize - CurrentClipAmmo;
        int reloadAmmount = Mathf.Min(maxReloadAmount, availableBulletsInCurrentClip);

        CurrentClipAmmo = CurrentClipAmmo + reloadAmmount;
        CurrentAmmo -= reloadAmmount;
    }

    public bool CanReload()
    {
        return CurrentClipAmmo < ClipSize && CurrentAmmo > 0;
    }
}
