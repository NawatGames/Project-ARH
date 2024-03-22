using UnityEngine;

public class EatBehavior : MonoBehaviour
{
    [SerializeField] private AlienStateMachine alienStateMachine;
    [SerializeField] private GameObject playerGameObject;

    [SerializeField] private GameObject edibleObject;

    [SerializeField] private bool objectIsInRange = false;
    [SerializeField] private bool objectWasEaten;

    [SerializeField] private float throwForce;
    private Rigidbody2D _edibleObjectRigidBody;


    private void Awake()
    {
        playerGameObject = GameObject.FindWithTag("Alien");
        alienStateMachine = playerGameObject.GetComponent<AlienStateMachine>();
    }

    public void EatObject()
    {
        if (objectWasEaten)
        {
            // Cospe o Objeto com uma certa força
            objectWasEaten = false;
            edibleObject.SetActive(true);

            if (alienStateMachine.Sprite.transform.localScale.x <= 0)
            {
                _edibleObjectRigidBody.velocity = new Vector2(throwForce * -1, _edibleObjectRigidBody.velocity.y);
                
            }
            else
            {
                _edibleObjectRigidBody.velocity = new Vector2(throwForce, _edibleObjectRigidBody.velocity.y);

            }

            //Debug.Log("Cuspi o Objeto");
        }
        else if (objectIsInRange && !objectWasEaten)
        {
            objectWasEaten = true;
            edibleObject.SetActive(false);
            _edibleObjectRigidBody = edibleObject.GetComponent<Rigidbody2D>();
            //Debug.Log("Comi o Objeto");
        }

    }

    void Update()
    {

        if (objectWasEaten)
        {
            edibleObject.transform.position = gameObject.transform.position;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!objectWasEaten && !objectIsInRange && other.CompareTag("Food"))
        {
            edibleObject = other.gameObject;

            objectIsInRange = true;
            //Debug.Log("Food Is In Range!");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!objectWasEaten)
        {
            objectIsInRange = false;
            edibleObject = null;
        }
    }
    private void OnEnable()
    {
        alienStateMachine.onEatEvent.AddListener(EatObject);
    }

    private void OnDisable()
    {
        alienStateMachine.onEatEvent.RemoveListener(EatObject);

    }
}