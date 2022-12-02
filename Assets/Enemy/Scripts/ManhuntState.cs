using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManhuntState : EnemyState
{
    private float attackDistance;
    private float manhuntDistance;
    private Transform playerTransform;

    public ManhuntState(Enemy enemy, EnemyStateMachine sm) : base(enemy, sm) { }

    public override void Enter()
    {
        base.Enter();
        playerTransform = GameObject.FindGameObjectWithTag("PLayer").transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void HandleInput()
    {
        base.HandleInput();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(Vector3.Distance(enemy.transform.position, playerTransform.position) < attackDistance)
        {
            sm.ChangeState(enemy.attackState);
        }

    }

    public override void PhisicsUpdate()
    {
        base.PhisicsUpdate();
        if (Vector3.Distance(enemy.transform.position, playerTransform.position) < manhuntDistance)
        {
            enemy.Manhunt(Vector3.Normalize(enemy.transform.position - playerTransform.position).z);
        }
    }
}
