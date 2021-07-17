using System.Collections.Generic;
using UnityEngine;

public abstract class PipelineExecutableBase : ObjHeader, IPipelineExecutableObj {
    List<PipelineProfile> m_profiles;
    List<IDataObjMeta> m_attachedInputs;
    int m_attachedInputsSize; // allocated size may differ
    List<IDataObjMeta> m_attachedOutputs;
    int m_attachedOutputsSize; // allocated size may differ
    List<IDataObjMeta> m_tmpInputs;
    int m_tmpInputsSize; // allocated size may differ
    List<IDataObjMeta> m_tmpOutputs;
    int m_tmpOutputsSize; // allocated size may differ
    int m_activeProfileIndex;
    PipelineProfile m_activeProfile;
    public bool Enabled {
        get=>m_activeProfileIndex < 0;
        set {
            if (value && m_activeProfileIndex < 0 || !value && m_activeProfileIndex >= 0) {
                int newApi = -1 - m_activeProfileIndex;
                ActiveProfileIndex = newApi; // apply setter
            }
        }
    }
    public int NProfiles { get=>m_profiles.Count; }
    public PipelineProfile GetProfile(int index) { return m_profiles[index]; }
    public PipelineProfile ActiveProfile { get=>m_activeProfile; }
    public int ActiveProfileIndex {
        get => m_activeProfileIndex;
        set {
            #if DEBUG
                if (value > m_profiles.Count-1) {
                    
                    throw new System.IndexOutOfRangeException("Expecting value between -1.." + (m_profiles.Count-1).ToString());
                }
            #endif
            m_activeProfileIndex = value;
            if (m_activeProfileIndex < 0) {
                m_activeProfile = null;
            } else {
                m_activeProfile = m_profiles[m_activeProfileIndex];
                UpdateListSizes();
            }
        }
    }
    public void AttachInput(IDataObjMeta obj, int port) {
        #if DEBUG
            if (port > m_activeProfile.NInputs-1 || port < 0) {
                throw new System.IndexOutOfRangeException("Expecting value between 0.." + (m_activeProfile.NInputs-1).ToString());
            }
        #endif
        m_attachedInputs[port] = obj;
    }
    public void AttachOutput(IDataObjMeta obj, int port) {
        #if DEBUG
            if (port > m_activeProfile.NOutputs-1 || port < 0) {
                throw new System.IndexOutOfRangeException("Expecting value between 0.." + (m_activeProfile.NOutputs-1).ToString());
            }
        #endif
        m_attachedOutputs[port] = obj;
    }
    public void DetachInput(int port) {
        #if DEBUG
            if (port > m_activeProfile.NInputs-1 || port < 0) {
                throw new System.IndexOutOfRangeException("Expecting value between 0.." + (m_activeProfile.NInputs-1).ToString());
            }
        #endif
        m_attachedInputs[port] = null;
    }
    public void DetachOutput(int port) {
        #if DEBUG
            if (port > m_activeProfile.NOutputs-1 || port < 0) {
                throw new System.IndexOutOfRangeException("Expecting value between 0.." + (m_activeProfile.NOutputs-1).ToString());
            }
        #endif
        m_attachedInputs[port] = null;
    }
    public void DetachAllInputs() { for (int i = 0; i < m_attachedInputs.Count; ++i) { m_attachedInputs[i] = null; } }
    public void DetachAllOutputs() { for (int i = 0; i < m_attachedOutputs.Count; ++i) { m_attachedOutputs[i] = null; } }
    public void DetachAllPorts() { DetachAllInputs(); DetachAllOutputs(); }
    public void ExecuteAttached() {
        InternalExecute(m_attachedInputs, m_attachedOutputs);
    }
    public void Execute(IDataObjMeta obj0) {
        #if DEBUG
            int nVars = m_activeProfile.NInputs + m_activeProfile.NOutputs;
            if (nVars != 1) {
                throw new System.ArgumentException("Active PipelineProfile requires " + nVars + " data objects, received 1");
            }
        #endif
        if (m_activeProfile.NInputs > 0) {
            m_tmpInputs[0] = obj0;
        } else {
            m_tmpOutputs[0] = obj0;
        }
        InternalExecute(m_tmpInputs, m_tmpOutputs);
    }
    public void Execute(IDataObjMeta obj0, IDataObjMeta obj1) {
        #if DEBUG
            int nVars = m_activeProfile.NInputs + m_activeProfile.NOutputs;
            if (nVars != 2) {
                throw new System.ArgumentException("Active PipelineProfile requires " + nVars + " data objects, received 2");
            }
        #endif
        switch (m_activeProfile.NInputs) {
            case 0:
                m_tmpOutputs[0] = obj0;
                m_tmpOutputs[1] = obj1;
                break;
            case 1:
                m_tmpInputs[0] = obj0;
                m_tmpOutputs[0] = obj1;
                break;
            default: // case 2
                m_tmpInputs[0] = obj0;
                m_tmpInputs[1] = obj1;
                break;
        }
        InternalExecute(m_tmpInputs, m_tmpOutputs);
    }
    public void Execute(IDataObjMeta obj0, IDataObjMeta obj1, IDataObjMeta obj2) {
        #if DEBUG
            int nVars = m_activeProfile.NInputs + m_activeProfile.NOutputs;
            if (nVars != 3) {
                throw new System.ArgumentException("Active PipelineProfile requires " + nVars + " data objects, received 3");
            }
        #endif
        switch (m_activeProfile.NInputs) {
            case 0:
                m_tmpOutputs[0] = obj0;
                m_tmpOutputs[1] = obj1;
                m_tmpOutputs[2] = obj2;
                break;
            case 1:
                m_tmpInputs[0] = obj0;
                m_tmpOutputs[0] = obj1;
                m_tmpOutputs[1] = obj2;
                break;
            case 2:
                m_tmpInputs[0] = obj0;
                m_tmpInputs[1] = obj1;
                m_tmpOutputs[0] = obj2;
                break;
            default: // case 3
                m_tmpInputs[0] = obj0;
                m_tmpInputs[1] = obj1;
                m_tmpInputs[2] = obj2;
                break;
        }
        InternalExecute(m_tmpInputs, m_tmpOutputs);
    }
    public void Execute(IDataObjMeta obj0, IDataObjMeta obj1, IDataObjMeta obj2, IDataObjMeta obj3) {
        #if DEBUG
            int nVars = m_activeProfile.NInputs + m_activeProfile.NOutputs;
            if (nVars != 4) {
                throw new System.ArgumentException("Active PipelineProfile requires " + nVars + " data objects, received 4");
            }
        #endif
        switch (m_activeProfile.NInputs) {
            case 0:
                m_tmpOutputs[0] = obj0;
                m_tmpOutputs[1] = obj1;
                m_tmpOutputs[2] = obj2;
                m_tmpOutputs[3] = obj3;
                break;
            case 1:
                m_tmpInputs[0] = obj0;
                m_tmpOutputs[0] = obj1;
                m_tmpOutputs[1] = obj2;
                m_tmpOutputs[2] = obj3;
                break;
            case 2:
                m_tmpInputs[0] = obj0;
                m_tmpInputs[1] = obj1;
                m_tmpOutputs[0] = obj2;
                m_tmpOutputs[1] = obj3;
                break;
            case 3:
                m_tmpInputs[0] = obj0;
                m_tmpInputs[1] = obj1;
                m_tmpInputs[2] = obj2;
                m_tmpOutputs[0] = obj3;
                break;
            default: // case 4
                m_tmpInputs[0] = obj0;
                m_tmpInputs[1] = obj1;
                m_tmpInputs[2] = obj2;
                m_tmpInputs[3] = obj3;
                break;
        }
        InternalExecute(m_tmpInputs, m_tmpOutputs);
    }
    public void Execute(IDataObjMeta obj0, IDataObjMeta obj1, IDataObjMeta obj2, IDataObjMeta obj3, IDataObjMeta obj4) {
        #if DEBUG
            int nVars = m_activeProfile.NInputs + m_activeProfile.NOutputs;
            if (nVars != 5) {
                throw new System.ArgumentException("Active PipelineProfile requires " + nVars + " data objects, received 5");
            }
        #endif
        switch (m_activeProfile.NInputs) {
            case 0:
                m_tmpOutputs[0] = obj0;
                m_tmpOutputs[1] = obj1;
                m_tmpOutputs[2] = obj2;
                m_tmpOutputs[3] = obj3;
                m_tmpOutputs[4] = obj4;
                break;
            case 1:
                m_tmpInputs[0] = obj0;
                m_tmpOutputs[0] = obj1;
                m_tmpOutputs[1] = obj2;
                m_tmpOutputs[2] = obj3;
                m_tmpOutputs[3] = obj4;
                break;
            case 2:
                m_tmpInputs[0] = obj0;
                m_tmpInputs[1] = obj1;
                m_tmpOutputs[0] = obj2;
                m_tmpOutputs[1] = obj3;
                m_tmpOutputs[2] = obj4;
                break;
            case 3:
                m_tmpInputs[0] = obj0;
                m_tmpInputs[1] = obj1;
                m_tmpInputs[2] = obj2;
                m_tmpOutputs[0] = obj3;
                m_tmpOutputs[1] = obj4;
                break;
            case 4:
                m_tmpInputs[0] = obj0;
                m_tmpInputs[1] = obj1;
                m_tmpInputs[2] = obj2;
                m_tmpInputs[3] = obj3;
                m_tmpOutputs[0] = obj4;
                break;
            default: // case 5
                m_tmpInputs[0] = obj0;
                m_tmpInputs[1] = obj1;
                m_tmpInputs[2] = obj2;
                m_tmpInputs[3] = obj3;
                m_tmpInputs[4] = obj4;
                break;
        }
        InternalExecute(m_tmpInputs, m_tmpOutputs);
    }
    public void Execute(params IDataObjMeta[] objs) {
        #if DEBUG
            int nVars = m_activeProfile.NInputs + m_activeProfile.NOutputs;
            if (nVars != objs.Length) {
                throw new System.ArgumentException("Active PipelineProfile requires " + nVars + " data objects, received " + objs.Length);
            }
        #endif
        int nInputs = m_activeProfile.NInputs;
        for (int i = 0; i < nInputs; ++i) {
            m_tmpInputs[i] = objs[i];
        }
        for (int i = 0; i < m_activeProfile.NOutputs; ++i) {
            m_tmpOutputs[i] = objs[i + nInputs];
        }
        InternalExecute(m_tmpInputs, m_tmpOutputs);
    }

    public abstract void InternalExecute(List<IDataObjMeta> inputs, List<IDataObjMeta> outputs);
    void UpdateListSizes() {
        for (int i = m_attachedInputsSize; i < m_activeProfile.NInputs; ++i) {
            m_attachedInputs.Add(null);
            m_tmpInputs.Add(null);
        }
        for (int i = m_attachedOutputsSize; i < m_activeProfile.NOutputs; ++i) {
            m_attachedOutputs.Add(null);
            m_tmpOutputs.Add(null);
        }
        m_attachedInputsSize = m_activeProfile.NInputs;
        m_attachedOutputsSize = m_activeProfile.NOutputs;
    }

    PipelineExecutableBase(
        string name,
        IObjRegistry parent = null,
        List<PipelineProfile> profiles = null
    ) : base (name, parent) {
        m_profiles = profiles;
        m_attachedInputs = new List<IDataObjMeta>();
        m_attachedOutputs = new List<IDataObjMeta>();
        m_tmpInputs = new List<IDataObjMeta>();
        m_tmpOutputs = new List<IDataObjMeta>();
        ActiveProfileIndex = 0; // setter triggers UpdateListSizes
    }

    PipelineExecutableBase(PipelineExecutableBase obj) : base(obj) {
        m_profiles = new List<PipelineProfile>(obj.m_profiles.Count);
        foreach (PipelineProfile pp in obj.m_profiles) {
            m_profiles.Add(new PipelineProfile(pp));
        }
    }
    PipelineExecutableBase() : base() {
        m_profiles = new List<PipelineProfile>();
        m_attachedInputs = new List<IDataObjMeta>();
        m_attachedInputsSize = 0;
        m_attachedOutputs = new List<IDataObjMeta>();
        m_attachedOutputsSize = 0;
        m_tmpInputs = new List<IDataObjMeta>();
        m_tmpInputsSize = 0;
        m_tmpOutputs = new List<IDataObjMeta>();
        m_tmpOutputsSize = 0;
        m_activeProfileIndex = -1;
    }
}
