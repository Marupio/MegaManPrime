using UnityEngine;
using UnityEditor;
using System.Collections;

[InitializeOnLoad]
[CustomEditor(typeof(SnapToTileGrid), true)]
[CanEditMultipleObjects]
public class SnapToTileGridEditor : Editor
{
    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        SnapToTileGrid actor = target as SnapToTileGrid;
        actor.UpdateGridValues();
        if (actor.snapToTilemapGrid)
            actor.transform.position = RoundTransform (actor.transform.position, actor.snapValue, actor.snapOffset);

        if (actor.sizeToTilemapGrid)
            actor.transform.localScale = RoundTransform(actor.transform.localScale, actor.sizeValue, Vector2.zero);
    }
    
        // The snapping code
    private Vector3 RoundTransform (Vector3 v, Vector2 snapValue, Vector2 snapOffset)
    {
        return new Vector3
        (
            snapValue.x * Mathf.Round((v.x - snapOffset.x) / snapValue.x) + snapOffset.x,
            snapValue.y * Mathf.Round((v.y - snapOffset.y) / snapValue.y) + snapOffset.y,
            v.z
        );
    }
}