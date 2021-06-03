using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnappedEnemy<T> : SnapToTileGrid where T : Enemy
{
    public T enemy;

#if UNITY_EDITOR
    public override void OnValidate()
    {
        base.OnValidate();
    }
#endif
}
