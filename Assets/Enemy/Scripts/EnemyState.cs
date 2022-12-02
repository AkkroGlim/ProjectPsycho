using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState
{
    protected Enemy enemy;
    protected EnemyStateMachine sm;

    protected EnemyState(Enemy enemy, EnemyStateMachine sm)
    {
        this.enemy = enemy;
        this.sm = sm;
    }

    public virtual void Enter() { }

    public virtual void Exit() { }

    public virtual void HandleInput() { }

    public virtual void LogicUpdate() { }

    public virtual void PhisicsUpdate() { }
}
