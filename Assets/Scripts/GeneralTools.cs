using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Tilemaps;

public static class GeneralTools {
    public static bool IsInLayerMask(GameObject obj, LayerMask layerMask) {
        return ((layerMask.value & (1 << obj.layer)) > 0);
    }

    public static bool Assert(bool test) {
        if (test) {
            return true;
        } else {
            Debug.LogError("Failed assert");
            return false;
        }
    }
    public static bool AssertNotNull<T>(T entity, string description) {
        if (entity == null) {
            Debug.LogError("Null object returned: " + description + ", Type=" + entity.GetType());
            return false;
        }
        return true;
    }

    /// <summary>
    /// Checks target collider's Loyalty against my own.  If the pairing is valid, return the IGetHurt interface of it.
    /// </summary>
    /// <param name="target">The target collider I hit</param>
    /// <param name="myCollider">My collider</param>
    /// <param name="myTeam">My loyalty</param>
    /// <param name="origin">A string clue to help debugging</param>
    /// <returns>The IGetHurt interface of the collider I hit</returns>
    public static IGetHurt ApplyRulesOfEngagement(Collider2D target, Collider2D myCollider, Team myTeam, string origin) {
        if (target == myCollider) {
            Debug.Log("ApplyRulesOfEngagement - I found my own collider from " + origin);
        }
        if (target.tag.Contains("IgnoreHits")) {
            // As the tag says
            return null;
        }
        // Use '...InParent' search because complex prefabs may have colliders as children
        ILoyalty targetLoyalty = target.gameObject.GetComponentInParent<ILoyalty>();
        if (targetLoyalty == null) {
            // Fail quiet, may not be that kind of GameObject
            return null;
        }
        Team targetTeam = targetLoyalty.side;
        switch (myTeam) {
            case Team.Neutral:
                // Neutral can hit anyone
                break;
            case Team.BadGuys:
                // BadGuys can only hit GoodGuys
                if (targetTeam == Team.GoodGuys) {
                    break;
                } else {
                    return null;
                }
            case Team.GoodGuys:
                // GoodGuys can hit Neutral and BadGuys
                if (targetTeam == Team.GoodGuys) {
                    return null;
                }
                break;
            default:
                Debug.LogError("Unhandled Team, ApplyRulesOfEngagement " + origin);
                break;
        }
        // Again, using ...InParent function because complex prefabs may have IGetHurt component at a higher level
        IGetHurt entity = target.GetComponentInParent<IGetHurt>();
        // No need to AssertNotNull because it may not be 'hurtable'
        return entity;
    }

    /// <summary>
    /// Return bit from int
    /// </summary>
    public static bool GetBit(this byte b, int bitNumber) {
        return (b & (1 << bitNumber-1)) != 0;
    }

    // Waiting for upgrade to .NET
    public static void ArrayDotFill<T>(ref T[] array, T element) {
        for (int i = 0; i < array.Length; ++i) {
            array[i] = element;
        }
    }

    /// <summary>
    /// Returns true if the generic type T is a float or int
    /// </summary>
    public static bool OneD<T>() {
        return typeof(float).IsAssignableFrom(typeof(T)) || typeof(int).IsAssignableFrom(typeof(T));
    }
    /// <summary>
    /// Returns true if the generic type T is a Vector2 or Vector2Int
    /// </summary>
    public static bool TwoD<T>() {
        return typeof(Vector2).IsAssignableFrom(typeof(T)) || typeof(Vector2Int).IsAssignableFrom(typeof(T));
    }
    /// <summary>
    /// Returns true if the generic type T is a Vector3 or Vector3Int
    /// </summary>
    public static bool ThreeD<T>() {
        return typeof(Vector3).IsAssignableFrom(typeof(T)) || typeof(Vector3Int).IsAssignableFrom(typeof(T));
    }
    /// <summary>
    /// Returns true if the generic type T is a Vector4
    /// </summary>
    public static bool FourD<T>() {
        return typeof(Vector4).IsAssignableFrom(typeof(T));
    }
    /// <summary>
    /// Returns number of dimensions based on the type T, with float = 1, Vector2 = 2, ... up to Vector4.  -1 if unknown.
    /// </summary>
    public static int NDimensions<T>() {
        if (OneD<T>())   return 1;
        if (TwoD<T>())   return 2;
        if (ThreeD<T>()) return 3;
        if (FourD<T>())  return 4;
        return -1;
    }

}

// public interface ITraits<T> {
//     public T Zero(T dummy);
//     public T SmoothDamp(T current, T target, ref T currentVelocity, float smoothTime, float maxSpeed, float deltaTime);
// }

// public class Traits<T> : ITraits<T> {
//     public static readonly ITraits<T> P = Traits.P as ITraits<T> ?? new Traits<T>();
//     public T Zero(T dummy) { throw new System.NotImplementedException(); }
//     public T SmoothDamp(T current, T target, ref T currentVelocity, float smoothTime, float maxSpeed, float deltaTime) {
//         throw new System.NotImplementedException();
//     }
// }
// class Traits : ITraits<float> , ITraits<Vector2>, ITraits<Vector3> {
//     public static Traits P = new Traits(); 
//     public float Zero(float dummy) { return 0f; }
//     public Vector2 Zero(Vector2 dummy) { return Vector2.zero; }
//     public Vector3 Zero(Vector3 dummy) { return Vector3.zero; }
//     public float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed, float deltaTime) {
//         Debug.Log("Float SmoothDamp");
//         return Mathf.SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
//     }
//     public Vector2 SmoothDamp(Vector2 current, Vector2 target, ref Vector2 currentVelocity, float smoothTime, float maxSpeed, float deltaTime) {
//         Debug.Log("Vector2 SmoothDamp");
//         return Vector2.SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
//     }
//     public Vector3 SmoothDamp(Vector3 current, Vector3 target, ref Vector3 currentVelocity, float smoothTime, float maxSpeed, float deltaTime) {
//         Debug.Log("Vector3 SmoothDamp");
//         return Vector3.SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
//     }
// }
// class Traits : ITraits<Vector2> {
//     public static Traits P = new Traits(); 
//     public Vector2 Zero {get { return Vector2.zero; } }
//     public Vector2 SmoothDamp(Vector2 current, Vector2 target, ref Vector2 currentVelocity, float smoothTime, float maxSpeed, float deltaTime) {
//         return Vector2.SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
//     }
// }
// public class Traits<T> {
//     public virtual T Zero { get; }
//     public virtual T SmoothDamp(
//         T current,
//         T target,
//         ref T currentVelocity,
//         float smoothTime,
//         float maxSpeed,
//         float deltaTime
//     ) {
//         throw new System.NotImplementedException();
//     }
// }

// public class TraitsFloat : Traits<float> {
//     public override float Zero { get=>0f; }
//     public override float SmoothDamp(
//         float current,
//         float target,
//         ref float currentVelocity,
//         float smoothTime,
//         float maxSpeed,
//         float deltaTime
//     ) {
//         return Mathf.SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
//     }
// }
// public class TraitsVector2 : Traits<Vector2> {
//     public override Vector2 Zero { get=>Vector2.zero; }
//     public override Vector2 SmoothDamp(
//         Vector2 current,
//         Vector2 target,
//         ref Vector2 currentVelocity,
//         float smoothTime,
//         float maxSpeed,
//         float deltaTime
//     ) {
//         return Vector2.SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
//     }
// }
// public class TraitsVector3 : Traits<Vector3> {
//     public override Vector3 Zero { get=>Vector3.zero; }
// }
// public class TraitsQuaternion : Traits<Quaternion> {
//     public override Quaternion Zero { get=>Quaternion.identity; }
// }


// // Testing GeneralTools::One,Two,ThreeD
// string tests = "";
// tests += "int:" + GeneralTools.OneD<int>() + "," + GeneralTools.TwoD<int>()+ "," + GeneralTools.ThreeD<int>() + "\n";
// tests += "float:" + GeneralTools.OneD<float>() + "," + GeneralTools.TwoD<float>()+ "," + GeneralTools.ThreeD<float>() + "\n";
// tests += "Vector2Int:" + GeneralTools.OneD<Vector2Int>() + "," + GeneralTools.TwoD<Vector2Int>()+ "," + GeneralTools.ThreeD<Vector2Int>() + "\n";
// tests += "Vector2:" + GeneralTools.OneD<Vector2>() + "," + GeneralTools.TwoD<Vector2>()+ "," + GeneralTools.ThreeD<Vector2>() + "\n";
// tests += "Vector3Int:" + GeneralTools.OneD<Vector3Int>() + "," + GeneralTools.TwoD<Vector3Int>()+ "," + GeneralTools.ThreeD<Vector3Int>() + "\n";
// tests += "Vector3:" + GeneralTools.OneD<Vector3>() + "," + GeneralTools.TwoD<Vector3>()+ "," + GeneralTools.ThreeD<Vector3>() + "\n";
// tests += "Vector4:" + GeneralTools.OneD<Vector4>() + "," + GeneralTools.TwoD<Vector4>()+ "," + GeneralTools.ThreeD<Vector4>() + "\n";
// Debug.Log(tests);
// Result - it works:
// int:        True, False,False
// float:      True, False,False
// Vector2Int: False,True, False
// Vector2:    False,True, False
// Vector3Int: False,False,True
// Vector3:    False,False,True
// Vector4:    False,False,False
//
// UnityEngine.Debug:Log (object)
// Sandbox:Update () (at Assets/Sandbox.cs:39)
