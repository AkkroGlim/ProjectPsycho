using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManhuntState : EnemyState
{    
    public ManhuntState(Enemy enemy, EnemyStateMachine sm) : base(enemy, sm) { }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        enemy.RemoveEnemyVelocity();
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
            enemy.Manhunt(-Vector3.Normalize(enemy.transform.position - playerTransform.position).z);
        }
        else
        {
            enemy.RemoveEnemyVelocity();
        }
    }
}
