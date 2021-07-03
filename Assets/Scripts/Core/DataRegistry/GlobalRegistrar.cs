#if MULTITHREADING
    using System.Threading;
#endif

public static class GlobalRegistrar {
    // *** ModTag static data
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
    public static void UpdateModTag(ModTag mtag) {
        #if MULTITHREADING
            mtag.Tag = Interlocked.Increment(ref m_currentModTag));
        #else
            mtag.Tag = ++m_currentModTag;
        #endif
    }
    public static long GetNextId() { return ++m_currentId; }
}
