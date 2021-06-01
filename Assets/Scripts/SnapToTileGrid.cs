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
        Grid[] grids = FindObjectsOfType<Grid>();
        if (grids.Length == 1)
        {
            // Do this one silently, as it may be working as intended
            gameObject.transform.SetParent(grids[0].transform);
            UpdateGridValues();
            return;
        }
        if (grids.Length == 0)
        {
            // Instantiate a grid as a component
            Debug.LogWarning(gameObject.name + " cannot find a Grid to snap to, instantiate default Grid as a component");
            gameObject.AddComponent<Grid>();
            grid = GetComponent<Grid>();
            UpdateGridValues();
            return;
        }
        // Multiple grids exist, and I'm not set to one
        Debug.LogWarning(gameObject.name + " is missing its Grid, multiple exist.  Selecting first one: " + grids[0].gameObject.name);
        grid = grids[0];
        UpdateGridValues();
    }
#endif
}