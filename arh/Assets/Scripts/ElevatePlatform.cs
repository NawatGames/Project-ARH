using UnityEngine;

public class ElevatePlatform : MonoBehaviour
{
    public Transform upPosition; // The target position when moving up
    public Transform downPosition; // The target position when moving down
    public float moveSpeed = 2.0f; // Speed of movement

    private bool _isMovingUp = false;
    private bool _isMovingDown = false;
    

    private void FixedUpdate()
    {
        if (_isMovingUp)
            MoveToPosition(upPosition);
        else if (_isMovingDown)
            MoveToPosition(downPosition);
    }

    private void MoveToPosition(Transform targetPosition)
    {
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, step);

        if (Vector3.Distance(transform.position, targetPosition.position) < 0.01f)
        {
            if (targetPosition == upPosition)
                _isMovingUp = false;
            else if (targetPosition == downPosition)
                _isMovingDown = false;
        }
    }

    public void StartMovingUp()
    {
        _isMovingDown = false;
        _isMovingUp = true;
        Debug.Log("Player em cima da placa de pressão");
    }

    public void StartMovingDown()
    {
        _isMovingUp = false;
        _isMovingDown = true;
        Debug.Log("Player saiu da placa de pressão");
    }
    
}