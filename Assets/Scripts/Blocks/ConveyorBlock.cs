using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ConveyorBlock : SnapToTileGrid, ISurfaceModel
{
    // *** References
    Animator animator;
    MegaManController megaManController;

    private string surfaceName = "ConveyorBlock";
    /// <summary>
    /// Velocity of the wall in the x-direction, e.g. conveyors, moving platforms
    /// </summary>
    [Tooltip("Speed of the conveyer - positive to the right")]
    [SerializeField] private float conveyorSpeed;
    [Tooltip("Colour name - used to find animation")]
    public string colourName;

    private float mu = 1;
    private float resistance = 0;
    private bool slidable = true;
    private float animationSpeedScalingFactor = 1;

    private ClipSelector clippy;


    // *** ISurface interface

    public string SurfaceName { get => surfaceName; set => surfaceName = value; }
    public float Mu { get => mu; set => mu = value; }
    public Vector2 WallVelocity { get => new Vector2(conveyorSpeed, 0); set => conveyorSpeed = value.x; }
    public float Resistance { get => resistance; set => resistance = value; }
    public bool Unslidable { get => slidable; set => slidable = value; }
    public bool Slidable { get => slidable; set => slidable = value; }

    void Awake()
    {
        animator = GetComponent<Animator>();
        megaManController = FindObjectOfType<MegaManController>();
        if (megaManController != null)
        {
            animationSpeedScalingFactor = megaManController.RunSpeed * 0.5f;
        }
        else
        {
            animationSpeedScalingFactor = 2.5f;
        }
        string direction = "Right";
        if (conveyorSpeed < 0)
        {
            direction = "Left";
        }
        string clipName = colourName + direction;
        Debug.Log("Add component...");
        clippy = gameObject.AddComponent<ClipSelector>();
        clippy.clipName = clipName;
        Debug.Log("clipName set...");
    }


    void Start()
    {
        float animSpeed = Mathf.Abs(conveyorSpeed) / animationSpeedScalingFactor;
        animator.speed = animSpeed;
    }
}
