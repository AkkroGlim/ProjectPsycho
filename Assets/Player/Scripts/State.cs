
public abstract class State
{
    protected PlayerControllerScr player;
    protected StateMachine stateMachine;

    protected State(PlayerControllerScr player, StateMachine stateMachine)
    {
        this.player = player;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter()
    {

    }

    public virtual void Exit()
    {
        player.HideMoveFlag();
    }

    public virtual void HandleInput()
    {

    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {

    }
}
