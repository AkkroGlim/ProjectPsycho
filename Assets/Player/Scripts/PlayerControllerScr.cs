using UnityEngine;

public class PlayerControllerScr : MonoBehaviour
{
    private Animator playerAnimator;
    private Rigidbody playerRigid;
    private StateMachine moveSM;
    private Vector3 hidingPosition;
    private Vector3 hidingScale;
    private float defaultPlayerPositionX;
    private Vector3 defaultPlayerScale;
    private bool hideFlag;
    private float speed = 70f;
    private float walkSpeed = 70f;
    private float runSpeed = 140f;

    private float mousePositionX;
    private float newMousePositionX;
    
    public DefaultState defaultState;
    public HidingState hidingState;
    public TurnState turnState;


    void Start()
    {
        playerRigid = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        TriggerEvent.triggerEvent.AddListener(HidingControl);
        defaultPlayerPositionX = transform.position.x;
        defaultPlayerScale = transform.localScale;
        mousePositionX = newMousePositionX = Mathf.Sign(Input.mousePosition.x - Screen.width / 2);

        moveSM = new StateMachine();
        defaultState = new DefaultState(this, moveSM);
        hidingState = new HidingState(this, moveSM);
        turnState = new TurnState(this, moveSM);

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

    public void Move(float moveDirection, float faceDirection)
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = runSpeed * moveDirection;
            moveDirection = moveDirection * 2;
        }
        else
        {
            speed = walkSpeed * moveDirection;
        }

        playerAnimator.SetFloat("Move", moveDirection);

        Vector3 targetVelocity = speed * transform.forward * Time.deltaTime * faceDirection;
        targetVelocity.y = playerRigid.velocity.y;
        playerRigid.velocity = targetVelocity;

        if (speed != 0)
        {
            PlayerEvent.moveEvent.Invoke(true);
        }
        else
        {
            PlayerEvent.moveEvent.Invoke(false);
        }
    }

    public void Turn(float direction)
    {
        transform.Rotate(0f, -500 * direction * Time.deltaTime, 0f);
    }

    public bool isPlayerTurnAround()
    {
        newMousePositionX = Mathf.Sign(Input.mousePosition.x - Screen.width / 2);
        if (mousePositionX != newMousePositionX)
        {
            mousePositionX = newMousePositionX;
            return true;
        }
        return false;
    }

    public void ActiveTurnAnimation()
    {
        playerAnimator.SetTrigger("Direction");
    }
}
