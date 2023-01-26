using UnityEngine;

public class ShelterTrigger : MonoBehaviour
{
    [SerializeField] private Transform shelter;
    private static Vector3 DistanceToShelter = new Vector3(0f, 0f , 1.1f);

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            TriggerEvent.triggerEvent.Invoke(shelter.position, DistanceToShelter, true);
            HidingHint.HintToggle();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            TriggerEvent.triggerEvent.Invoke(Vector3.zero, Vector3.zero, false);
            HidingHint.HintToggle();
        }        
    }
}
