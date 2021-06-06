using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDie
{
    /// <summary>
    /// Do death scene
    /// </summary>
    public void Die();

    /// <summary>
    /// Die has already been called on me, now check if I'm ready to die
    /// </summary>
    public bool Dying();

    /// <summary>
    /// Returns true when I'm ready to be Destroyed
    /// </summary>
    public bool ReadyToDie();
}
