using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] bool _runAbility = true;
    [SerializeField] bool _jumpAbility = true;
    [SerializeField] EMovementControllerStates _states;
    [SerializeField] MovementControllerSettings _settings;
    [SerializeField] CharacterController _controller;
    public Vector2 inputVector { get; set; }
    float _yVelocity = 0f;

    float _walkStandMultipiler = 0f;
    float _walkCrouchMultipiler = 0f;
    float _walkProneMultipiler = 0f;
    float _runStandMultipiler = 0f;
    MovementMultipilerOnState[] _movementMultipilerOnStates;

    bool _isRun;
    bool _isCrouch;
    bool _isProne;

    public bool IsRun
    {
        get
        {
            return _isRun;
        }
        set
        {
            _isRun = value;
            if (value == true && (IsCrouch || IsProne))
            {
                IsCrouch = false;
                IsProne = false;
            }
        }
    }
    public bool IsCrouch
    {
        get
        {
            return _isCrouch;
        }
        set
        {
            _isCrouch = value;
            if (value == true && (IsRun || IsProne))
            {
                IsRun = false;
                IsProne = false;
            }
        }
    }
    public bool IsProne
    {
        get
        {
            return _isProne;
        }
        set
        {
            _isProne = value;
            if (value == true && (IsRun || IsCrouch))
            {
                IsRun = false;
                IsCrouch = false;
            }
        }
    }
    Vector3 velocity;

    void OnEnable()
    {
        _walkStandMultipiler = GetMultipiler(EMovementControllerStates.WalkStand);
        _walkCrouchMultipiler = GetMultipiler(EMovementControllerStates.WalkCrouch);
        _walkProneMultipiler = GetMultipiler(EMovementControllerStates.WalkProne);
        _runStandMultipiler = GetMultipiler(EMovementControllerStates.RunStand);
    }
    void Awake()
    {
        _controller = gameObject.GetComponentInParent<CharacterController>();
    }
    void FixedUpdate()
    {
        Move(new Vector3(inputVector.x, 0, inputVector.y));
        if (!_controller.isGrounded)
        {
            _yVelocity += Physics.gravity.y;
        }
    }
    void Update()
    {
        if (inputVector == Vector2.zero && _controller.isGrounded)
        {
            if (IsCrouch)
            {
                _states = EMovementControllerStates.IdleCrouch;
            }
            else if (IsProne)
            {
                _states = EMovementControllerStates.IdleProne;
            }
            else
            {
                _states = EMovementControllerStates.IdleStand;
            }
        }
        else if (inputVector != Vector2.zero && _controller.isGrounded)
        {
            if (inputVector.y < 0)
            {
                if (IsRun)
                {
                    _states = EMovementControllerStates.RunStandBack;
                }
                else if (IsCrouch)
                {
                    _states = EMovementControllerStates.WalkCrouchBack;
                }
                else if (IsProne)
                {
                    _states = EMovementControllerStates.WalkProneBack;
                }
                else
                {
                    _states = EMovementControllerStates.WalkStandBack;
                }
            }
            else
            {
                if (IsRun)
                {
                    _states = EMovementControllerStates.RunStand;
                }
                else if (IsCrouch)
                {
                    _states = EMovementControllerStates.WalkCrouch;
                }
                else if (IsProne)
                {
                    _states = EMovementControllerStates.WalkProne;
                }
                else
                {
                    _states = EMovementControllerStates.WalkStand;
                }
            }

        }
        else if (!_controller.isGrounded)
        {
            if (_controller.velocity.y > 0)
            {
                _states = EMovementControllerStates.Jump;
            }
            else
            {
                _states = EMovementControllerStates.Fall;
            }
        }
    }
    float GetMultipiler(EMovementControllerStates controllerState)
    {
        foreach (MovementMultipilerOnState _multipiler in _movementMultipilerOnStates)
        {
            if (_multipiler.State == controllerState)
            {
                return _multipiler.Multipiler;
            }
        }
        return 1;
    }

    public void OnCrouch(bool isCrouch)
    {
        IsCrouch = isCrouch;
    }
    public void OnProne(bool isProne)
    {
        IsProne = isProne;
    }
    public void OnRun(bool isRun)
    {
        IsRun = isRun;
    }
    public void Jump()
    {
        if (_jumpAbility && _controller.isGrounded)
        {
            _yVelocity = _settings.JumpForce;
            while (_controller.velocity.y > 0 && !_controller.isGrounded)
            {
                _states = EMovementControllerStates.Jump;
            }
        }
    }
    public void Move(Vector3 inputVector)
    {
        float _playerSpeed = _states == EMovementControllerStates.RunStand ? _settings.Speed * _runStandMultipiler : _settings.Speed * _walkStandMultipiler;
        float _multipier1 = _controller.isGrounded ? (_states == EMovementControllerStates.WalkCrouch ? _walkCrouchMultipiler : (_states == EMovementControllerStates.WalkProne ? _walkProneMultipiler : 1)) : _settings.JumpSpeedMultipiler;
        float _speedMultipier = inputVector.y < 0 && _controller.isGrounded ? _settings.BackMultipiler : _multipier1;
        float _speed = _playerSpeed * _speedMultipier;
        Vector3 targetVelocity = inputVector * _speed;

        if (inputVector == Vector3.zero)
        {
            float _deceleration = _controller.isGrounded ? _settings.DecelerationOnGround : _settings.DecelerationInAir;
            velocity = Vector3.Lerp(velocity, Vector3.zero, Time.fixedDeltaTime * _deceleration);
        }
        else
        {
            float _acceleration = _controller.isGrounded ? _settings.AccelerationOnGround : _settings.AccelerationInAir;
            velocity = Vector3.Lerp(velocity, targetVelocity, Time.fixedDeltaTime * _acceleration);
        }
        Vector3 _velocity = new Vector3(velocity.x, _yVelocity, velocity.z);

        _controller.Move(transform.TransformDirection(_velocity));
    }
}