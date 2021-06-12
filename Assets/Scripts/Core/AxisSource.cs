// External sources that affect movement
public enum AxisSourceType
{
    None,                   // Billiards
    ConstantSpeed,          // Conveyor (includes constant rotation)
    ConstantAcceleration,   // Gravity (includes constant angular acceleration)
    ConstantForce           // Rocket (includes constant torque)
}


public struct AxisSource
{
    public AxisSourceType Type;
    public float Value;
    public AxisSource(AxisSource asIn)
    {
        Type = asIn.Type;
        Value = asIn.Value;
    }
    public AxisSource(AxisSourceType typeIn, float valueIn)
    {
        Type = typeIn;
        Value = valueIn;
    }
}
