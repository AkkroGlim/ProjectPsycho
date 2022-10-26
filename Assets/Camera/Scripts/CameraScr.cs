using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScr : MonoBehaviour
{
    [SerializeField] private Transform player;
    private int distance = 10;
    private float focusRadius = 1f;
    private Vector3 focusPoint;
    private float focusCentering = 0.5f;
    private bool motherfuckingFlag = false;

    private void Awake()
    {
        focusPoint = player.position;
    }

    private void LateUpdate()
    {
        UpdateFocusPoint();
        Vector3 lookDirection = transform.forward;
        transform.localPosition = focusPoint - lookDirection * distance;
    }

    private void UpdateFocusPoint()
    {
        Vector3 targetPoint = player.position;
        float distance = Vector3.Distance(targetPoint, focusPoint);
        float t = 1f;
        if (distance > 0.01f && !motherfuckingFlag)
        {
            t = Mathf.Pow(1f - focusCentering, Time.unscaledDeltaTime);
        }
        if (distance > focusRadius)
        {
            motherfuckingFlag = true;
        }
        if (motherfuckingFlag)
        {
            t = Mathf.Min(t, focusRadius / distance);
        }
        focusPoint = Vector3.Lerp(targetPoint, focusPoint, t);
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