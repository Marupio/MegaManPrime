using System.Collections.Generic;

// public class GeneralObjUpdater : IObjUpdater {
//     protected List<ISourceDataObjMeta> m_inputs;
//     protected List<IDerivedDataObjMeta> m_outputs;

//     void PerformUpdatesFor<L>(IDerivedDataObj<L> target);
//     void PerformAllUpdates();
//     void InitDependsOnFor<L>(IDerivedDataObj<L> target, out List<ISourceDataObjMeta> dependsOn);
//     void InitAllDependsOn();

//     // protected Vector2 GetVector2(string name);
// }

public class ObjUpdaterBase : ObjHeader, IObjUpdater {
    // Reverse lookup
    protected Dictionary<IDerivedDataObjMeta, int> m_index;

    // Indexed data
    protected List<IDerivedDataObjMeta> m_outputs;
    protected List<List<ISourceDataObjMeta>> m_inputs;
    // Indexed settings
    protected List<DataTypeEnum> m_outputTypes;
    protected List<string> m_outputNames;
    protected List<List<DataTypeEnum>> m_inputTypes;
    protected List<bool> m_derivedVarInitComplete;

    public List<IDerivedDataObjMeta> AllDerivedData { get=>m_outputs; }
    public bool PerformUpdatesFor(IDerivedDataObjMeta target);
    public void PerformAllUpdates();
    public bool InitDependsOnFor(IDerivedDataObjMeta target, out List<ISourceDataObjMeta> dependsOn) {
        int index;
        if (m_index.TryGetValue(target, out index)) {
            dependsOn = m_inputs[index];
            m_derivedVarInitComplete[index] = true;
            return true;
        }
        dependsOn = null;
        return false;
    }
    public void InitAllDependsOn() {
        for (int index = 0; index < m_inputs.Count; ++index) {
            IDerivedDataObjMeta obj = m_outputs[index];
            obj.Updater = this;
            obj.DependsOn = m_inputs[index];
        }
    }
    public bool SpawnAllDerived();
}
