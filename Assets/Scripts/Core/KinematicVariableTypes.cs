using System.Collections.Specialized;
using System.Collections.Generic;
using UnityEngine;

public class KinematicVariableTypes {
    BitVector32 m_bv = new BitVector32(0);
    static int m_maxBits = 6;

    public static KinematicVariableTypes None         { get { return new KinematicVariableTypes(0); } }
    public static KinematicVariableTypes Position     { get { return new KinematicVariableTypes(1); } }
    public static KinematicVariableTypes Speed        { get { return new KinematicVariableTypes(2); } }
    public static KinematicVariableTypes Acceleration { get { return new KinematicVariableTypes(4); } }
    public static KinematicVariableTypes Force        { get { return new KinematicVariableTypes(8); } }
    public static KinematicVariableTypes Jerk         { get { return new KinematicVariableTypes(16);} }

    // C# switch hack, look away
    public int Enum { get => m_bv.Data; }
    public const int NoneEnum = 0;
    public const int PositionEnum = 1;
    public const int SpeedEnum = 2;
    public const int AccelerationEnum = 4;
    public const int ForceEnum = 8;
    public const int JerkEnum = 16;

    public KinematicVariableTypes() { }
    public KinematicVariableTypes(int data) { m_bv = new BitVector32(data); }
    // Who needs bit operations?  We can do it with for loops! (Look away, I had no internet at the time)
    public bool Contains(KinematicVariableTypes kv) {
        for (int i = 0; i < m_maxBits; ++i)
        {
            if (m_bv[i] == true && kv.m_bv[i] == false)
            {
                return false;
            }
        }
        return true;
    }
    public void Add(KinematicVariableTypes kv)
    {
        for (int i = 0; i < m_maxBits; ++i) {
            if (kv.m_bv[i] == true) {
                m_bv[i] = true;
            }
        }
    }
    public void Remove(KinematicVariableTypes kv) {
        for (int i = 0; i < m_maxBits; ++i)
        {
            if (kv.m_bv[i] == true)
            {
                m_bv[i] = false;
            }
        }
    }
}
