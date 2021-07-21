using UnityEngine;

public static class GeneralGameTools {

    public static bool IsInLayerMask(GameObject obj, LayerMask layerMask) {
        return ((layerMask.value & (1 << obj.layer)) > 0);
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
}