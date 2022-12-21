using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    public static UnityEvent<Vector3> triggerEvent = new UnityEvent<Vector3>();
}
