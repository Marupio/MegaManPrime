/// <summary>
/// A trigger that can be set and reset.  When set, Get returns true, then resets the trigger.  It is 'used'.
/// </summary>
public struct Trigger {
    // Default value is false, default Trigger initializes unset, correct desired behaviour
    bool m_triggerSet;
    public bool Peek() { return m_triggerSet; }
    public bool Get() {
        if (m_triggerSet) {
            Reset();
            return true;
        }
        return false;
    }
    public void Set() { m_triggerSet = true; }
    public void Reset() { m_triggerSet = false; }
    public Trigger(bool triggerSet) { m_triggerSet = triggerSet; }
    public static implicit operator bool(Trigger trigger) { return trigger.Get(); }
    public static Trigger Default { get=>new Trigger(); }
    public static Trigger DefaultArmed { get=>new Trigger(true); }
}