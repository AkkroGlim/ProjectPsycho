using UnityEngine;

public class PlayerControllerScr : MonoBehaviour
{
    private Rigidbody playerRigid;
    private StateMachine moveSM;
    private Vector3 hidingPosition;
    private Vector3 hidingScale;
    private float defaultPlayerPositionX;
    private Vector3 defaultPlayerScale;
    private bool hideFlag;
    public DefaultState defaultState;
    public HidingState hidingState;


    void Start()
    {
        playerRigid = GetComponent<Rigidbody>();
        TriggerEvent.triggerEvent.AddListener(HidingControl);
        defaultPlayerPositionX = transform.position.x;
        defaultPlayerScale = transform.localScale;
        moveSM = new StateMachine();
        defaultState = new DefaultState(this, moveSM);
        hidingState = new HidingState(this, moveSM);

        moveSM.Initialize(defaultState);
    }

    private void Update()
    {
        moveSM.currentState.HandleInput();
        moveSM.currentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        moveSM.currentState.PhysicsUpdate();
    }

    private void HidingControl(Vector3 hidingPosition, Vector3 hidingScale)
    {
        this.hidingPosition = new Vector3(hidingPosition.x, transform.position.y, hidingPosition.z);
        this.hidingScale = hidingScale;
    }

    public void HideMove()
    {
        Vector3 targetPoint;
        if (!hideFlag)
        {
            targetPoint = new Vector3(defaultPlayerPositionX, transform.position.y, transform.position.z);
        }
        else
        {
            targetPoint = hidingPosition;
        }

        float distance = Vector3.Distance(transform.position, targetPoint);

        if (distance < 0.03f)
        {
            transform.position = targetPoint;
        }
        else
        {
            float t = Mathf.Pow(0.1f, Time.unscaledDeltaTime);
            transform.position = Vector3.Lerp(targetPoint, transform.position, t);
        }
    }

    public bool isHideOver()
    {
        if (transform.position.x == defaultPlayerPositionX && transform.localScale == defaultPlayerScale)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public Vector3 hidingChecker()
    {
        return hidingScale;
    }

    public void HideMoveFlag()
    {
        hideFlag = !hideFlag;
    }

    public void Move(float speed)
    {
        Vector3 targetVelocity = speed * transform.forward * Time.deltaTime;
        targetVelocity.y = playerRigid.velocity.y;
        playerRigid.velocity = targetVelocity;

        if (speed != 0f)
        {
            PlayerEvent.moveEvent.Invoke(true);
        }
        else
        {
            PlayerEvent.moveEvent.Invoke(false);
        }
    }
}
