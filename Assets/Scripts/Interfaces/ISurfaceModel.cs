using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISurfaceModel
{

    public string SurfaceName { get; set; }
    public float Mu { get; set; }
    public Vector2 WallVelocity { get; set; }
    public float Resistance { get; set; }
    public bool Unslidable { get; set; }
    public bool Slidable { get; set; }

    // Convenience
    public bool Null();
    public bool Normal();
    public bool Slippery();
    public bool Frictionless();
    public bool Static();
    public bool Moving();
    public bool Trudging();

}
