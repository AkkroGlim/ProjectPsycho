using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScr : MonoBehaviour
{
    private Rigidbody playerRigid;
    private float speed;
    private float walkSpeed = 5.6f;
    private float runSpeed = 6.6f;
    void Start()
    {
        speed = walkSpeed;
        playerRigid = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        PlayerControl();
    }

    private void PlayerControl()
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
}
