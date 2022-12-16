using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnState : State
{
    public TurnState(PlayerControllerScr player, StateMachine stateMachine) : base(player, stateMachine) { }

    private Vector3 targetAngle;
    private float direction;


    public override void Enter()
    {
        base.Enter();
        direction = Mathf.Sign(Input.mousePosition.x - Screen.width / 2);
        targetAngle = player.transform.localEulerAngles + new Vector3(0f, -180f, 0f) * direction; 
        targetAngle.y = Mathf.Round(targetAngle.y);
        player.ActiveTurnAnimation();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void HandleInput()
    {
        base.HandleInput();
        if (Vector3.Distance(player.transform.localEulerAngles, targetAngle) < 10)
        {
            player.transform.localEulerAngles = targetAngle;
            stateMachine.ChangeState(player.defaultState);
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.Turn(direction);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();       
    }
}
