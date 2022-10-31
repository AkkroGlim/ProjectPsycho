using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraScr : MonoBehaviour
{
    [SerializeField] private Transform player;
    private int distance = 10;
    private float focusRadius = 1f;
    private Vector3 focusPoint;
    private float focusCentering = 0.7f;
    private bool focusFlag = false;
    private float t = 1f;
    private int reloadCamera = 0;
    private float cameraSpeedMultiplier = 1.3f;

    private void Awake()
    {
        focusPoint = player.position;
    }

    private void FixedUpdate()
    {
        MoveChecker();
    }

    private void LateUpdate()
    {
        UpdateFocusPoint();

        

        Vector3 lookDirection = transform.forward;
        transform.position = focusPoint - lookDirection * distance;
    }

    private void UpdateFocusPoint()
    {
        Vector3 targetPoint = player.position;
        float distance = Vector3.Distance(targetPoint, focusPoint);

        if (distance > focusRadius && !focusFlag)
        {
            focusFlag = true;
            t = 1f;
        }
        if (distance > 0.05f && !focusFlag)
        {
            t = Mathf.Pow(1f - focusCentering, Time.unscaledDeltaTime);
            focusPoint = Vector3.Lerp(targetPoint, focusPoint, t);
        }
        else if (focusFlag)
        {
            if (distance < 0.05f)
            {
                focusPoint = targetPoint;
            }
            else
            {
                t -= 0.00005f;
                targetPoint = new Vector3(targetPoint.x, targetPoint.y, targetPoint.z * cameraSpeedMultiplier);
                focusPoint = Vector3.Lerp(targetPoint, focusPoint, t);
            }
        }


       
    }

    private void MoveChecker()
    {
        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            reloadCamera++;
            if (reloadCamera > 50)
            {
                reloadCamera = 0;
                focusFlag = false;
                t = 1f;
            }
        }
    }
}





//Vector3 targetPoint = player.position;
//float distance = Vector3.Distance(targetPoint, focusPoint);
//float t = 1f;
//if (distance > 0.01f && focusCentering > 0f)
//{
//    t = Mathf.Pow(1f - focusCentering, Time.unscaledDeltaTime);
//}
//if (distance > focusRadius)
//{
//    t = Mathf.Min(t, focusRadius / distance);
//}
//focusPoint = Vector3.Lerp(targetPoint, focusPoint, t);