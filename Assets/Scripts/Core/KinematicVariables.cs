using System.Collections.Specialized;
using System.Collections.Generic;
using UnityEngine;

public class KinematicVariables {
    BitVector32 m_bv = new BitVector32(0);
    static int m_maxBits = 6;

    public static KinematicVariables None         { get { return new KinematicVariables(0); } }
    public static KinematicVariables Position     { get { return new KinematicVariables(1); } }
    public static KinematicVariables Speed        { get { return new KinematicVariables(2); } }
    public static KinematicVariables Acceleration { get { return new KinematicVariables(4); } }
    public static KinematicVariables Force        { get { return new KinematicVariables(8); } }
    public static KinematicVariables Jerk         { get { return new KinematicVariables(16);} }

    // C# switch hack, look away
    public int Enum { get => m_bv.Data; }
    public const int NoneEnum = 0;
    public const int PositionEnum = 1;
    public const int SpeedEnum = 2;
    public const int AccelerationEnum = 4;
    public const int ForceEnum = 8;
    public const int JerkEnum = 16;

    public KinematicVariables() { }
    public KinematicVariables(int data) { m_bv = new BitVector32(data); }
    // Who needs bit operations?  We can do it with for loops! (Look away, I had no internet at the time)
    public bool Contains(KinematicVariables kv) {
        for (int i = 0; i < m_maxBits; ++i)
        {
            if (m_bv[i] == true && kv.m_bv[i] == false)
            {
                return false;
            }
        }
        return true;
    }
    public void Add(KinematicVariables kv)
    {
        for (int i = 0; i < m_maxBits; ++i) {
            if (kv.m_bv[i] == true) {
                m_bv[i] = true;
            }
        }
    }
    public void Remove(KinematicVariables kv) {
        for (int i = 0; i < m_maxBits; ++i)
        {
            if (kv.m_bv[i] == true)
            {
                m_bv[i] = false;
            }
        }
    }
}
