using UnityEngine;

public class ShelterTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Vector3 hidingPosition = new Vector3(transform.position.x, transform.position.y, 0f);
        TriggerEvent.triggerEvent.Invoke(null);
        Debug.Log("Можно притаица");
    }

    private void OnTriggerExit(Collider other)
    {
        TriggerEvent.triggerEvent.Invoke(null);
        Debug.Log("Незя притаица");
    }
}
