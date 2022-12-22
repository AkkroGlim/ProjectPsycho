using UnityEngine;

public class PlayerControllerScr : MonoBehaviour
{
    private Animator playerAnimator;
    private Rigidbody playerRigid;
    private StateMachine moveSM;

    private float defaultPlayerPositionX;
    public bool mayVault;
    public bool mayHide;
    private Vector3 shelterPosition;

    private float speed = 70f;
    private float walkSpeed = 70f;
    private float speedMultiplier = 1;

    private float mousePositionX;
    private float newMousePositionX;

    public DefaultState defaultState;
    public HidingState hidingState;
    public TurnState turnState;
    public VaultState vaultState;

    void Start()
    {
        playerRigid = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        TriggerEvent.triggerEvent.AddListener(InteractZone);
        defaultPlayerPositionX = transform.position.x;
        mayHide = false;

        mousePositionX = 1f;
        newMousePositionX = Mathf.Sign(Input.mousePosition.x - Screen.width / 2);
        playerAnimator.SetFloat("DirectionFloat", newMousePositionX);

        moveSM = new StateMachine();
        defaultState = new DefaultState(this, moveSM);
        hidingState = new HidingState(this, moveSM);
        turnState = new TurnState(this, moveSM);
        vaultState = new VaultState(this, moveSM);

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

    public void Centring()
    {
        if(defaultPlayerPositionX != transform.position.x)
        {
            Vector3 target = transform.position;
            target.x = defaultPlayerPositionX;
            transform.position = Vector3.MoveTowards(transform.position, target, 3 * Time.deltaTime);
        }
    }

    public void Move(float moveDirection, float faceDirection)
    {
        if (Input.GetKey(KeyCode.LeftShift) && moveDirection == faceDirection)
        {
            speedMultiplier = Mathf.Clamp(speedMultiplier + 0.04f, 1f, 2f);
        }
        else
        {
            speedMultiplier = Mathf.Clamp(speedMultiplier - 0.04f, 1f, 2f);
        }

        speed = walkSpeed * moveDirection * speedMultiplier;
        moveDirection = moveDirection * speedMultiplier;
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

    public bool IsPlayerTurnAround()
    {
        newMousePositionX = Mathf.Sign(Input.mousePosition.x - Screen.width / 2);
        if (mousePositionX != newMousePositionX)
        {
            mousePositionX = newMousePositionX;
            playerAnimator.SetFloat("DirectionFloat", mousePositionX);
            return true;
        }      
        return false;
    }

    public void ActiveTurnAnimation()
    {
        playerAnimator.SetTrigger("Turn");
    }

    public void ActiveVaultAnimation()
    {
        playerAnimator.SetTrigger("Vault");
    }

    public void Hiding()
    {
        shelterPosition.y = transform.position.y;
        transform.position = Vector3.MoveTowards(transform.position, shelterPosition, 3 * Time.deltaTime);
    }

    private void InteractZone(Vector3 shelterPosition)
    {
        this.shelterPosition = shelterPosition;
        mayHide = !mayHide;        
    }
}
