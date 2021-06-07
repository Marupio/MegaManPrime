using UnityEngine;

public enum Team
{
    Neutral,
    GoodGuys,
    BadGuys
}

public interface ILoyalty
{
    /// <summary>
    /// To what side am I loyal?
    /// </summary>
    public Team side { get; set; }
}