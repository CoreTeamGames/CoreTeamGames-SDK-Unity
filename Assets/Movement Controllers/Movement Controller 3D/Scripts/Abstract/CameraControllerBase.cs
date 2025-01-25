using UnityEngine;

namespace CoreTeamGamesSDK.MovementController.ThreeD
{
    public abstract class CameraControllerBase : MonoBehaviour
    {
        #region Variables
        [SerializeField] private float _sensivity;
        [SerializeField] private float _minXAngle = -90;
        [SerializeField] private float _maxXAngle = 90;
        [SerializeField] private bool _invertX;
        [SerializeField] private bool _invertY;
        #endregion

        #region Properties
        /// <summary>
        /// The sensivity of camera
        /// </summary>
        public float Sensivity => _sensivity;
        /// <summary>
        /// The minimal vertical look angle of camera
        /// </summary>
        public float MinXAngle => _minXAngle;
        /// <summary>
        /// The maximal vertical look angle of camera
        /// </summary>
        public float MaxXAngle => _maxXAngle;
        /// <summary>
        /// Invert vertical look
        /// </summary>
        public bool InvertX => _invertX;
        /// <summary>
        /// Invert horizontal look
        /// </summary>
        public bool InvertY => _invertY;
        #endregion

        #region Methods
        public abstract void Initialize();
        public abstract void Look(Vector2 lookVector);
        #endregion
    }
}