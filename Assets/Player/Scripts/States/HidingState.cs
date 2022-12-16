using UnityEngine;

public class HidingState : State
{   
    private bool unhiding;

    public HidingState(PlayerControllerScr player, StateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        HidingHint.HintToggle();
    }

    public override void Exit()
    {
        base.Exit();
        HidingHint.HintToggle();
        player.HideMoveFlag();
    }

    public override void HandleInput()
    {
        base.HandleInput();
        unhiding = Input.GetKeyUp(KeyCode.E);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (unhiding)
        {
            stateMachine.ChangeState(player.defaultState);
        }
        player.HideMove();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
