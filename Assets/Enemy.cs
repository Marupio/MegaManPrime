using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy base class with interfaces used by other classes that interact with it
/// </summary>
abstract public class Enemy : MonoBehaviour
{
    public abstract void Hit(Transform hitPoint, int damage, GameObject bullet);
}
