using UnityEngine;

[CreateAssetMenu(fileName = "Movement controller settings",menuName = "CoreTeamSDK/Movement controller/Settings")]
public class MovementControllerSettings : ScriptableObject
{
    [SerializeField] MovementMultipilerOnState[] _movementMultipilerOnStates;
    [SerializeField] float _backMultipiler = 0.5f;
    [SerializeField] float _jumpSpeedMultipiler = 0.2f;
    [SerializeField] float _decelerationOnGround = 5;
    [SerializeField] float _decelerationInAir = 10;
    [SerializeField] float _accelerationOnGround = 4;
    [SerializeField] float _accelerationInAir = 2;
    [SerializeField] float _jumpForce = 1f;
    [SerializeField] float _speed = 1f;
    
    public MovementMultipilerOnState[] MovementMultipilerOnStates { get { return _movementMultipilerOnStates; } }
    public float BackMultipiler { get { return _backMultipiler; } }
    public float JumpSpeedMultipiler { get { return _jumpSpeedMultipiler; } }
    public float DecelerationOnGround { get { return _decelerationOnGround; } }
    public float DecelerationInAir { get { return _decelerationInAir; } }
    public float AccelerationOnGround { get { return _accelerationOnGround; } }
    public float AccelerationInAir { get { return _accelerationInAir; } }
    public float JumpForce { get { return _jumpForce; } }
    public float Speed { get { return _speed; } }
}