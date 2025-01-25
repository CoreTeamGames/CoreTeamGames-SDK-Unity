using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDown2DController : MonoBehaviour, IMoveable2DTD
{
    #region Variables
    [SerializeField] private ControllerParameters _parameters;
    [SerializeField] private Rigidbody2D _rigidbody2d;
    [SerializeField] private float _velocity;

    private Vector2 _currentVelocity;
    #endregion

    #region Properties
    public Rigidbody2D AttachedRigidbody2D => _rigidbody2d;
    public Vector2 InputVector { get; set; }
    #endregion

    #region Code
    private void FixedUpdate()
    {
        Move(InputVector);
    }

    public void Move(Vector2 moveVector)
    {
        if (moveVector.magnitude > 0)
        {
            // Увеличиваем текущую скорость с учетом ускорения
            _currentVelocity = Vector2.MoveTowards(_currentVelocity, moveVector * _parameters.Speed, _parameters.Acceleration * Time.deltaTime);
        }
        else
        {
            // Постепенно снижаем скорость, если нет входного движения
            _currentVelocity = Vector2.MoveTowards(_currentVelocity, Vector2.zero, _parameters.Decelleration * Time.deltaTime);
        } 

        _rigidbody2d.velocity = _currentVelocity;
    }
    #endregion
}