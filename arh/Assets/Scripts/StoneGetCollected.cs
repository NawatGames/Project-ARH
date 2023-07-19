using UnityEngine;
using UnityEngine.Events;

public class StoneGetCollected : MonoBehaviour
{
    [SerializeField] private UnityEvent stoneCollected;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !(other.isTrigger))
        {
            stoneCollected.Invoke();
            gameObject.SetActive(false);
        }
    }

    public void TestMethod()
    {
        Debug.Log("Player coletou a rocha");
    }
}
