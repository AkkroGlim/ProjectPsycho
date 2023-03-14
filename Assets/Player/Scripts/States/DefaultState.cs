using UnityEngine;

public class DefaultState : State
{
    private float horizontalInput;

    public DefaultState(PlayerControllerScr player, StateMachine stateMachine) : base(player, stateMachine) { }


    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        horizontalInput = 0f;
    }

    public override void HandleInput()
    {
        base.HandleInput();
        horizontalInput = Input.GetAxis("Horizontal");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player.Gravity();
        player.Move();
        player.actionWithWeapon?.Invoke();
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();       
                           
    }  
}
