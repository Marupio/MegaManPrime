public class AxisProfile
{
    string m_name;
    AxisSource m_axisSource;
    AxisMovement m_axisMovement;
    bool m_smoothingEnabled;
    float m_smoothingTime;

    public string Name { get => m_name; set => m_name = value; }
    public AxisSource SourceType { get=> m_axisSource; set => m_axisSource = value; }
    public AxisMovement AxisMovement { get => m_axisMovement; set => m_axisMovement = value; }
    public bool SmoothingEnabled
    {
        get => m_smoothingEnabled;
        set { m_smoothingEnabled = InternalSmoothingAllowed() ? value : false; }
    }
    public float SmoothingTime
    {
        get => m_smoothingTime;
        set { m_smoothingTime = InternalSmoothingAllowed() ? value : 0;}
    }

    public AxisProfile(string name, AxisSource axisSource, AxisMovement axisMovement, bool smoothing)
    {
        m_name = name;
        m_axisSource = axisSource;
        m_axisMovement = axisMovement;
        SmoothingEnabled = smoothing;
    }

    /// <summary>
    /// Enforces no smoothing for Instantaneous movement control
    /// </summary>
    protected bool InternalSmoothingAllowed()
    {
        ImpulseMovement impulseType = m_axisMovement.ImpulseType();
        if (impulseType != null && impulseType.Instantaneous)
        {
            return false;
        }
        return true;
    }
}
