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
    private float cameraSpeedMultiplier = 0.8f;
    private delegate void MoveVariant();
    MoveVariant moveVar;

    private void Awake()
    {
        focusPoint = player.position;
        PlayerEvent.moveEvent.AddListener(MoveChoice);
        moveVar += MoveCamera;
    }

    private void LateUpdate()
    {        
        if(Time.timeScale > 0)
        {
            moveVar();
        }
        

        Vector3 lookDirection = transform.forward;
        transform.position = focusPoint - lookDirection * distance;
    }

    private void SmartMoveCamera()
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
                if (transform.position.z > targetPoint.z)
                {
                    targetPoint -= new Vector3(0f, 0f, cameraSpeedMultiplier);
                }
                else
                {
                    targetPoint += new Vector3(0f, 0f, cameraSpeedMultiplier);
                }               
                focusPoint = Vector3.Lerp(targetPoint, focusPoint, t);
            }
        }        
    }

    private void MoveCamera()
    {
        Vector3 targetPoint = player.position;
        t = Mathf.Pow(1f - focusCentering, Time.unscaledDeltaTime);
        focusPoint = Vector3.Lerp(targetPoint, focusPoint, t);
    }

    private void MoveChoice(bool i)
    {
        if (i)
        {
            moveVar = null;         
            moveVar += SmartMoveCamera;
        }
        else
        {
            moveVar = null;
            focusFlag = false;
            t = 1f;
            moveVar += MoveCamera;
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