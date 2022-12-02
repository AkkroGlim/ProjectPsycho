using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyStateMachine esm;
    private float speed = 100f;

    public ManhuntState manhuntState;
    public AttackState attackState;

    void Start()
    {
        esm = new EnemyStateMachine();
        manhuntState = new ManhuntState(this, esm);
        attackState = new AttackState(this, esm);
    }


    void Update()
    {

    }

    public void Manhunt(float direction)
    {

    }
}
