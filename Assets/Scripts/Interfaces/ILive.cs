using UnityEngine;

/// <summary>
/// Base class for entities with health that can be hurt / healed
/// </summary>
public interface ILive
{
    public int Health { get; }
    public int MaxHealth { get; set; }

    public bool Alive();
    public bool Dead();
    /// <summary>
    /// Something hurt me, take damage, maybe even die
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage);
    /// <summary>
    /// Something is healing me
    /// </summary>
    /// <param name="healing">Amount of healing</param>
    /// <param name="okayToExceedMax">When true, health can go above maxHealth</param>
    public void Heal(int healing, bool okayToExceedMax = false);
    /// <summary>
    /// Go through all overActors and have them play their death scene
    /// Ensure everyone is ready to Die, then destroy the object
    /// </summary>
}
