using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    public static UnityEvent<Vector3, Vector3, bool> triggerEvent = new UnityEvent<Vector3, Vector3, bool>();
}
