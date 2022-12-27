using UnityEngine;

public class DefaultState : State
{
    private float horizontalInput;
    private bool hiding;

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
        hiding = Input.GetKeyDown(KeyCode.E);
        horizontalInput = Input.GetAxis("Horizontal");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player.Centring();

        if (player.IsPlayerTurnAround())
        {
            stateMachine.ChangeState(player.turnState);
        }

        if(player.mayInteract && Input.GetKeyDown(KeyCode.Space) && player.MayVault())
        {
            stateMachine.ChangeState(player.vaultState);
        }

        if (player.mayInteract && Input.GetKeyDown(KeyCode.E))
        {
            stateMachine.ChangeState(player.hidingState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();       
        player.Move(horizontalInput , Mathf.Sign(Input.mousePosition.x - Screen.width / 2));                   
    }  
}
