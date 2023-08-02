using UnityEngine;
using UnityEngine.Events;

public class StoneGetCollected : MonoBehaviour
{
    [SerializeField] private UnityEvent OnStoneCollected;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !(other.isTrigger))
        {
            OnStoneCollected.Invoke();
            gameObject.SetActive(false);
        }
    }

    public void TestMethod()
    {
        Debug.Log("Player coletou a rocha");
    }
}
