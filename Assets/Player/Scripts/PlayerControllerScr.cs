using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScr : MonoBehaviour
{
    private Rigidbody playerRigid;
    private float speed;
    private float walkSpeed = 5.6f;
    private float runSpeed = 6.6f;
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
        speed = walkSpeed;
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

    public void PlayerControl()
    {
        float horizontal = Input.GetAxis("Horizontal");

        if (horizontal != 0f)
        {
            PlayerEvent.moveEvent.Invoke(true);
        }
        else
        {
            PlayerEvent.moveEvent.Invoke(false);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = runSpeed;
        }
        else
        {
            speed = walkSpeed;
        }
        playerRigid.AddRelativeForce(0f, 0f, horizontal * speed * Time.deltaTime, ForceMode.VelocityChange);


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
            transform.localScale = defaultPlayerScale;
        }
        else
        {
            targetPoint = hidingPosition;
            if(hidingScale.y < 2f)
            {
                transform.localScale = new Vector3(1f, 0.5f, 1f);
            }
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
        if(transform.position.x == defaultPlayerPositionX && transform.localScale == defaultPlayerScale)
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
}
