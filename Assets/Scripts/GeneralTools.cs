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
}


