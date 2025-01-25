using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TopDown2DControllerInputManager : MonoBehaviour
{
    #region Variables
    [SerializeField] TopDown2DController _controller;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
        if (_controller == null)
        {
            Debug.LogError($"Controller isn{"'"}t set");
            this.enabled = false;
        }
    }
    public void OnMove(InputValue value)
    {
        _controller.InputVector = value.Get<Vector2>();
    }
}
