using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LadderHandler : MonoBehaviour
{
    // Ladder data
    private Grid m_ladderGrid;
    private Tilemap m_ladderMap;
    private TilemapCollider2D m_ladderCollider;

    // Ground data - for trapdoors
    private bool m_groundHandlingEnabled = false;
    private GameObject m_groundObject;
    private Grid m_groundGrid;
    private Tilemap m_groundMap;
    private TilemapCollider2D m_groundCollider;


    [Header("What Are Ladders?")]
    [Tooltip("A mask determining what is a ladder")]
    [SerializeField] private LayerMask m_ladderLayers;


    [Header("What Is Ground?")]
    [SerializeField] private bool m_specifyLayerMask = false;
    [SerializeField] private LayerMask m_groundLayers;
    [SerializeField] private bool m_specifyMinDepth = false;
    [SerializeField] private float m_groundMinDepth;
    [SerializeField] private bool m_specifyMaxDepth = false;
    [SerializeField] private float m_groundMaxDepth;
    [SerializeField] private bool m_specifyNameContains = false;
    [SerializeField] private string m_groundNameContains = "";
    [SerializeField] private bool m_specifyNameExact = false;
    [SerializeField] private string m_groundNameExact = "";
    [SerializeField] private bool m_specifyTagContains = false;
    [SerializeField] private string m_groundTagContains = "";
    [SerializeField] private bool m_specifyTagExact = false;
    [SerializeField] private string m_groundTagExact = "";


    // Monobehaviour interface

    private void Awake()
    {
        Debug.Log("My name is " + gameObject.name);
        //m_ladderMap = FindObjectOfType<Tilemap>();
        m_ladderMap = GetComponent<Tilemap>();
        m_ladderCollider = GetComponent<TilemapCollider2D>();
        m_ladderGrid = m_ladderMap.layoutGrid;

        ObjectFinder groundFinder = new ObjectFinder();
        { // ObjectFinder scope for code-folding
            if (m_specifyLayerMask)
            {
                groundFinder.SetLayerMask(m_groundLayers);
            }
            if (m_specifyMinDepth)
            {
                groundFinder.SetMinDepth(m_groundMinDepth);
            }
            if (m_specifyMaxDepth)
            {
                groundFinder.SetMaxDepth(m_groundMaxDepth);
            }
            if (m_specifyNameContains)
            {
                groundFinder.SetNameContains(m_groundNameContains);
            }
            if (m_specifyNameExact)
            {
                groundFinder.SetNameExact(m_groundNameExact);
            }
            if (m_specifyTagContains)
            {
                groundFinder.SetTagContains(m_groundTagContains);
            }
            if (m_specifyTagExact)
            {
                groundFinder.SetTagExact(m_groundTagExact);
            }

            m_groundObject = groundFinder.FindObject();
            if (m_groundObject)
            {
                m_groundMap = m_groundObject.GetComponent<Tilemap>();
                if (m_groundMap)
                {
                    m_groundGrid = m_groundMap.layoutGrid;
                    m_groundCollider = m_groundObject.GetComponent<TilemapCollider2D>();
                    if (m_groundCollider)
                    {
                        m_groundHandlingEnabled = true;
                        Debug.Log("My ground GameObject is named " + m_groundObject.name);
                    }
                }
            }
        }
        ResetTrapdoors();
    }


    // Access

    public Grid LadderGrid() { return m_ladderGrid; }
    public Tilemap LadderMap() { return m_ladderMap; }
    public TilemapCollider2D LadderMapCollider() { return m_ladderCollider; }


    // Query

    // Passive ladder operations - does not need ground

    /// <summary>
    /// Tests if the given collider overlaps a ladder
    /// </summary>
    /// <param name="testCollider">Collider to check for ladder overlaps</param>
    /// <returns>true if collider overlaps a ladder</returns>
    public bool OnLadder(Collider2D testCollider)
    {
        return testCollider.IsTouching(m_ladderCollider);
    }

    /// <summary>
    /// Finds all ladders overlapping testCollider, and returns the one that is closest to the testCollider's centre bounds
    /// </summary>
    /// <param name="testCollider"></param>
    /// <param name="closestPosition">gets set to the 2D mid-point of the closest ladder tile</param>
    /// <returns>true if a ladder was found</returns>
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

    //DeleteCells
    //HasTile
    //SetTile(Vector3Int position, TileBase tile);

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
        //Vector3 halfWay = m_ladderGrid.cellSize * 0.5f;
        Vector2 testCtr = testCollider.bounds.center;
        foreach (Vector3Int cellI in neighbours)
        {
            if (m_ladderMap.HasTile(cellI))
            {
                Vector2 ladderCoords = m_ladderMap.GetCellCenterWorld(cellI);// m_ladderGrid.CellToWorld(cellI) + halfWay;
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


    private void ResetTrapdoors()
    {
        if (!m_groundHandlingEnabled)
        {
            return;
        }
        BoundsInt cellBounds = m_ladderMap.cellBounds;
        for (int x = cellBounds.min.x; x <= cellBounds.max.x; ++x)
        {
            // At top, all ladder tiles need trapdoors
            Vector3Int curLadderCell = new Vector3Int(x, cellBounds.max.y, 0);
            if (m_ladderMap.HasTile(curLadderCell))
            {
                AddLadderTop(curLadderCell);
            }
            for (int y = cellBounds.max.y-1; y >= cellBounds.min.y; --y)
            {
                
            }
        }
    }


    private void AddLadderTop(Vector3Int ladderCell)
    {
        Vector3Int groundCoords = LadderCellToGroundCell(ladderCell);
        if (m_groundMap.HasTile(groundCoords))
        {
            // We need to combine ground & ladder tiles together
        }
    }

    struct TrapDoor
    {
        /// <summary>
        /// Coordinates in the ladder map of the TrapDoor
        /// </summary>
        public Vector3Int ladderCell { get; set; }
        /// <summary>
        /// Tile to put on ladderMap when opening for MegaMan to pass
        /// If a tile exists on both ground and ladder maps, this should be a pixel-by-pixel combination, otherwise it should just be a copy of the
        /// tile from the ladder map
        /// </summary>
        /// <value></value>
        public Tile openTile { get; set; }
    }



    private Vector3Int LadderCellToGroundCell(Vector3Int ladderCell)
    {
        return m_groundGrid.WorldToCell(m_ladderGrid.CellToWorld(ladderCell));
    }


    private Vector3Int GroundCellToLadderCell(Vector3Int groundCell)
    {
        return m_ladderGrid.WorldToCell(m_groundGrid.CellToWorld(groundCell));
    }
}
