using UnityEngine;

public class UsesExampleInterface : MonoBehaviour, IExample
{
    BitExample exampleBit;

    public int Property { get => exampleBit.Property; set => exampleBit.Property = value; }
    public bool Function() { return exampleBit.Function();}

    void Awake()
    {
        exampleBit = new BitExample();
    }
}