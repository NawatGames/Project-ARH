using UnityEngine;

[CreateAssetMenu(menuName = "Player Data")] //Create a new playerData object by right clicking in the Project Menu then Create/Player/Player Data and drag onto the player
public class PlayerData : ScriptableObject
{
    [Header("Walk Variables")]
    [SerializeField] private float _appliedMovementSpeed;
    
    [Space(5)]
    
    [Header("Jump Variables")]
    [SerializeField] private float _appliedJumpForce;
    [SerializeField] private float _coyoteTimer;
    [SerializeField] private float _bufferTime;

    
    // Getters and Setter
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
