using UnityEngine;

[CreateAssetMenu(menuName = "Player Data")] //Create a new playerData object by right clicking in the Project Menu then Create/Player/Player Data and drag onto the player
public class PlayerData : ScriptableObject
{
    [SerializeField] private float _appliedMovementSpeed;
    [SerializeField] private float _appliedJumpForce;
    [SerializeField] private float _coyoteTimer;
    [SerializeField] private float _bufferTime;

    public float CoyoteTimer
    {
        get { return _coyoteTimer; }
    }
    public float AppliedMovementSpeed
    {
        get { return _appliedMovementSpeed; }
    }
    public float AppliedJumpForce
    {
        get { return _appliedJumpForce; }
    }
    public float BufferTimer
    {
        get => _bufferTime;
    }
}
