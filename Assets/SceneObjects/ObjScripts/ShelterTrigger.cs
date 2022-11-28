using UnityEngine;

public class ShelterTrigger : MonoBehaviour
{
    [SerializeField] Transform shelter;
    private Vector3 distance;
    private Vector3 leftDistance = new Vector3(0f, 0f, 1.1f);
    private Vector3 rightDistance = new Vector3(0f, 0f, -1.1f);
    private void Start()
    {
        if (shelter.position.z > transform.position.z)
        {
            distance = leftDistance;
        }
        else
        {
            distance = rightDistance;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Vector3 hidingPosition = shelter.position - distance;
        Vector3 hidingScale = shelter.localScale;
        TriggerEvent.triggerEvent.Invoke(hidingPosition, hidingScale);
        HidingHint.HintToggle();
    }

    private void OnTriggerExit(Collider other)
    {
        TriggerEvent.triggerEvent.Invoke(Vector3.zero, Vector3.zero);
        HidingHint.HintToggle();
    }
}
