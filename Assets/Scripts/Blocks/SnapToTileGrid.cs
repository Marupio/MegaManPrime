using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;

public class SnapToTileGrid : MonoBehaviour
{
#if UNITY_EDITOR
    private Grid grid;
    public bool snapToTilemapGrid = true;
    public bool sizeToTilemapGrid = false;
    [HideInInspector] public Vector2 snapValue = new Vector2(0.5f, 0.5f);
    [HideInInspector] public Vector2 snapOffset = Vector2.zero;
    [HideInInspector] public Vector2 sizeValue = new Vector2(0.5f, 0.5f);

    public void UpdateGridValues()
    {
        if (grid == null)
        {
            SetGridRef();
        }
        snapValue = (Vector2)grid.cellSize + (Vector2)grid.cellGap;
        snapOffset = (Vector2)grid.transform.position;
        Tilemap tilemap = grid.GetComponentInChildren<Tilemap>();
        if (tilemap != null)
        {
            snapOffset += (Vector2)tilemap.tileAnchor;
        }
        sizeValue = (Vector2)grid.cellSize;
    }

    public virtual void OnValidate()
    {
        SetGridRef();
        UpdateGridValues();
    }


    public void SetGridRef()
    {
        if (grid != null)
        {
            // Assume everything is okay
            return;
        }

        // First look for other SnapToTileGrid objects and group them together
        SnapToTileGrid[] snapObjects = FindObjectsOfType<SnapToTileGrid>();
        foreach(SnapToTileGrid snapObject in snapObjects)
        {
            grid = snapObject.grid;
            if (grid == null)
            {
                // Unusable object
                continue;
            }
            // Use existing object
            return;
        }

        // Still didn't find it... probably first one being created
        Grid[] grids = FindObjectsOfType<Grid>();
        if (grids.Length == 0)
        {
            // // Instantiate a grid as a component
            // gameObject.AddComponent<Grid>();
            // grid = GetComponent<Grid>();
            // Debug.LogWarning(gameObject.name + " cannot find a Grid to snap to, instantiate default Grid as a component.");
            // Set to Config defaults
            snapValue = Config.defaultSnapToTileGrid_snapValue;
            snapOffset = Config.defaultSnapToTileGrid_snapOffset;
            sizeValue = Config.defaultSnapToTileGrid_sizeValue;

            return;
        }
        if (grids.Length == 1)
        {
            // Do this one silently, as it may be working as intended
            grid = grids[0];
        }
        else
        {
            // Multiple grids exist, and I'm not set to one
            grid = grids[0];
            UpdateGridValues();
            Debug.LogWarning(gameObject.name + " is missing its Grid, multiple exist.  Selecting first one: " + grids[0].gameObject.name);
        }
    }
#endif
}