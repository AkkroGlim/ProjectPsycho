using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Melee", menuName = "WeaponData/Melee")]

public class MeleeSO : ScriptableObject
{
    public new string name;
    public MeleeType Type;
    public float damage;
    public float attackSpeed;

    public GameObject ModelPrefab;
    private GameObject model;

    public Vector3 SpawnPosition;
    public Vector3 SpawnRotation;
    

    public void Spawn(Transform parent)
    {
        model = Instantiate(ModelPrefab);
        model.transform.SetParent(parent, false);
        model.transform.localPosition = SpawnPosition;
        model.transform.localRotation = Quaternion.Euler(SpawnRotation);                
    }
}
