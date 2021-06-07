using UnityEngine;

public enum Aquiescence
{
    Chaotic,
    Neutral,
    Lawful
}

public interface IDisposition
{
    /// <summary>
    /// Whha is my disposition
    /// </summary>
    public Aquiescence disposition { get; set; }
}