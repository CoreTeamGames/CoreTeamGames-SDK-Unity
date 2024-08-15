using UnityEngine;

/// <summary>
/// Interface for moveable objects (Player, NPC, etc.)
/// </summary>
public interface IMoveable
{
    #region Properties
    /// <summary>
    /// Current velocity of object
    /// </summary>
    Vector3 Velocity { get; }
    #endregion

    #region Methods
    /// <summary>
    /// Moves object in 2D space
    /// </summary>
    /// <param name="_inputVector2">The input for move in 2D space</param>
    void Move(Vector2 _inputVector2);

    /// <summary>
    /// Moves object in 3D space
    /// </summary>
    /// <param name="_inputVector3">The input for move in 3D space</param>
    void Move(Vector3 _inputVector3);

    /// <summary>
    /// The jump
    /// </summary>
    void Jump();
    #endregion
}