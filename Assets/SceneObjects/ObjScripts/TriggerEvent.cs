using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    public static UnityEvent<Transform> triggerEvent = new UnityEvent<Transform>();
}
