using UnityEngine;

public class ShelterTrigger : MonoBehaviour
{
    [SerializeField] Transform shelter;
    [SerializeField] GameObject hint;
    private Vector3 distance = new Vector3(0f, 0f, 1f);

    private void OnTriggerEnter(Collider other)
    {
        Vector3 hidingPosition = shelter.position - distance;
        Vector3 hidingScale = shelter.localScale;
        TriggerEvent.triggerEvent.Invoke(hidingPosition, hidingScale);
        hint.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        TriggerEvent.triggerEvent.Invoke(Vector3.zero, Vector3.zero);
        hint.SetActive(false);
    }
}
