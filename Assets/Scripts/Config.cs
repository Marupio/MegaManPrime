using UnityEngine;

public class Config
{
    // Default SnapToTileGrid settings
    static public bool    defaultSnapToTileGrid_snapToTilemapGrid = true;
    static public bool    defaultSnapToTileGrid_sizeToTilemapGrid = false;
    static public Vector2 defaultSnapToTileGrid_snapValue  = new Vector2(0.5f, 0.5f);
    static public Vector2 defaultSnapToTileGrid_snapOffset = new Vector2(0, 0);
    static public Vector2 defaultSnapToTileGrid_sizeValue  = new Vector2(0.5f, 0.5f);
}

// TODO
// Propagate GeneralTools.AssertNotNull
