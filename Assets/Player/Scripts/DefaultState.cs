using UnityEngine;

public class DefaultState : State
{
    private bool hiding;
    private bool isHideOver;
    private float speed = 70f;
    private float horizontalInput;
    private float walkSpeed = 70f;
    private float runSpeed = 100f;

    public DefaultState(PlayerControllerScr player, StateMachine stateMachine) : base(player, stateMachine)
    {
    }

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

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = runSpeed;
        }
        else
        {
            speed = walkSpeed;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (isHideOver)
        {
            player.Move(horizontalInput * speed);
        }       
    }

    

    

    
}
