using CoreTeamGamesSDK.MovementController.ThreeD;
using CoreTeamGamesSDK.Input.Management;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables
    [SerializeField] private InputManager _manager;
    [SerializeField] private MovementControllerBase _movementController;
    [SerializeField] private CameraControllerBase _cameraController;
    #endregion

    #region Code
    private void Start()
    {
        if (_movementController != null)
        {
            _movementController.Initialize();
        }

        if (_cameraController != null)
        {
            _cameraController.Initialize();
        }
    }

    private void OnEnable()
    {
        InputManager.Instance.SubscribeToInput("Move", OnMove);
        InputManager.Instance.SubscribeToInput("Jump", OnJump);
        InputManager.Instance.SubscribeToInput("Duck", OnDuck);
        InputManager.Instance.SubscribeToInput("Prone", OnProne);
        InputManager.Instance.SubscribeToInput("Run", OnRun);
        InputManager.Instance.SubscribeToInput("Look", OnLook);
    }

    private void OnDisable()
    {
        InputManager.Instance.UnsubscribeFromInput("Move", OnMove);
        InputManager.Instance.UnsubscribeFromInput("Jump", OnJump);
        InputManager.Instance.UnsubscribeFromInput("Duck", OnDuck);
        InputManager.Instance.UnsubscribeFromInput("Prone", OnProne);
        InputManager.Instance.UnsubscribeFromInput("Run", OnRun);
        InputManager.Instance.UnsubscribeFromInput("Look", OnLook);
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        if (_movementController != null)
        {
            Vector2 moveInput = context.ReadValue<Vector2>();
            _movementController.Move(moveInput);
        }
    }

    private void OnLook(InputAction.CallbackContext context)
    {
        if (_cameraController != null)
        {
            Vector2 lookVector = context.ReadValue<Vector2>();
            _cameraController.Look(lookVector);
        }
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (_movementController != null && context.performed)
        {
            _movementController.Jump();
        }
    }
    private void OnDuck(InputAction.CallbackContext context)
    {
        if (_movementController != null)
        {
            _movementController.Duck();
        }
    }
    private void OnProne(InputAction.CallbackContext context)
    {
        if (_movementController != null)
        {
            _movementController.Prone();
        }
    }

    private void OnRun(InputAction.CallbackContext context)
    {
        if (_movementController != null)
        {
            _movementController.Run(context.ReadValueAsButton());
        }
    }
    #endregion
}