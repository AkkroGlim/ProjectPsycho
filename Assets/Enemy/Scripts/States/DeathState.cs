using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : EnemyState
{
    public DeathState(Enemy enemy, EnemyStateMachine sm) : base(enemy, sm) { }

    public override void Enter()
    {
        base.Enter();
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
    }

    public override void PhisicsUpdate()
    {
        base.PhisicsUpdate();
    }
}
