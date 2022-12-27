using UnityEngine;

public class VaultState : State
{
    public VaultState(PlayerControllerScr player, StateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        player.ActiveVaultAnimation();
        player.PlayerTangibilityTogle();
    }

    public override void Exit()
    {
        base.Exit();
        player.PlayerTangibilityTogle();
    }

    public override void HandleInput()
    {
        base.HandleInput();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
