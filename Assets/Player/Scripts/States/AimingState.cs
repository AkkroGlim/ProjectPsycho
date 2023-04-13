using UnityEngine;
public class AimingState : State
{
    public AimingState(PlayerControllerScr player, StateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        player.ExitAiming();
    }

    public override void HandleInput()
    {
        base.HandleInput();
        if (!player.IsAiming() || player.IsSprint())
        {
            stateMachine.ChangeState(player.defaultState);
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player.Gravity();
        player.Move();
        player.Aiming();
        player.actionWithWeapon?.Invoke();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
