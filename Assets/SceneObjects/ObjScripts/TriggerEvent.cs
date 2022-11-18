using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    public static UnityEvent<bool> triggerEvent = new UnityEvent<bool>();
}
