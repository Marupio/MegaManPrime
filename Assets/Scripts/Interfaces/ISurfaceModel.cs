using UnityEngine;

public interface ISurfaceModel
{
    public string SurfaceName { get; set; }
    public float Mu { get; set; }
    public Vector2 WallVelocity { get; set; }
    public float Resistance { get; set; }
    public bool Unslidable { get; set; }
    public bool Slidable { get; set; }
}
