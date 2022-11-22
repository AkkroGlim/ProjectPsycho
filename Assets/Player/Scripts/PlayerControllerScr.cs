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
    public Transform hidingPosition;
    public DefaultState defaultState;
    public HidingState hidingState;


    void Start()
    {
        speed = walkSpeed;
        playerRigid = GetComponent<Rigidbody>();
        TriggerEvent.triggerEvent.AddListener(HidingControl);
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

    private void HidingControl(Transform hidingPosition)
    {
        this.hidingPosition = hidingPosition;
    }

    public void Hiding()
    {
        transform.position = hidingPosition.position;
        transform.localScale = new Vector3(transform.localScale.x , hidingPosition.localScale.y , transform.localScale.z);
    }

}
