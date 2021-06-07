using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// A simple concrete implementation of the ISurfaceModel interface
/// </summary>
public class SurfaceModel : MonoBehaviour, ISurfaceModel
{
    // Surface characteristics

    [Tooltip("Name of surface model")]
    [SerializeField] private string surfaceName;
    /// <summary>
    /// Coefficient of friction
    /// 0 = slide forever, 1 = pavement-like
    /// </summary>
    [Tooltip("Material friction - 0 = slip 'n' slide, 1 = like pavement")]
    [SerializeField] [Range(0, 1)] private float mu;
    /// <summary>
    /// Velocity of the wall in the x-direction, e.g. conveyors, moving platforms
    /// </summary>
    [Tooltip("The speed and direction of the wall movement")]
    [SerializeField] private Vector2 wallVelocity;
    /// <summary>
    /// To emulate 'trudging' (e.g. through deep water, sand, mud)
    /// trudgingSpeed = normalSpeed * resistance
    /// </summary>
    [Tooltip("Is it hard for MegaMan to walk along this surface? 0 = no resistance (easy to move), 1 = full resistance (cannot move)")]
    [SerializeField] [Range(0, 1)] private float resistance;
    /// <summary>
    /// True if MegaMan can slide across this ground
    /// </summary>
    [Tooltip("Check this if MegaMan can't slide here")]
    [SerializeField] private bool slidable = true;

    public string SurfaceName { get => surfaceName; set => surfaceName = value; }
    public float Mu { get => mu; set => mu = value; }
    public Vector2 WallVelocity { get => wallVelocity; set => wallVelocity = value; }
    public float Resistance { get => resistance; set => resistance = value; }
    public bool Unslidable { get => slidable; set => slidable = value; }
    public bool Slidable { get => slidable; set => slidable = value; }

    public SurfaceModel(string surfaceNameIn, float muIn, Vector2 wallVelocityIn, float resistanceIn, bool slidableIn = true)
    {
        surfaceName = surfaceNameIn;
        mu = muIn;
        wallVelocity = wallVelocityIn;
        resistance = resistanceIn;
        slidable = slidableIn;
        if (mu < 0 || mu > 1)
        {
            Debug.LogError("Ground mu value out of range 0..1 : " + mu);
        }
        if (resistance < 0 || resistance > 1)
        {
            Debug.LogError("Ground resistance value out of range 0..1 : " + resistance);
        }
    }
}
