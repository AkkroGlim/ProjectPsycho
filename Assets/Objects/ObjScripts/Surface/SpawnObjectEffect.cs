using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Impact System/Spawn Object Effect", fileName = "SpawnObjectEffect")]

public class SpawnObjectEffect : ScriptableObject
{
    public GameObject Prefab;
    public float Probability = 1;
    public bool RandomizeRotation;
    public Vector3 RandomizedRotationMultiplier = Vector3.zero;
}
