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
        Grid grid = GetComponentInParent<Grid>();
        snapValue = (Vector2)grid.cellSize + (Vector2)grid.cellGap;
        snapOffset = (Vector2)grid.transform.position;
        Tilemap tilemap = grid.GetComponentInChildren<Tilemap>();
        if (tilemap != null)
        {
            snapOffset += (Vector2)tilemap.tileAnchor;
        }
        sizeValue = (Vector2)grid.cellSize;
    }

    public void OnValidate()
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
            grid = snapObject.GetComponentInParent<Grid>();
            if (grid == null)
            {
                // Unusable object
                continue;
            }
            // Use existing parent
            gameObject.transform.SetParent(snapObject.transform.parent);
        }

        if (grid != null)
        {
            // Okay, you can go now
            return;
        }

        // Still didn't find it... probably first one being created
        Grid[] grids = FindObjectsOfType<Grid>();
        if (grids.Length == 0)
        {
            // Instantiate a grid as a component of a new parent gameObject
            GameObject snapGroupObject = new GameObject("SnappedObjects");
            snapGroupObject.AddComponent<Grid>();
            GameObject newParent = Instantiate<GameObject>(snapGroupObject);
            grid = newParent.GetComponent<Grid>();
            gameObject.transform.SetParent(newParent.transform);
            Debug.LogWarning(gameObject.name + " cannot find a Grid to snap to, instantiate default Grid as a parent GameObject");
            UpdateGridValues();
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
            Debug.LogWarning(gameObject.name + " is missing its Grid, multiple exist.  Selecting first one: " + grids[0].gameObject.name);
        }
        {
            // Local scope to clear non-C++ errors that I'm not used to encountering
            GameObject snapGroupObject = new GameObject("SnappedObjects");
            GameObject newParent = Instantiate<GameObject>(snapGroupObject, grid.transform.position, grid.transform.rotation, grid.transform);
            gameObject.transform.SetParent(newParent.transform);
            UpdateGridValues();
        }
    }
#endif
}