using UnityEngine;

/// <summary>
/// Components that have death scenes (or actions to take before Destroy()) register with this class, and they get to perform their action
/// Classes with this interface are responsible for calling Destroy() on their GameObjects
/// </summary>
public interface IDestroy
{
    /// <summary>
    /// Go through all overActors and have them play their death scene
    /// Ensure everyone is ready to Die, then destroy the object
    /// </summary>
    public void FinalRites();
    /// <summary>
    /// Register an overActor to the list
    /// </summary>
    /// <param name="overActor">Component that wants to do something before being Destroyed</param>
    public void IHaveFinalWords(IDie overActor);
    /// <summary>
    /// For whatever reason, an overACtor is now gone, so it nolonger needs to do anything before Destroy
    /// </summary>
    /// <param name="overActor">Component that lelft</param>
    public void NeverMind(IDie overActor);
}