using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultState : State
{
    private bool hiding;


    public DefaultState(PlayerControllerScr player, StateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("hi");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void HandleInput()
    {
        base.HandleInput();
        hiding = Input.GetKeyDown(KeyCode.E);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (player.canHide && hiding)
        {
            Debug.Log("� ��������� �����������");
            stateMachine.ChangeState(player.hidingState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        player.PlayerControl();
    }

    

    

    
}
