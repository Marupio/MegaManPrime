using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGasp
{
    /// <summary>
    /// Do death scene
    /// </summary>
    public void Gasp();


    /// <summary>
    /// Gasp has already been called on me, check if I'm ready to die
    /// </summary>
    public bool Gasped();

    /// <summary>
    /// Returns true when I'm ready to be Destroyed
    /// </summary>
    public bool ReadyToDie();
}
