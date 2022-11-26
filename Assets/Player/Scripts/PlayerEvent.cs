using UnityEngine;
using UnityEngine.Events;

public class PlayerEvent : MonoBehaviour
{
    public static UnityEvent<bool> moveEvent = new UnityEvent<bool>();
}
