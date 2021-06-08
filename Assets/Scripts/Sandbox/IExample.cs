using UnityEngine;

public interface IExample
{
    public int Property { get; set; }
    public bool Function();
}

public class BitExample : IExample
{
    private int property;

    public int Property { get => property; set => property = value; }
    public bool Function()
    {
        if (property > 0)
        {
            return true;
        }
        return false;
    }
}