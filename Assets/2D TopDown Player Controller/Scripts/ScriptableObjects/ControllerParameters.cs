using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    [CreateAssetMenu(menuName = "CoreTeamSDK/2D Top Down controller/Parameters")]
public class ControllerParameters : ScriptableObject
{
    #region Variables
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _acceleration = 1f;
    [SerializeField] private float _decelleration = 1f;
    #endregion

    #region Properties
    public float Speed => _speed;
    public float Acceleration => _acceleration;
    public float Decelleration => _decelleration;
    #endregion
}