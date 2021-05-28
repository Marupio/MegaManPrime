using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Grid))]
public class LadderHandler : MonoBehaviour
{
    // Ladder data
    private Grid m_ladderGrid;
    private GameObject m_ladderObject;
    private Tilemap m_ladderMap;
    private TilemapCollider2D m_ladderCollider;

    // Ground data - for trapdoors
    private bool m_groundHandlingEnabled = false;
    private GameObject m_groundObject;          // other object
    private Grid m_groundGrid;                  // maybe my parent, maybe not
    private Tilemap m_groundMap;                // other's component
    private TilemapCollider2D m_groundCollider; // other's component

    /// <summary>
    /// Data structure for the state of a TrapDoor
    /// </summary>    
    protected struct TrapDoor
    {
        /// <summary>
        /// This tile gets added to the m_groundMap when TrapDoor is closed
        /// </summary>
        public TileBase groundTile { get; set; }
        /// <summary>
        /// If ladder and ground overlap, it is normally closed
        /// If it is only a ladder tile (at the top of a ladder), it is normally open
        /// </summary>
        public bool defaultOpen { get; set; }
        /// <summary>
        /// Current state of the TrapDoor
        /// </summary>
        public bool open { get; set; }

        public TrapDoor(TileBase gt, bool defaultIn)
        {
            groundTile = gt;
            defaultOpen = defaultIn;
            open = defaultIn;
        }
    }
    /// <summary>
    /// Contains location and state of all TrapDoors:
    ///     m_trapDoors : key=ladderCell coords in Vector2Int, value=TrapDoor struct (above)
    /// </summary>
    Dictionary<Vector2Int, TrapDoor> m_trapDoors;
    /// <summary>
    /// Contains list of all TrapDoor coordinates, in ladderCell coords, as Vector2Int types
    /// </summary>
    private List<Vector2Int> m_trapDoorCoords;


    // Self-generated, only if groundHandling and ladder/ground doubles exist
    bool m_fakeGroundEnabled = false;
    private GameObject m_fakeGroundObject;
    private Grid m_fakeGroundGrid;
    private Tilemap m_fakeGroundMap;
    private TilemapCollider2D m_fakeGroundCollider;


    [Header("What are Ladders?")]
    [SerializeField] private ObjectFinder m_ladderFinder;


    [Header("What is Ground?")]
    [SerializeField] private ObjectFinder m_groundFinder;


     // Monobehaviour interface

    private void Awake()
    {
        string problem = InitReferences();
        if (problem != "")
        {
            Debug.Log("Failed to properly initialise LadderHandler " + gameObject.name + " for reason: " + problem);
            enabled = false;
            return;
        }
        InitTrapDoors();
    }


    // private void FixedUpdate()
    // {
    // }


    private void OnDestroy()
    {
        if (m_ladderCollider)
        {
            Debug.Log("Destroying valid script");
        }
        else
        {
            Debug.Log("Destroying invalid script");
        }
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
        if (!testCollider)
        {
            Debug.LogError("Missing testCollider");
        }
        if (!m_ladderCollider)
        {
            Debug.LogError("Missing ladderCollider");
        }
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


    private string InitReferences()
    {
        m_ladderObject = m_ladderFinder.FindObject();
        if (!m_ladderObject)
        {
            return "Could not find the Ladder GameObject";
        }
        m_ladderMap = m_ladderObject.GetComponent<Tilemap>();
        if (!m_ladderMap)
        {
            return "Could not find Tilemap on the Ladder GameObject";
        }
        m_ladderCollider = m_ladderObject.GetComponent<TilemapCollider2D>();
        if (!m_ladderCollider)
        {
            return "Could not find the TilemapCollider2D on the Ladder GameObject";
        }
        m_ladderGrid = m_ladderMap.layoutGrid;
        if (!m_ladderGrid)
        {
            return "Could not find the Grid on the Ladder GameObject";
        }
        m_groundObject = m_groundFinder.FindObject();
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
                }
            }
        }
        return "";
    }


    /// <summary>
    /// At the top of a ladder, with no ground involved
    /// </summary>
    /// <param name="ladderCell">Location of top of ladder</param>
    private void AddTrapDoor(Vector3Int ladderCell)
    {
        TileBase ladderTile = m_ladderMap.GetTile(ladderCell);
        TrapDoor td = new TrapDoor(ladderTile, true);
        m_trapDoors.Add((Vector2Int)ladderCell, td);
        m_trapDoorCoords.Add((Vector2Int)ladderCell);
        m_groundMap.SetTile(LadderCellToGroundCell(ladderCell), ladderTile);
    }


    /// <summary>
    /// At a place where the ladder passes through a ground tile - regardless if this is the top of the ladder, we need a trapdoor here
    /// </summary>
    /// <param name="ladderCell">Location in ladder grid</param>
    /// <param name="groundCell">Location in ground grid</param>
    private void AddTrapDoor(Vector3Int ladderCell, Vector3Int groundCell)
    {
        if (!m_fakeGroundEnabled)
        {
            Debug.LogError("Attempting to AddTrapDoor where both ground and ladder tiles exist, but no fakeGround exists");
            return;
        }
        Vector3Int fakeGroundCell = GroundCellToFakeGroundCell(groundCell);
        TileBase groundTile = m_groundMap.GetTile(groundCell);
        TrapDoor td = new TrapDoor(groundTile, false);
        m_trapDoors.Add((Vector2Int)ladderCell, td);
        m_trapDoorCoords.Add((Vector2Int)ladderCell);
        m_fakeGroundMap.SetTile(fakeGroundCell, groundTile);
    }


    /// <summary>
    /// Close the TrapDoor at the given location
    /// TrapDoor must exist there, but it doesn't have to be open
    /// </summary>
    /// <param name="ladderCell">Location of the TrapDoor</param>
    private void CloseTrapDoor(Vector3Int ladderCell)
    {
        // Assume it exists
        TrapDoor td = m_trapDoors[(Vector2Int)ladderCell];
        if (td.open)
        {
            m_groundMap.SetTile(LadderCellToGroundCell(ladderCell), td.groundTile);
            td.open = false;
            m_trapDoors[(Vector2Int)ladderCell] = td;
        }
    }


    /// <summary>
    /// Open the TrapDoor at the given location
    /// TrapDoor must exist there, but it doesn't have to be closed
    /// </summary>
    /// <param name="ladderCell">Location of the TrapDoor</param>
    private void OpenTrapDoor(Vector3Int ladderCell)
    {
        // Assume it exists
        TrapDoor td = m_trapDoors[(Vector2Int)ladderCell];
        if (!td.open)
        {
            m_groundMap.SetTile(LadderCellToGroundCell(ladderCell), null);
            td.open = true;
            m_trapDoors[(Vector2Int)ladderCell] = td;
        }
    }


    private Vector3Int LadderCellToGroundCell(Vector3Int ladderCell)
    {
        return m_groundGrid.WorldToCell(m_ladderGrid.CellToWorld(ladderCell));
    }


    private Vector3Int LadderCellToFakeGroundCell(Vector3Int ladderCell)
    {
        return m_fakeGroundGrid.WorldToCell(m_ladderGrid.CellToWorld(ladderCell));
    }


    private Vector3Int GroundCellToLadderCell(Vector3Int groundCell)
    {
        return m_ladderGrid.WorldToCell(m_groundGrid.CellToWorld(groundCell));
    }


    private Vector3Int GroundCellToFakeGroundCell(Vector3Int groundCell)
    {
        return m_fakeGroundGrid.WorldToCell(m_groundGrid.CellToWorld(groundCell));
    }


    private Vector3Int FakeGroundCellToLadderCell(Vector3Int fakeGroundCell)
    {
        return m_ladderGrid.WorldToCell(m_fakeGroundGrid.CellToWorld(fakeGroundCell));
    }


    private Vector3Int FakeGroundCellToGroundCell(Vector3Int fakeGroundCell)
    {
        return m_groundGrid.WorldToCell(m_fakeGroundGrid.CellToWorld(fakeGroundCell));
    }


    // BoundsInt cellBounds = m_ladderMap.cellBounds;
    // m_trapDoorIndex = new List<List<int>>();
    private void InitTrapDoors()
    {
        bool fakeGroundNeeded = false;
        if (!m_groundHandlingEnabled)
        {
            return;
        }
        BoundsInt cellBounds = m_ladderMap.cellBounds;
        for (int x = cellBounds.min.x; x <= cellBounds.max.x; ++x)
        {
            // At top, all ladder tiles need trapdoors
            Vector3Int curLadderCell = new Vector3Int(x, cellBounds.max.y, 0);
            Vector3Int curGroundCell = LadderCellToGroundCell(curLadderCell);
            bool onLadder = false;
            if (m_ladderMap.HasTile(curLadderCell))
            {
                onLadder = true;
                if (m_groundMap.HasTile(curGroundCell))
                {
                    if (!fakeGroundNeeded)
                    {
                        fakeGroundNeeded = true;
                        MakeFakeGroundObjects();
                    }
                    AddTrapDoor(curLadderCell, curGroundCell);
                }
                else
                {
                    AddTrapDoor(curLadderCell);
                }
            }
            for (int y = cellBounds.max.y - 1; y >= cellBounds.min.y; --y)
            {
                curLadderCell.y = y;
                bool curOnLadder = m_ladderMap.HasTile(curLadderCell);
                if (curOnLadder)
                {
                    curGroundCell = LadderCellToGroundCell(curLadderCell);
                    if (m_groundMap.HasTile(curGroundCell))
                    {
                        // Regardless if this is a ladder top or not, we need a trapdoor here
                        if (!fakeGroundNeeded)
                        {
                            fakeGroundNeeded = true;
                            MakeFakeGroundObjects();
                        }
                        AddTrapDoor(curLadderCell, curGroundCell);
                    }
                    else if (!onLadder)
                    {
                        // This is the top of a ladder, no ground involved
                        AddTrapDoor(curLadderCell);
                    }
                }
                onLadder = curOnLadder;
            }
        }
    }


    private void MakeFakeGroundObjects()
    {
        if (m_fakeGroundEnabled)
        {
            Debug.LogError("Attempting to MakeFakeGroundObjects when they've already been made");
            return;
        }
        m_fakeGroundEnabled = true;
        m_ladderMap.enabled = false;
        m_fakeGroundObject = Instantiate(m_ladderMap.gameObject, m_ladderGrid.transform);
        m_fakeGroundMap = m_fakeGroundObject.GetComponent<Tilemap>();
        m_fakeGroundGrid = m_fakeGroundMap.layoutGrid;
        m_fakeGroundCollider = m_fakeGroundObject.GetComponent<TilemapCollider2D>();
        m_fakeGroundMap.ClearAllTiles();

        // Hide me behind ladders, but infront of ground - constraint - ground must have sorting order two before ladder or another sorting layer
        m_fakeGroundObject.GetComponent<TilemapRenderer>().sortingOrder--;
    }

}
