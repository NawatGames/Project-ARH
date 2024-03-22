using UnityEngine;

public class AlienEatPointScan : MonoBehaviour
{
    [SerializeField] private AlienStateMachine alienStateMachine;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!alienStateMachine.hasStoredObject && !alienStateMachine.isEdibleInRange && other.CompareTag("Food"))
        {
            alienStateMachine.currentEdibleObject = other.gameObject;

            alienStateMachine.isEdibleInRange = true;
            //Debug.Log("Food Is In Range!");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!alienStateMachine.hasStoredObject)
        {
            alienStateMachine.isEdibleInRange = false;
            alienStateMachine.currentEdibleObject = other.gameObject;
        }
    }
}