using UnityEngine;

namespace CoreTeamGamesSDK.MovementController.ThreeD
{
    [CreateAssetMenu(menuName = "CoreTeamSDK/3D Movement Controller/Parameters")]
    public class MovementControllerParameters : ScriptableObject
    {
        #region Variables
        // The speed of Controller
        [SerializeField] private float _speed = 5f;
        // The Multipiler of speed when Controller running
        [SerializeField] private float _runSpeedMultipiler = 1.5f;
        // The Multipiler of speed when Controller moves backward
        [SerializeField] private float _backSpeedMultipiler = 0.6f;
        // The Multipiler of speed when Controller strafes left or right
        [SerializeField] private float _sideSpeedMultipiler = 0.9f;
        // The force of jump
        [SerializeField] private float _jumpForce = 2f;
        // The Multipiler of speed when Controller moves in prone state
        [SerializeField] private float _proneSpeedMultipiler = 0.4f;
        // The Multipiler of speed when Controller moves in duck (sit) state
        [SerializeField] private float _duckSpeedMultipiler = 0.5f;
        #endregion

        #region Properties
        ///<summary>
        /// The speed of Controller
        ///</summary>
        public float Speed => _speed;
        ///<summary>
        /// The Multipiler of speed when Controller running
        ///</summary>
        public float RunSpeedMultipiler => _runSpeedMultipiler;
        ///<summary>
        /// The Multipiler of speed when Controller moves backward
        ///</summary>
        public float BackSpeedMultipiler => _backSpeedMultipiler;
        ///<summary>
        /// The Multipiler of speed when Controller strafes left or right
        ///</summary>
        public float SideSpeedMultipiler => _sideSpeedMultipiler;
        ///<summary>
        /// The force of jump
        ///</summary>
        public float JumpForce => _jumpForce;
        ///<summary>
        /// The Multipiler of speed when Controller moves in prone state
        ///</summary>
        public float ProneSpeedMultipiler => _proneSpeedMultipiler;
        ///<summary>
        /// The Multipiler of speed when Controller moves in duck (sit) state
        ///</summary>
        public float DuckSpeedMultipiler => _duckSpeedMultipiler;
        #endregion
    }
}