using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LadderHandler : MonoBehaviour
{
    private Grid m_ladderGrid;
    private Tilemap m_ladderMap;
    private TilemapCollider2D m_ladderCollider;

    [Tooltip("A mask determining what is a ladder")]
    [SerializeField] private LayerMask m_whatAreLadders;


    // Monobehaviour interface

    private void Awake()
    {
        Debug.Log("My name is " + gameObject.name);
        //m_ladderMap = FindObjectOfType<Tilemap>();
        Tilemap[] allTilemaps = FindObjectsOfType<Tilemap>();
        int found = 0;
        foreach (Tilemap mapI in allTilemaps)
        {
            if (GeneralTools.IsInLayerMask(mapI.gameObject, m_whatAreLadders))
            {
                m_ladderMap = mapI;
                ++found;
            }
        }
        if (found != 1)
        {
            Debug.LogError("Found " + found + " tilemaps on ladder LayerMask.  Need 1 and only 1.  Cannot detect ladders correctly!");
            enabled = false;
        }
        else
        {
            m_ladderGrid = m_ladderMap.layoutGrid;
            m_ladderCollider = m_ladderMap.gameObject.GetComponent<TilemapCollider2D>();
            if (!m_ladderCollider)
            {
                Debug.LogError("Ladder tilemap " + m_ladderMap.gameObject.name + " has no TilemapCollider2D.  Cannot detect ladders correctly!");
                enabled = false;
            }
        }
    }


    // Access

    public Grid LadderGrid()
    {
        return m_ladderGrid;
    }
    public Tilemap LadderMap()
    {
        return m_ladderMap;
    }
    public TilemapCollider2D LadderMapCollider()
    {
        return m_ladderCollider;
    }


    // Query

    public bool OnLadder(Collider2D testCollider)
    {
        return testCollider.IsTouching(m_ladderCollider);
    }


    public bool ClosestLadder(Collider2D testCollider, ref Vector2 closestPosition)
    {
        if (!OnLadder(testCollider))
        {
            return false;
        }
        Vector2 closestPt = m_ladderCollider.ClosestPoint(testCollider.bounds.center);
        Vector3Int closestCell = m_ladderGrid.WorldToCell(closestPt);
        return CheckNeighbours(testCollider, closestCell, ref closestPosition);
    }


    private bool CheckNeighbours(Collider2D testCollider, Vector3Int closestCell, ref Vector2 closestPosition)
    {
        List<Vector3Int> neighbours = new List<Vector3Int>();
        BoundsInt cellBounds = m_ladderMap.cellBounds;

        // Find all neighbours to closestCell
        for (int x = -1; x <= 1; ++x)
        {
            for (int y = -1; y <= 1; ++y)
            {
                Vector3Int newCellCoords = new Vector3Int(closestCell.x + x, closestCell.y + y, closestCell.z);
                if (cellBounds.Contains(newCellCoords))
                {
                    neighbours.Add(newCellCoords);
                }
            }
        }

        float minDist = float.MaxValue;
        Vector2 bestCoords = Vector2.zero;
        bool foundLadder = false;
        Vector3 halfWay = m_ladderGrid.cellSize * 0.5f;
        Vector2 testCtr = testCollider.bounds.center;
        foreach (Vector3Int cellI in neighbours)
        {
            if (m_ladderMap.HasTile(cellI))
            {
                Vector2 ladderCoords = m_ladderGrid.CellToWorld(cellI) + halfWay;
                float curDist = Vector2.Distance(testCtr, ladderCoords);
                if (curDist < minDist)
                {
                    minDist = curDist;
                    foundLadder = true;
                    bestCoords = ladderCoords;
                }
            }
        }
        if (!foundLadder)
        {
            Debug.LogError("Could not find Ladder closest to " + closestCell + ", but OnLadder was true");
            return false;
        }
        closestPosition = bestCoords;
        return foundLadder;
    }
}
