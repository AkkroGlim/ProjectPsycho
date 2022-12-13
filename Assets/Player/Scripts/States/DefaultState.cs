using UnityEngine;

public class DefaultState : State
{
    private bool hiding;
    private bool isHideOver;
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
        hiding = Input.GetKeyDown(KeyCode.E);
        horizontalInput = Input.GetAxis("Horizontal");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (player.hidingChecker() != Vector3.zero && hiding)
        {
            stateMachine.ChangeState(player.hidingState);
        }

        isHideOver = player.isHideOver();

        if (!isHideOver)
        {
            player.HideMove();
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (isHideOver)
        {
            player.Move(horizontalInput , Mathf.Sign(Input.mousePosition.x - Screen.width / 2));           
        }
    }  
}
