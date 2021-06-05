using UnityEngine;
using System.Collections;

//public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
public class SnapToGrid : MonoBehaviour
{
#if UNITY_EDITOR
    public bool snapToGrid = true;
    public float snapValue = 0.5f;

    public bool sizeToGrid = false;
    public float sizeValue = 0.25f;
#endif
}