using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using System;

namespace CoreTeamGamesSDK.Input.Management
{
    public class InputManager : MonoBehaviour
    {
        #region Variables
        private PlayerInput _playerInput;
        private InputActionAsset _inputActions;
        private Dictionary<string, Action<InputAction.CallbackContext>> _inputEvents;
        #endregion

        #region Properties
        /// <summary>
        /// The instance of InputManager
        /// </summary>
        public static InputManager Instance { get; private set; }
        #endregion

        #region Code
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                InitializeInput();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void InitializeInput()
        {
            _playerInput = GetComponent<PlayerInput>();
            _inputActions = _playerInput.actions;
            _inputEvents = new Dictionary<string, Action<InputAction.CallbackContext>>();

            foreach (InputActionMap actionMap in _inputActions.actionMaps)
            {
                foreach (InputAction action in actionMap.actions)
                {
                    _inputEvents[action.name] = null;

                    action.performed += context => InvokeEvent(action.name, context);
                    action.canceled += context => InvokeEvent(action.name, context);
                    action.started += context => InvokeEvent(action.name, context);
                }
            }
        }

        public void SubscribeToInput(string actionName, Action<InputAction.CallbackContext> callback)
        {
            if (_inputEvents.ContainsKey(actionName))
            {
                _inputEvents[actionName] += callback;
            }
            else
            {
                Debug.LogWarning($"Action {actionName} not found in Input Actions");
            }
        }

        public void UnsubscribeFromInput(string actionName, Action<InputAction.CallbackContext> callback)
        {
            if (_inputEvents.ContainsKey(actionName))
            {
                _inputEvents[actionName] -= callback;
            }
        }

        private void InvokeEvent(string actionName, InputAction.CallbackContext context)
        {
            _inputEvents[actionName]?.Invoke(context);
        }

        public T GetInputValue<T>(string actionName) where T : struct
        {
            InputAction action = _inputActions.FindAction(actionName);

            if (action != null)
            {
                return action.ReadValue<T>();
            }

            Debug.LogWarning($"Action {actionName} not found");
            return default;
        }

        public void EnableActionMap(string actionMapName)
        {
            InputActionMap actionMap = _inputActions.FindActionMap(actionMapName);

            if (actionMap != null)
            {
                actionMap.Enable();
            }
        }

        public void DisableActionMap(string actionMapName)
        {
            InputActionMap actionMap = _inputActions.FindActionMap(actionMapName);

            if (actionMap != null)
            {
                actionMap.Disable();
            }
        }

        private void OnDestroy() => _inputEvents?.Clear();
        #endregion
    }
}