using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TestScript : MonoBehaviour
{
    public Vector2Int m_coords;
    public GameObject m_groundObject;
    public GameObject m_ladderObject;
    
    private GridLayout m_tileGrid;
    private Tilemap m_ladderTileMap;
    private TilemapRenderer m_ladderRenderer;
    private Tilemap m_groundTileMap;

    // private SpriteRenderer m_spriteRenderer;

    private void Awake()
    {
        // m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_ladderTileMap = m_ladderObject.GetComponent<Tilemap>();
        m_ladderRenderer = GetComponent<TilemapRenderer>();
        m_groundTileMap = m_groundObject.GetComponent<Tilemap>();
        m_tileGrid = m_ladderTileMap.layoutGrid;


        Vector3Int cellCoords = new Vector3Int(m_coords.x, m_coords.y, 0);
        Debug.Log("Ladder --> World = " + m_ladderTileMap.GetCellCenterWorld(cellCoords));
        Vector3 groundCellToWorld = m_groundTileMap.GetCellCenterWorld(cellCoords);
        Debug.Log("Ground --> World = " + groundCellToWorld);
        Tile t = m_ladderTileMap.GetTile<Tile>(cellCoords);
        if (t) {Debug.Log("Ladder present.");} else {Debug.Log("Ladder not present.");}
        Tile tGround = m_groundTileMap.GetTile<Tile>(cellCoords);
        Debug.Log("Checking " + cellCoords);
        if (tGround) { Debug.Log("Ground present."); } else { Debug.Log("Ground not present."); }

        BoundsInt cellBounds = m_groundTileMap.cellBounds;
        Debug.Log("Ground bounds = " + cellBounds);
        Debug.Log("Min = " + cellBounds.min + ", max = " + cellBounds.max);
        int nFound = 0;
        string lineStr = "";
        for (int x = cellBounds.min.x; x <= cellBounds.max.x; ++x)
        {
            // At top, all ladder tiles need trapdoors
            lineStr += x.ToString() + ": ";
            for (int y = cellBounds.max.y - 1; y >= cellBounds.min.y; --y)
            {
                Vector3Int curLadderCell = new Vector3Int(x, y, 0);
                if (m_groundTileMap.HasTile(curLadderCell))
                {
                    lineStr += Mathf.Abs(y).ToString() + " X ";
                    ++nFound;
                }
                else
                {
                    lineStr += Mathf.Abs(y).ToString() + " O ";
                }
            }
            lineStr += "\n";
        }
        Debug.Log("Found " + nFound + " ground elements");
        Debug.Log(lineStr);



        //m_spriteRenderer.sprite = t.sprite;
        // Matrix4x4 tf = t.transform;
        // Debug.Log("transform = " + tf);
        // Debug.Log("border = " + t.sprite.border);
        // string vertStr = "";
        // foreach(Vector2 vert in t.sprite.vertices) {vertStr += " " + vert.ToString();}
        // Debug.Log("vertices = " + vertStr);
        // Debug.Log("textureRectOffset = " + t.sprite.textureRectOffset);
        // Debug.Log("textureRect = " + t.sprite.textureRect);
        // Debug.Log("packingRotation = " + t.sprite.packingRotation);
        // Debug.Log("packingMode = " + t.sprite.packingMode);
        // Debug.Log("packed = " + t.sprite.packed);
        // Debug.Log("pivot = " + t.sprite.pivot);
        // Debug.Log("associatedAlphaSplitTexture = " + t.sprite.associatedAlphaSplitTexture);
        // Debug.Log("spriteAtlasTextureScale = " + t.sprite.spriteAtlasTextureScale);
        // Debug.Log("pixelsPerUnit = " + t.sprite.pixelsPerUnit);
        // Debug.Log("texture = " + t.sprite.texture);
        // string triStr = "";
        // foreach (ushort us in t.sprite.triangles) { triStr += " " + us.ToString(); }
        // Debug.Log("triangles = " + triStr);
        // string uvStr = "";
        // foreach(Vector2 vec in t.sprite.uv)  {uvStr += " " + vec.ToString();}
        // Debug.Log("uv = " + t.sprite.uv.GetLength(0) + uvStr);
        // Debug.Log("bounds = " + t.sprite.bounds);
        // Debug.Log("rect = " + t.sprite.rect);
        // Debug.Log("bounds = " + t.sprite.bounds);

        Vector3Int cell = new Vector3Int(-2, 1, 0);
        if (m_groundTileMap.HasTile(cell))
        {
            Debug.Log("YESSS");
        }
        else
        {
            Debug.Log("NNNOOOO");
        }
        TileBase tg = m_groundTileMap.GetTile(cell);
        Debug.Log(tg.name);

        Vector3 newPosition = new Vector3(-1, -2, 0);
        // TilemapRenderer newTmr = Instantiate(m_ladderRenderer);

        GameObject cLadderObject = Instantiate(m_ladderObject, m_tileGrid.transform);
        Destroy(cLadderObject.GetComponent<LadderHandler>());
        Destroy(cLadderObject.GetComponent<ObjectFinder>());
        Tilemap cLadderMap = cLadderObject.GetComponent<Tilemap>();
        TilemapRenderer cLadderRenderer = cLadderObject.GetComponent<TilemapRenderer>();
        Grid cLadderGrid = cLadderMap.layoutGrid;
        cLadderMap.size = new Vector3Int(10, 10, 1);
        Debug.Log(cLadderMap.cellBounds);
        Debug.Log(cLadderRenderer.chunkSize);
        Vector3Int targetCell = new Vector3Int(1,1,0);

////////
        BoundsInt newBounds = cLadderMap.cellBounds;
        Debug.Log("Ground bounds = " + newBounds);
        Debug.Log("Min = " + newBounds.min + ", max = " + newBounds.max);
        string newLineStr = "";
        int nNonZero = 0;
        for (int x = newBounds.min.x; x <= newBounds.max.x; ++x)
        {
            // At top, all ladder tiles need trapdoors
            newLineStr += x.ToString() + ": ";
            for (int y = newBounds.max.y - 1; y >= newBounds.min.y; --y)
            {
                Vector3Int curLadderCell = new Vector3Int(x, y, 0);
                cLadderMap.SetTile(curLadderCell, tg);
                Vector3 worldPos = cLadderMap.GetCellCenterWorld(targetCell);
                bool nonZero = worldPos != Vector3.zero;
                bool hasTile = cLadderMap.HasTile(curLadderCell);
                if (nonZero && hasTile)
                {
                    ++nNonZero;
                    newLineStr += Mathf.Abs(y).ToString() + " X ";
                }
                else if (nonZero)
                {
                    newLineStr += Mathf.Abs(y).ToString() + " N ";
                }
                else if (hasTile)
                {
                    newLineStr += Mathf.Abs(y).ToString() + " T ";
                }
                else
                {
                    newLineStr += Mathf.Abs(y).ToString() + " O ";
                }
            }
            newLineStr += "\n";
        }
        Debug.Log(newLineStr);
////////

        Debug.Log(targetCell + " <-- Ladder | World --> " + cLadderMap.GetCellCenterWorld(targetCell));
        //Debug.Log(cLadderGrid.name);

        // Tilemap newTilemap = Instantiate(m_ladderTileMap);//, gameObject.transform);
        // newTilemap.ClearAllTiles();
        // newTilemap.size = new Vector3Int(10,10,1);
        // newTilemap.BoxFill(new Vector3Int(2,2,0), tg, 1, 1, 3, 3);
        // newTilemap.RefreshAllTiles();
        // Debug.Log(newTilemap.cellBounds);
        //Vector3Int newOffset = new Vector3Int(8,0,0);
        //newTilemap.SetTile(newOffset, tg);

        // Data coming out of test Sprite
        // transform            1 0 0 0  ,  0 1 0 0  ,  0 0 1 0  ,  0 0 0 1
        // border               (0.0, 0.0, 0.0, 0.0)
        // vertices             (-0.5, 0.5) (0.5, 0.5) (-0.5, -0.5) (0.5, -0.5)
        // textureRectOffset    (0.0, 0.0)
        // textureRect          (x:96.00, y:144.00, width:16.00, height:16.00)
        // packingRotation      None
        // packingMode          Tight
        // packed               False
        // pivot                (8.0, 8.0)
        // associatedAlphaSplitTexture      null
        // spriteAtlasTextureScale          1
        // pixelsPerUnit        16
        // texture              Starter Tileset Red (UnityEngine.Texture2D)
        // triangles            0 1 2 2 1 3
        // uv                   (0.7, 0.6) (0.8, 0.6) (0.7, 0.5) (0.8, 0.5)
        // bounds               Center: (0.0, 0.0, 0.0), Extents: (0.5, 0.5, 0.1)
        // rect                 (x:96.00, y:144.00, width:16.00, height:16.00)
        // bounds               Center: (0.0, 0.0, 0.0), Extents: (0.5, 0.5, 0.1)

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
