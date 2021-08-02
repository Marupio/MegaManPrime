using System.Collections.Generic;

public class CloneResult {
    /// <summary>
    /// All new objects resulting from cloning process
    /// For each obj that could not be cloned, the old one will sit in its place
    /// The first object will always be the initial trigger object
    /// </summary>
    List<IObj> m_newObjs;
    /// <summary>
    /// All old objects that were involved in the clone
    /// </summary>
    List<IObj> m_oldObjs;
    /// <summary>
    /// Mapping between old and new object Ids
    /// If object could not be cloned, old and new will be the same
    /// </summary>
    Dictionary<long, long> m_oldIdToNewId;
    Dictionary<long, long> m_newIdToOldId;

    public List<IObj> NewObjs { get=>m_newObjs; set=>m_newObjs=value; }
    public List<IObj> OldObjs { get=>m_oldObjs; set=>m_oldObjs=value; }
    Dictionary<long, long> OldIdToNewId { get=>m_oldIdToNewId; set=>m_oldIdToNewId=value; }
    Dictionary<long, long> NewIdToOldId { get=>m_newIdToOldId; set=>m_newIdToOldId=value; }

    public void Add(IObj newObj, IObj oldObj) {
        AddNewObj(newObj);
        AddOldObj(oldObj);
        AddMapping(oldObj, newObj);
    }
    public void AddRange(List<IObj> newObjs, List<IObj> oldObjs) {
        AddRangeNewObj(newObjs);
        AddRangeOldObj(oldObjs);
        AddMappingRange(oldObjs, newObjs);
    }
    public void Add(CloneResult cr) {
        AddRange(cr.m_oldObjs, cr.m_newObjs);
    }

    private void AddNewObj(IObj obj) { m_newObjs.Add(obj); }
    private void AddRangeNewObj(List<IObj> objs) { m_newObjs.AddRange(objs); }
    private void AddOldObj(IObj obj) { m_oldObjs.Add(obj); }
    private void AddRangeOldObj(List<IObj> objs) { m_oldObjs.AddRange(objs); }
    private void AddMapping(IObj oldId, IObj newId) {
        m_oldIdToNewId.Add(oldId.Id, newId.Id);
        m_newIdToOldId.Add(newId.Id, oldId.Id);
    }
    private void AddMappingRange(List<IObj> oldIds, List<IObj> newIds) {
        for (int i = 0; i < oldIds.Count; ++i) {
            m_oldIdToNewId.Add(oldIds[i].Id, newIds[i].Id);
            m_newIdToOldId.Add(newIds[i].Id, oldIds[i].Id);
        }
    }

    // *** Constructors
    public CloneResult(List<IObj> oldObjs, List<IObj> newObjs) {
        m_oldObjs = new List<IObj>();
        m_newObjs = new List<IObj>();
        m_newIdToOldId = new Dictionary<long, long>();
        m_oldIdToNewId = new Dictionary<long, long>();
        AddRange(oldObjs, newObjs);
    }
    public CloneResult(IObj oldObj, IObj newObj) {
        m_oldObjs = new List<IObj>();
        m_newObjs = new List<IObj>();
        m_newIdToOldId = new Dictionary<long, long>();
        m_oldIdToNewId = new Dictionary<long, long>();
        Add(oldObj, newObj);
    }
    public CloneResult() {
        m_oldObjs = new List<IObj>();
        m_newObjs = new List<IObj>();
        m_newIdToOldId = new Dictionary<long, long>();
        m_oldIdToNewId = new Dictionary<long, long>();
    }
}
