/// <summary>
/// Interface for hitting objects (Player, vehicles, some props, etc.)
/// </summary>
public interface IHitable
{
    #region Properties
    /// <summary>
    /// Health of the object
    /// </summary>
    int Health { get; }

    /// <summary>
    /// Armor of the object
    /// </summary>
    int Armor { get; }

    /// <summary>
    /// If true, the object can take damage
    /// </summary>
    bool EnableDamage { get; }

    /// <summary>
    /// If true, the object can have armor
    /// </summary>
    bool EnableArmor { get; }
    #endregion

    #region Methods
    /// <summary>
    /// Hits the object
    /// </summary>
    /// <param name="damage">The damage, what applied to the object</param>
    void Hit(float damage);
    #endregion
}