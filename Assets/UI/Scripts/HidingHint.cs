using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingHint : MonoBehaviour
{
    [SerializeField] private GameObject hint;
    private static GameObject hintToggle;
    private void Start()
    {
        hintToggle = hint;
    }
    public static void HintToggle()
    {
        hintToggle.SetActive(!hintToggle.activeSelf);
    }
}
