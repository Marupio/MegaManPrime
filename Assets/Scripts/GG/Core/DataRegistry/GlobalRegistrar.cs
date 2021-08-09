using System.Collections.Generic;
#if MULTITHREADING
    using System.Threading;
#endif

public static class GlobalRegistrar {
    // *** ModTag static data
    public static readonly ModTag UnTaggedModTag;
    private static long m_currentModTag = long.MinValue+1; // Current ModTag value
    public const long ModTagUntagged = long.MinValue;

    // *** Id static data
    private static long m_currentId = long.MinValue+1;
    // DataObjectFilter depends on this value being long.MinValue
    public const long IdAnonymous = long.MinValue;

    public static ModTag GetNextModTag() {
        #if MULTITHREADING
            return new ModTag(Interlocked.Increment(ref m_currentModTag));
        #else
            return new ModTag(++m_currentModTag);
        #endif
    }
    public static void UpdateModTag(ref ModTag mtag) {
        #if MULTITHREADING
            mtag.Tag = Interlocked.Increment(ref m_currentModTag));
        #else
            mtag.Tag = ++m_currentModTag;
        #endif
    }
    public static long GetNextId() { return ++m_currentId; }
    public static CloneResult Clone(IObj target, bool recursive=true) {
        return new CloneResult(target, target.Clone());
    }
    public static CloneResult Clone(IObjRegistry target, bool recursive, bool cloneDerivedSources, IObjRegistry parent=null) {
        if (!cloneDerivedSources) {
            if (recursive) {
                return target.CloneFamily(parent);
            } else {
                return new CloneResult(target, target.Clone(parent));
            }
        }
        // TODO - compile error suppression
        return new CloneResult();
        // HashSet<IObj> dSources;
        // if (recursive) {
        // }
        // TODO build a list of all derivedSources, and any derivedSources' derivedSources, and so on
        // If recursive, then you have a clone family to cross reference as well
        // Any derivedSources that are not currently included get added as a child of parent.
    }
    public static CloneResult Clone(IDerivedDataObjMeta target, bool cloneDerivedSources) {
        if (!cloneDerivedSources) {
            return new CloneResult(target, target.Clone());
        }

        // TODO - compile error suppression
        return new CloneResult();
    }

    // *** Static constructor
    static GlobalRegistrar() {
        UnTaggedModTag = new ModTag(ModTagUntagged);
    }
}
