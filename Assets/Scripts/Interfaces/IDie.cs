
/// <summary>
/// Interface for Components that want to do something just before the GameObject is Destroyed()
/// If an associated IDestroy exists (as a component or parent), then they register with it.
/// If one doesn't exist, they Destroy themselves after performing their Die() action.
/// 
/// MUST call IHaveFinalWords in Start() on associated IDestroy object
/// </summary>
public interface IDie
{
    /// <summary>
    /// Do death scene
    /// </summary>
    public void Die();

    /// <summary>
    /// Die has already been called on me, but I may still not yet be ready.
    /// Check if I'm ReadyToDie().
    /// </summary>
    public bool Dying();

    /// <summary>
    /// Returns true when I'm ready to be Destroyed
    /// </summary>
    public bool ReadyToDie();
}


/// <summary>
/// Helps an IDie class remember to register with associated ISelfDestruct
/// </summary>
public class HelperIDie
{
    ISelfDestruct m_reaper;
    IDie m_parentClass;

    HelperIDie(ISelfDestruct reaperIn, IDie parentClassIn)
    {
        m_reaper = reaperIn;
        m_parentClass = parentClassIn;
    }

    void Start()
    {
        m_reaper.IHaveFinalWords(m_parentClass);
    }
    // parentClass implements:
    // public void Die();
    // public bool Dying();
    // public bool ReadyToDie();
}