using UnityEngine;

public class HidingState : State
{   
    private bool unhiding;

    public HidingState(PlayerControllerScr player, StateMachine stateMachine) : base(player, stateMachine)
    {
    }

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
        unhiding = Input.GetKeyUp(KeyCode.E);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (unhiding)
        {
            stateMachine.ChangeState(player.defaultState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
