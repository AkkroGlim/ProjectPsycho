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
        if (other.gameObject.tag.Equals("Player"))
        {
            Vector3 hidingPosition = shelter.position - distance;
            TriggerEvent.triggerEvent.Invoke(hidingPosition);
            HidingHint.HintToggle();
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            TriggerEvent.triggerEvent.Invoke(Vector3.zero);
            HidingHint.HintToggle();
        }
        
    }
}
