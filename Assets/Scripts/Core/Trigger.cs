/// <summary>
/// A trigger that can be set and reset.  When set, Get returns true, then resets the trigger.  It is 'used'.
/// </summary>
public struct Trigger {
    bool m_triggerSet; // TODO - Check to ensure bool inits false, if they init true, we have to reverse triggerSet
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
    public static implicit  operator bool(Trigger trigger) { return trigger.Get(); }
    public Trigger(bool triggerSet) { m_triggerSet = triggerSet; }
}