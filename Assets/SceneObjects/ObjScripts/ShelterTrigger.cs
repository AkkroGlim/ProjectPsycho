using UnityEngine;

public class ShelterTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        TriggerEvent.triggerEvent.Invoke(true);
        Debug.Log("����� ��������");
    }

    private void OnTriggerExit(Collider other)
    {
        TriggerEvent.triggerEvent.Invoke(false);
        Debug.Log("���� ��������");
    }
}
