using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New EnemyData", menuName = "Enemy", order = 51)]

public class EnemyData : ScriptableObject
{
    [SerializeField] private float speed;

    public float Speed { get { return speed; } }
}
