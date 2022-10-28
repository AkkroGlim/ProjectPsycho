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

    private void Awake()
    {
        focusPoint = player.position;
    }

    private void FixedUpdate()
    {
        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            reloadCamera++;
            if(reloadCamera > 20)
            {
                reloadCamera = 0;
                focusFlag = false;
                t = 1f;
            }
        }
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
        
        if (distance > focusRadius)
        {
            focusFlag = true;
            t = 1;
        }
        if (distance > 0.01f && !focusFlag)
        {
            t = Mathf.Pow(1f - focusCentering, Time.unscaledDeltaTime);
            focusPoint = Vector3.Lerp(targetPoint, focusPoint, t);
        }
        else
        {
            t -= 0.006f;
            focusPoint = Vector3.Lerp(targetPoint, focusPoint, t);            
        }
        //focusPoint = Vector3.Lerp(targetPoint, focusPoint, t);
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