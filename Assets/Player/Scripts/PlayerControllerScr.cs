using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScr : MonoBehaviour
{
    private Rigidbody playerRigid;
    private float speed = 700;
    void Start()
    {
        playerRigid = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        playerRigid.AddForce(0f, 0f, horizontal * speed * Time.deltaTime, ForceMode.Acceleration);
    }
}
