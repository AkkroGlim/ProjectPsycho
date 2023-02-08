using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyData/Enemy Move", fileName = "EnemyMove")]

public class EnemyMoveSo : ScriptableObject
{
    [SerializeField] private float speed;

    public float Speed { get { return speed; } }
}
