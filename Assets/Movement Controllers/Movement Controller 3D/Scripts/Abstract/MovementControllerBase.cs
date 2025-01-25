using System;
using UnityEngine;

namespace CoreTeamGamesSDK.MovementController.ThreeD
{
    public abstract class MovementControllerBase : MonoBehaviour
    {
        #region Variables
        [SerializeField] private MovementControllerParameters _parameters;
        [SerializeField] private bool _enableMove = true;
        [SerializeField] private bool _enableJump = true;
        [SerializeField] private bool _enableRun = true;
        [SerializeField] private bool _enableDuck = true;
        [SerializeField] private bool _enableProne = true;
        #endregion

        #region Properties
        /// <summary>
        /// The Parameters of movement controller
        /// </summary>
        public MovementControllerParameters Parameters => _parameters;
        /// <summary>
        /// Is the controller run
        /// </summary>
        public abstract bool IsRun {get; }
        /// <summary>
        /// Is the controller ducks
        /// </summary>
        public abstract bool IsDuck {get;}
        /// <summary>
        /// Is the controller prones
        /// </summary>
        public abstract bool IsProne {get;}
        /// <summary>
        /// Is the Move enabled
        /// </summary>
        public bool EnableMove => _enableMove;
        /// <summary>
        /// Is the Jump enabled
        /// </summary>
        public bool EnableJump => _enableJump;
        /// <summary>
        /// Is the Run enabled
        /// </summary>
        public bool EnableRun => _enableRun;
        /// <summary>
        /// Is the Duck enabled
        /// </summary>
        public bool EnableDuck => _enableDuck;
        /// <summary>
        /// Is the Prone enabled
        /// </summary>
        public bool EnableProne => _enableProne;
        // Abstract properties

        /// <summary>
        /// Is the controller moves
        /// </summary>
        public abstract bool IsMove { get; }
        /// <summary>
        /// Is the controller grounded
        /// </summary>
        public abstract bool IsGrounded { get; }
        /// <summary>
        /// Current velocity of controller
        /// </summary>
        public abstract float Velocity { get; }
        #endregion

        #region Events
        public delegate void OnMove(Vector2 moveVector, float velocity, bool isRun, bool isDuck, bool isProne);
        public OnMove OnMoveEvent;
        public delegate void OnJump();
        public OnJump OnJumpEvent;
        public delegate void OnDuck();
        public OnDuck OnDuckEvent;
        public delegate void OnProne();
        public OnProne OnProneEvent;
        public delegate void OnStandUp();
        public OnStandUp OnStandUpEvent;
        #endregion

        #region Methods
        public abstract void Initialize();
        public abstract void Move(Vector2 moveVector);
        public abstract void Jump();
        public abstract void Duck();
        public abstract void Prone();
        public abstract void StandUp();
        public abstract void Run(bool isRun);
        #endregion
    }
}