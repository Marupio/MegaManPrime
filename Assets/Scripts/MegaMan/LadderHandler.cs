using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LadderHandler : MonoBehaviour
{
    // *** Internal classes, structs, types

    /// <summary>
    /// Simple enumeration - DoorPosition - Close or Open
    /// Pizza is a nonesense value... so null?  Maybe it means we don't know?
    /// </summary>
    public enum DoorPosition
    {
        Pizza,
        Close,
        Open
    }

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
        public DoorPosition defaultPosition { get; set; }
        /// <summary>
        /// Current state of the TrapDoor
        /// </summary>
        public DoorPosition position { get; set; }


        // *** Query

        /// <summary>
        /// Returns true if the door is open
        /// </summary>
        public bool Opened() { return position == DoorPosition.Open; }
        /// <summary>
        /// Returns true if the door is closed
        /// </summary>
        public bool Closed() { return position == DoorPosition.Close; }


        // *** Edit

        /// <summary>
        /// Open the door
        /// </summary>
        public void OpenDoor() { position = DoorPosition.Open; }
        /// <summary>
        /// Close the door
        /// </summary>
        public void CloseDoor() { position = DoorPosition.Close; }


        // *** Constructors

        public TrapDoor(TileBase gt, DoorPosition defaultPos)
        {
            groundTile = gt;
            defaultPosition = defaultPos;
            position = defaultPos;
        }
    }


    /// <summary>
    /// Used to watch when a collider leaves a given cell, and triggering a given action
    /// </summary>
    protected struct Watch
    {
        public Collider2D collider;
        public Vector2Int ladderCell;
        public DoorPosition actionToTake;
        public bool stale;

        public Watch(Collider2D col, Vector2Int cell, DoorPosition act)
        {
            collider = col;
            ladderCell = cell;
            actionToTake = act;
            stale = false;
        }
    }

    // *** Member data

    // MegaMan data
    private GameObject m_megaManObject;
    private MegaManController m_megaManController;
    private Collider2D m_megaManUprightLadderDetector;
    private Collider2D m_megaManGroundLadderDetector;

    // Ladder data
    private Grid m_ladderGrid;
    private GameObject m_ladderObject;
    private Tilemap m_ladderMap;
    private TilemapCollider2D m_ladderCollider;

    // Ground data - for trapdoors
    private bool m_groundHandlingEnabled = false;
    private GameObject m_groundObject;
    private Grid m_groundGrid;
    private Tilemap m_groundMap;
    private TilemapCollider2D m_groundCollider;

    /// <summary>
    /// Contains location and state of all TrapDoors:
    ///     m_trapDoors : key=ladderCell coords in Vector2Int, value=TrapDoor struct (above)
    /// </summary>
    Dictionary<Vector2Int, TrapDoor> m_trapDoors;
    TrapDoor m_trapDoorNull;
    /// <summary>
    /// Contains a list of all trapDoors that are currently in a non-default position
    /// key = ladderCell position, value = a time value, not yet figured out how I'm going to use it
    /// </summary>
    private Dictionary<Vector2Int, float> m_trapDoorsOffDefault;


    // Self-generated, only if groundHandling and ladder/ground doubles exist
    bool m_fakeGroundEnabled = false;
    private GameObject m_fakeGroundObject;
    private Grid m_fakeGroundGrid;
    private Tilemap m_fakeGroundMap;
    private TilemapCollider2D m_fakeGroundCollider;

    [Header("Settings")]
    [Tooltip("When MegaMan climbs to the top of a ladder, it will turn into a floor so he can stand on it.  If he jumps off, it will wait this "
        + "delay before turning back into a ladder.")]
    [SerializeField] [Range(0, 5)] private float m_standingOnLadderFloorDisappearDelay = 2;


    [Header("What is MegaMan?")]
    [SerializeField] private ObjectFinder m_megaManFinder;


    [Header("What are Ladders?")]
    [SerializeField] private ObjectFinder m_ladderFinder;


    [Header("What is Ground?")]
    [SerializeField] private ObjectFinder m_groundFinder;


     // *** MonoBehaviour interface

    private void Awake()
    {
        string problem = InitReferences();
        if (problem != "")
        {
            Debug.LogError("Failed to properly initialise LadderHandler " + gameObject.name + " for reason: " + problem);
            enabled = false;
            return;
        }
        InitTrapDoors();
    }


    private void FixedUpdate()
    {
        UpdateWatches();
    }

    // private void OnDestroy()
    // {
    // }


    // *** Access

    public Grid LadderGrid() { return m_ladderGrid; }
    public Tilemap LadderMap() { return m_ladderMap; }
    public TilemapCollider2D LadderMapCollider() { return m_ladderCollider; }


    // *** Query

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
    public bool ClosestLadder(Collider2D testCollider, out Vector2 closestPosition, out Vector3Int ladderCell)
    {
        if (!OnLadder(testCollider))
        {
            closestPosition = Vector2.zero;
            ladderCell = Vector3Int.zero;
            return false;
        }
        Vector2 closestPt = m_ladderCollider.ClosestPoint(testCollider.bounds.center);
        ladderCell = m_ladderGrid.WorldToCell(closestPt);
        return CheckNeighbours(testCollider, ladderCell, out closestPosition);
    }


    /// <summary>
    /// MegaMan has reached the top of the ladder under testCollider
    /// Triggers TrapDoor mechanics if necessary
    /// </summary>
    /// <param name="testCollider">Usually the groundCollider of MegaMan's current position</param>
    public void ToppedLadder(Collider2D testCollider)
    {
        Vector2 closestPosition;
        Vector3Int ladderCell;
        ClosestLadder(testCollider, out closestPosition, out ladderCell);
        if (m_trapDoors.ContainsKey((Vector2Int)ladderCell))
        {
            CloseTrapDoor(ladderCell, testCollider);
        }
    }


    public bool OpenTrapDoors(Collider2D testCollider)
    {
        if (!OnLadder(testCollider))
        {
            return false;
        }
        Vector2 closestPosition;
        Vector3Int ladderCell;
        ClosestLadder(testCollider, out closestPosition, out ladderCell);
        if (m_trapDoors.ContainsKey((Vector2Int)ladderCell))
        {
            OpenTrapDoor(ladderCell, testCollider);
            return true;
        }
        return false; // or should this also be true?
    }


    // *** Internal functions

    /// <summary>
    /// Goes through all the cells neighbouring closestCell, returns true and closestPosition if a ladder was found
    /// </summary>
    /// <param name="testCollider">Collider within which to test for ladders</param>
    /// <param name="closestCell">Centre cell from which to start search</param>
    /// <param name="closestPosition">Holds position of ladder, if any</param>
    /// <returns>True if ladder found</returns>
    private bool CheckNeighbours(Collider2D testCollider, Vector3Int closestCell, out Vector2 closestPosition)
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
            closestPosition = Vector2.zero;
            return false;
        }
        closestPosition = bestCoords;
        return foundLadder;
    }


    // *** Cell coordinate conversion

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


    /// <summary>
    /// Close the TrapDoor at the given location
    /// TrapDoor must exist there, but it doesn't have to be open
    /// </summary>
    /// <param name="ladderCell">Location of the TrapDoor</param>
    private void CloseTrapDoor(Vector3Int ladderCell, Collider2D collider)
    {
        // Assume it exists
        Vector2Int key = (Vector2Int)ladderCell;
        TrapDoor td = m_trapDoors[key];
        if (td.Opened())
        {
            Vector3Int groundCell = LadderCellToGroundCell(ladderCell);
            m_groundMap.SetTile(groundCell, td.groundTile);
            td.CloseDoor();
            m_trapDoors[key] = td;
            m_groundCollider.ProcessTilemapChanges();
        }
        // td is now closed
        if (td.defaultPosition != td.position && !m_trapDoorsOffDefault.ContainsKey(key))
        {
            m_trapDoorsOffDefault.Add(key, 0);
        }
    }


    /// <summary>
    /// Open the TrapDoor at the given location
    /// TrapDoor must exist there, but it doesn't have to be closed
    /// </summary>
    /// <param name="ladderCell">Location of the TrapDoor</param>
    private void OpenTrapDoor(Vector3Int ladderCell, Collider2D collider)
    {
        // Assume it exists
        Vector2Int key = (Vector2Int)ladderCell;
        TrapDoor td = m_trapDoors[key];
        if (td.Closed())
        {
            Vector3Int groundCell = LadderCellToGroundCell(ladderCell);
            m_groundMap.SetTile(groundCell, null);
            td.OpenDoor();
            m_trapDoors[key] = td;
            m_groundCollider.ProcessTilemapChanges();
        }
        // td is now closed
        if (td.defaultPosition != td.position && !m_trapDoorsOffDefault.ContainsKey(key))
        {
            m_trapDoorsOffDefault.Add(key, 0);
        }
    }


    private void UpdateWatches()
    {
        List<Vector2Int> keys = new List<Vector2Int>(m_trapDoorsOffDefault.Count);
        foreach (KeyValuePair<Vector2Int, float> entry in m_trapDoorsOffDefault)
        {
            keys.Add(entry.Key);
        }

        float factor = 1;
        float delta = Time.fixedDeltaTime;
        if (m_megaManGroundLadderDetector.IsTouching(m_fakeGroundCollider))
        {
            // Standing on TrapDoor, factor = 0, delta = 0
            factor = 0;
            delta = 0;
        }
        // Increment the time standing on ladder top, or set it to zero, depending on factor and delta
        List<Vector2Int> rmKeyList = new List<Vector2Int>(m_trapDoorsOffDefault.Count);
        foreach (Vector2Int key in keys)
        {
            if (m_trapDoors[key].defaultPosition == DoorPosition.Open)
            {
                m_trapDoorsOffDefault[key] = m_trapDoorsOffDefault[key] * factor + delta;
                if (m_trapDoorsOffDefault[key] >= m_standingOnLadderFloorDisappearDelay)
                {
                    rmKeyList.Add(key);
                    OpenTrapDoor((Vector3Int)key, m_megaManUprightLadderDetector);
                }
            }
        }
        foreach (Vector2Int key in rmKeyList)
        {
            m_trapDoorsOffDefault.Remove(key);
        }
        rmKeyList.Clear();
        if (!m_megaManUprightLadderDetector.IsTouching(m_fakeGroundCollider))
        {
            // Clear all that are default Close
            foreach (Vector2Int key in keys)
            {
                TrapDoor td = m_trapDoors[key];
                if (td.position == td.defaultPosition)
                {
                    // TrapDoor is back in correct position
                    rmKeyList.Add(key);
                    continue;
                }
                if (td.defaultPosition == DoorPosition.Close)
                {
                    if (td.Opened())
                    {
                        CloseTrapDoor((Vector3Int)key, m_megaManUprightLadderDetector);
                    }
                    rmKeyList.Add(key);
                }
            }
        }
        foreach (Vector2Int key in rmKeyList)
        {
            m_trapDoorsOffDefault.Remove(key);
        }
    }

    // *** Initialisation

    private string InitReferences()
    {
        // MegaMan-related references
        m_megaManObject = m_megaManFinder.FindObject();
        if (!m_megaManObject)
        {
            return "Could not find the MegaMan GameObject";
        }
        m_megaManController = m_megaManObject.GetComponent<MegaManController>();
        if (!m_megaManController)
        {
            return "Could not find the MegaManController component";
        }
        m_megaManUprightLadderDetector = m_megaManController.GetUprightLadderDetector();
        m_megaManGroundLadderDetector = m_megaManController.GetGroundLadderDetector();

        // Ladder-related references
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

        // Ground-related references
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
    /// Initialisation only
    /// </summary>
    /// <param name="ladderCell">Location of top of ladder</param>
    private void AddTrapDoor(Vector3Int ladderCell)
    {
        Vector3Int fakeGroundCell = LadderCellToFakeGroundCell(ladderCell);
        TileBase ladderTile = m_ladderMap.GetTile(ladderCell);
        TrapDoor td = new TrapDoor(ladderTile, DoorPosition.Open);
        m_trapDoors.Add((Vector2Int)ladderCell, td);
        // We use fakeGroundCollider to test when MegaMan has left a trapdoor area, so add it here, even though visually not important
        m_fakeGroundMap.SetTile(fakeGroundCell, ladderTile);
    }


    /// <summary>
    /// At a place where the ladder passes through a ground tile - regardless if this is the top of the ladder, we need a trapdoor here
    /// Initialisation only
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
        TrapDoor td = new TrapDoor(groundTile, DoorPosition.Close);
        m_trapDoors.Add((Vector2Int)ladderCell, td);
        m_fakeGroundMap.SetTile(fakeGroundCell, groundTile);
    }


    // BoundsInt cellBounds = m_ladderMap.cellBounds;
    // m_trapDoorIndex = new List<List<int>>();
    private void InitTrapDoors()
    {
        if (!m_groundHandlingEnabled)
        {
            return;
        }
        m_trapDoors = new Dictionary<Vector2Int, TrapDoor>();
        m_trapDoorNull = new TrapDoor(null, DoorPosition.Pizza);
        m_trapDoorsOffDefault = new Dictionary<Vector2Int, float>();
        MakeFakeGroundObjects();
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
        m_fakeGroundCollider.ProcessTilemapChanges();
        m_groundCollider.ProcessTilemapChanges();
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
