#if MULTITHREADING
    using System.Threading;
#endif
using System.Collections.Generic;
using UnityEngine;

public static class DataRegistryRoot {
    private static long m_currentModTag = long.MinValue; // Current ModTag value
    private static long m_currentId = long.MinValue;

    public static ModTag GetModTag() {
        #if MULTITHREADING
            return new ModTag(Interlocked.Increment(ref m_currentModTag));
        #else
            return new ModTag(++m_currentModTag);
        #endif
    }

    // // Assume no overflow 18,446,744,073,709,551,615 (pentillions)
    // private static int m_modTagWraps = 0; // Number of times the tag has wrapped around (realistically, always 0)
    // /// <summary>
    // /// Returns the number of times the ModTag has wrapped around
    // /// </summary>
    // public static int ModTagWraps { get => m_modTagWraps; }
}

/// <summary>
/// A basic header for objects that can be stored in the DataRegistry.  Other objects may depend on data in this object.
/// This can be attached to the actual object, or a seperate header used to refer to that object
/// </summary>
public interface IDataObject {
    /// <summary>
    /// Name assigned to this object, can be duplicated
    /// </summary>
    public string Name { get; }
    /// <summary>
    /// A unique ID number, assigned upon creation, and never changes
    /// </summary>
    public long Id { get; }
    /// <summary>
    /// A unique tag indicating when it was last modified, relative to other tags
    /// </summary>
    /// <value></value>
    public ModTag MTag { get; }
    /// <summary>
    /// The DataRegistry that I'm registered in, or null if orphan
    /// </summary>
    public IDataRegistry Parent { get; }
    /// Convert this to an IDerivedDataObject, or null if it is not one.
    /// </summary>
    /// <returns></returns>
    public IDerivedDataObject DerivedDataObject();
    /// <summary>
    /// Return a header-only IDataObject copy of this object
    /// </summary>
    public IDataObject CloneHeader();
}

/// <summary>
/// Additional interface components for DataObjects that depend on other data objects.
/// I.e. these objects have 'derived' data.
/// </summary>
public interface IDerivedDataObject : IDataObject {
    /// <summary>
    /// Other data objects on which this one depends
    /// </summary>
    public List<IDataObject> DependsOn { get; }
    /// <summary>
    /// Bring all my derived data up to date
    /// </summary>
    public void Update();
    /// <summary>
    /// True if I am up to date
    /// </summary>
    public bool UpToDate();
}

public interface IDataRegistry : IDataObject {
    public IDataObject FindDataObject(long id, bool recursive=true);
    public List<IDataObject> FindDataObjects(string name, bool recursive=true);
    public List<string> TableOfContents(bool recursive=true);
    /// <summary>
    /// List of all child objects' ids, optionally recursive.
    /// </summary>
    public List<long> Index(bool recursive=true);
    public void Register(IDataObject obj);
    public bool Unregister(IDataObject obj);
    public bool Unregister(long id);
}

/// <summary>
/// Modification tag, assigned incrementally.
/// Basically just a wrapped long now.  Was more.  Can be more.  But just a long right now.
/// </summary>
public struct ModTag {
    long m_tag;
    public ModTag(long tag) {
        m_tag = tag;
    }
    // *** Static operators
    public override bool Equals(object obj) {
        if (!(obj is ModTag))
           return false;

        ModTag mt = (ModTag)obj;
        if (mt.m_tag == m_tag) {
            return true;
        }
        return false;
    }
    public override int GetHashCode() { return m_tag.GetHashCode(); }
    public static bool operator== (ModTag lhs, ModTag rhs) { return lhs.m_tag == rhs.m_tag; }
    public static bool operator!= (ModTag lhs, ModTag rhs) { return lhs.m_tag != rhs.m_tag; }
    public static bool operator>= (ModTag lhs, ModTag rhs) { return lhs.m_tag >= rhs.m_tag; }
    public static bool operator<= (ModTag lhs, ModTag rhs) { return lhs.m_tag <= rhs.m_tag; }
    public static bool operator> (ModTag lhs, ModTag rhs) { return lhs.m_tag > rhs.m_tag; }
    public static bool operator< (ModTag lhs, ModTag rhs) { return lhs.m_tag < rhs.m_tag; }
    public static ModTag operator+ (ModTag lhs, ModTag rhs) { return new ModTag(lhs.m_tag + lhs.m_tag);}
    public static ModTag operator- (ModTag lhs, ModTag rhs) { return new ModTag(lhs.m_tag - lhs.m_tag);}
    public static ModTag operator++(ModTag mt) { return new ModTag(mt.m_tag + 1);}
    public static ModTag operator--(ModTag mt) { return new ModTag(mt.m_tag - 1);}
    // The rest of this stuff is legacy from when I was accommodating our long tag being incremented past 18.4 pentillion
    // public static bool operator> (ModTag lhs, ModTag rhs) {
    //     if (DataRegistryRoot.ModTagWraps == 0) {
    //         return lhs.m_tag > rhs.m_tag;
    //     }
    //     // There is a wrap-around in the ModTags, be careful
    //     int ls = GetSegment(lhs.m_tag);
    //     int rs = GetSegment(rhs.m_tag);
    //     int deltaSeg = rs - ls;
    //     if (deltaSeg > 4) {
    //         return true;
    //     } else if (deltaSeg < -4) {
    //         return false;
    //     } else {
    //         return lhs.m_tag > rhs.m_tag;
    //     }
    // }
    // public static bool operator< (ModTag lhs, ModTag rhs) {
    //     if (DataRegistryRoot.ModTagWraps == 0) {
    //         return lhs.m_tag < rhs.m_tag;
    //     }
    //     // There is a wrap-around in the ModTags, be careful
    //     int ls = GetSegment(lhs.m_tag);
    //     int rs = GetSegment(rhs.m_tag);
    //     int deltaSeg = rs - ls;
    //     if (deltaSeg > 4) {
    //         return false;
    //     } else if (deltaSeg < -4) {
    //         return true;
    //     } else {
    //         return lhs.m_tag < rhs.m_tag;
    //     }
    // }
    // Breaking number line into segments may be useful at some point, but was used for wrap-around ModTags, which we assume won't happen
    // public static readonly long[] Segments = new long[] {
    //     long.MinValue/4*3,
    //     long.MinValue/2,
    //     long.MinValue/4,
    //     0,
    //     long.MaxValue/4,
    //     long.MaxValue/2,
    //     long.MaxValue/4*3,
    //     long.MaxValue
    // };
    // public static int GetSegment(long tag) {
    //     int seg = 0;
    //     while (tag > Segments[seg++]) {}
    //     return --seg;
    // }
}


// // Test code for GetSegment function
//     int [] stops = new int[] { -6, -4, -2, 0, 2, 4, 6, 8, 10 };
//     for (int testMe = -8; testMe < 15; ++testMe) {
//         int s = 0;
//         while (testMe > stops[s++] && s < 9) {}
//         s--;
//         Console.WriteLine(testMe + ": " + s + ", stops="+stops[s]);
//     }
// // -8: 0, stops=-6
// // -7: 0, stops=-6
// // -6: 0, stops=-6
// // -5: 1, stops=-4
// // -4: 1, stops=-4
// // -3: 2, stops=-2
// // -2: 2, stops=-2
// // -1: 3, stops=0
// // 0: 3, stops=0
// // 1: 4, stops=2
// // 2: 4, stops=2
// // 3: 5, stops=4
// // 4: 5, stops=4
// // 5: 6, stops=6
// // 6: 6, stops=6
// // 7: 7, stops=8
// // 8: 7, stops=8
// // 9: 8, stops=10
// // 10: 8, stops=10
// // 11: 8, stops=10
// // 12: 8, stops=10
// // 13: 8, stops=10
// // 14: 8, stops=10
