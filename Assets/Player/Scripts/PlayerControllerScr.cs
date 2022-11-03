using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScr : MonoBehaviour
{
    private Rigidbody playerRigid;
    private float speed = 650;
    void Start()
    {
        playerRigid = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        PlayerControl();
    }

    private void PlayerControl()
    {
        float horizontal = Input.GetAxis("Horizontal");
        playerRigid.AddForce(0f, 0f, horizontal * speed * Time.deltaTime, ForceMode.Acceleration);
        if(horizontal != 0f)
        {
            PlayerEvent.moveEvent.Invoke(true);
        }
        else
        {
            PlayerEvent.moveEvent.Invoke(false);
        }
    }
}
