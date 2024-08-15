using UnityEngine;

public class MovementMultipilerOnState
{
    [SerializeField] float _multipiler;
    [SerializeField] EMovementControllerStates _state;

    public float Multipiler { get { return _multipiler; } }
    public EMovementControllerStates State { get { return _state; } }
}