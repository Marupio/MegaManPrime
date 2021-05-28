using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Tilemaps;

public class GeneralTools
{
    public static bool IsInLayerMask(GameObject obj, LayerMask layerMask)
    {
        return ((layerMask.value & (1 << obj.layer)) > 0);
    }

    public static Tile CombineTiles(Tile under, Tile over)
    {
        Tile both = under;

        return both;
    }
}


/// <summary>
/// Script adapted from version posted by NitromeSteed on unity forums at:
///     https://forum.unity.com/threads/merging-multiple-textures-sprites-into-one-texture.583393/
/// </summary>
public class SpriteMerge : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;// assumes you've dragged a reference into this
    public Transform mergeInput;// a transform with a bunch of SpriteRenderers you want to merge

    public enum RotationFlip
    {
        None, One, Two, Three, NoneFlip, OneFlip, TwoFlip, ThreeFlip
    }

    void Start()
    {
        // spriteRenderer.sprite = Create(new Vector2Int(2048, 2048), mergeInput);
    }

    public static Sprite Create(Vector2Int size, Transform input)
    {
        // return Create(size, input, new Vector2Int());
        return null;
    }

    // Tilemap
    // public Sprite GetSprite(Vector3Int position);
    // public TileBase GetTile(Vector3Int position);
    // public T GetTile<T>(Vector3Int position) where T : TileBase;
    // public bool HasTile(Vector3Int position);
    // public void RefreshAllTiles();
    // public void RefreshTile(Vector3Int position);
    // public void SetTile(Vector3Int position, TileBase tile);
    // public void SetTiles(Vector3Int[] positionArray, TileBase[] tileArray);


    /* Takes a transform holding many sprites as input and creates one flattened sprite out of them */
    public static Sprite MergeSprites(Vector2Int size, List<Sprite> input, Vector2Int offset)
    {
        if (input.Count == 0)
        {
            Debug.Log("No sprites supplied for SpriteMerge");
            return null;
        }

        Texture2D targetTexture = new Texture2D(size.x, size.y, TextureFormat.RGBA32, false, false);
        targetTexture.filterMode = FilterMode.Point;
        Color[] targetPixels = targetTexture.GetPixels();
        for (int i = 0; i < targetPixels.Length; i++) targetPixels[i] = Color.clear;// default pixels are not set
        int targetWidth = targetTexture.width;
        int tempX, tempY;

        foreach(Sprite spriteI in input)
        {
            // SpriteRenderer sr = spriteRenderers[i];
            // Check for rotation:
            // not only does rotation do funny things with x/y, but by doing so you're jumping into the next tile
            // this means that the drawing ends up 1 pixel off, because you're not at the bottom of the current tile
            // but at the top of the next tile. Only 90 degree turns and sprite.flipX implemented so far.
            RotationFlip rot = RotationFlip.None;
            // float r = sr.transform.localRotation.eulerAngles.z;
            Vector2 pivot = spriteI.pivot;
            // if (r == 90)
            // {
            //     if (sr.flipX)
            //     {
            //         rot = RotationFlip.OneFlip;
            //         pivot = new Vector2(-pivot.y + 1, -pivot.x + 1);
            //     }
            //     else
            //     {
            //         rot = RotationFlip.One;
            //         pivot = new Vector2(-pivot.y + 1, pivot.x);
            //     }
            // }
            // else if (r == 180)
            // {
            //     if (sr.flipX)
            //     {
            //         rot = RotationFlip.TwoFlip;
            //         pivot = new Vector2(pivot.x, -pivot.y + 1);
            //     }
            //     else
            //     {
            //         rot = RotationFlip.Two;
            //         pivot = new Vector2(pivot.x + 1, -pivot.y + 1);
            //     }
            // }
            // else if (r == 270)
            // {// fun fact: if you enter -90 into the inspector it gets picked up as 270 in here
            //     if (sr.flipX)
            //     {
            //         rot = RotationFlip.ThreeFlip;
            //         pivot = new Vector2(pivot.y, pivot.x);
            //     }
            //     else
            //     {
            //         rot = RotationFlip.Three;
            //         pivot = new Vector2(pivot.y, -pivot.x + 1);
            //     }
            // }
            // else if (sr.flipX)
            // {
            //     rot = RotationFlip.NoneFlip;
            //     pivot.x++;
            // }

            // Original code
            // Vector2 position = (Vector2)sr.transform.localPosition - pivot;
            // Debug.Log(position);
            // Vector2Int pRounded = new Vector2Int((int)position.x, (int)position.y) + offset;

            // Posted bugfix by user Pnvanol
        //     Vector2 position = (Vector2)sr.transform.localPosition * sr.sprite.pixelsPerUnit - sr.sprite.pivot;
        //     Vector2Int pRounded = new Vector2Int((int)position.x, (int)position.y);
        //     int sourceWidth = sr.sprite.texture.width;
        //     // if read/write is not enabled on texture (under Advanced) then this next bit throws an error
        //     // no way to check this without Try/Catch :(
        //     Color[] sourcePixels = null;
        //     try
        //     {
        //         sourcePixels = sr.sprite.texture.GetPixels();
        //     }
        //     catch (UnityException e)
        //     {
        //         if (e.Message.StartsWith("Texture '" + sr.sprite.texture.name + "' is not readable"))
        //         {
        //             Debug.LogError("Please enable read/write on texture [" + sr.sprite.texture.name + "]");
        //         }
        //     }
        //     for (int j = 0; j < sourcePixels.Length; j++)
        //     {
        //         Color source = sourcePixels[j];
        //         int x = j % sourceWidth;
        //         int y = j / sourceWidth;

        //         if (rot != 0)
        //         {
        //             tempX = x;
        //             tempY = y;
        //             switch (rot)
        //             {
        //                 case RotationFlip.NoneFlip:
        //                     x = -tempX;
        //                     y = tempY;
        //                     break;
        //                 case RotationFlip.One:
        //                     x = -tempY;
        //                     y = tempX;
        //                     break;
        //                 case RotationFlip.OneFlip:
        //                     x = -tempY;
        //                     y = -tempX;
        //                     break;
        //                 case RotationFlip.Two:
        //                     x = -tempX;
        //                     y = -tempY;
        //                     break;
        //                 case RotationFlip.TwoFlip:
        //                     x = tempX;
        //                     y = -tempY;
        //                     break;
        //                 case RotationFlip.Three:
        //                     x = tempY;
        //                     y = -tempX;
        //                     break;
        //                 case RotationFlip.ThreeFlip:
        //                     x = tempY;
        //                     y = tempX;
        //                     break;
        //             }
        //         }

        //         int index = (x + pRounded.x) + (y + pRounded.y) * targetWidth;
        //         if (index > 0 && index < targetPixels.Length)
        //         {
        //             Color target = targetPixels[index];
        //             if (target.a > 0)
        //             {
        //                 // alpha blend when we've already written to the target
        //                 float sourceAlpha = source.a;
        //                 float invSourceAlpha = 1f - source.a;
        //                 float alpha = sourceAlpha + invSourceAlpha * target.a;
        //                 Color result = (source * sourceAlpha + target * target.a * invSourceAlpha) / alpha;
        //                 result.a = alpha;
        //                 source = result;
        //             }
        //             targetPixels[index] = source;
        //         }
        //     }
        // }

        // targetTexture.SetPixels(targetPixels);
        // targetTexture.Apply(false, true);// read/write is disabled in 2nd param to free up memory
        return Sprite.Create(targetTexture, new Rect(new Vector2(), size), new Vector2(), 1, 1, SpriteMeshType.FullRect, new Vector4(2, 2, 2, 2));
    }
    return null;
}
}


