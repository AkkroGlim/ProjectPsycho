using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenuScr : MonoBehaviour
{
    private GameObject CurrentActiveMenu;
    [SerializeField] private GameObject BasedMenu;

    private void OnEnable()
    {
        CurrentActiveMenu = BasedMenu;
        CurrentActiveMenu.SetActive(true);
    }

    private void OnDisable()
    {
        CurrentActiveMenu.SetActive(false);
    }

    public void ChangeSettingsMenu(GameObject button)
    {
        CurrentActiveMenu.SetActive(false);
        CurrentActiveMenu = button.transform.GetChild(1).gameObject;
        CurrentActiveMenu.SetActive(true);
    }
}
