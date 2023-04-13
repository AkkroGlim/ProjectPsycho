
public class DefaultState : State
{
    public DefaultState(PlayerControllerScr player, StateMachine stateMachine) : base(player, stateMachine) { }


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
        if (player.IsAiming() && !player.IsSprint())
        {
            stateMachine.ChangeState(player.aimingState);
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player.Gravity();
        player.Move();
        player.Turn();
        player.actionWithWeapon?.Invoke();
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }  
}
