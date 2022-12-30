using UnityEngine;

public class VaultState : State
{
    public VaultState(PlayerControllerScr player, StateMachine stateMachine) : base(player, stateMachine) { }

    private Vector3 targetVaultPosition;

    public override void Enter()
    {
        base.Enter();
        player.PlayerTangibilityTogle();
        targetVaultPosition = player.PrepareToVault();
        player.ActiveVaultAnimation();
        HidingHint.HintToggle();
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
        if (player.transform.position == targetVaultPosition)
        {
            stateMachine.ChangeState(player.defaultState);
        }
        player.Vault();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
