using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscMenu : MonoBehaviour
{
    [SerializeField] private GameObject escMenu;


    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            OnEscMenu();
        }
    }

    private void OnEscMenu()
    {
        
        if (!escMenu.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
        
        escMenu.SetActive(!escMenu.activeSelf);
    }
}
