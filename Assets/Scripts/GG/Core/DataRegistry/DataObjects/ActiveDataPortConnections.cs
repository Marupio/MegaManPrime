using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// I contain the data objects that are connected to the active DataPortProfile
/// </summary>
public class ActiveDataPortConnections {
    List<IDataObjMeta> m_inputs;
    List<IDataObjMeta> m_outputs;
    DataPortProfile m_profile;

    // *** Access
    public List<IDataObjMeta> Inputs { get=>m_inputs; }
    public List<IDataObjMeta> Outputs { get=>m_outputs; }

    // *** Query
    public bool Ready {
        get {
            if (m_profile == DataPortProfile.Null) return false;
            if (m_inputs.Contains(null)) return false;
            if (m_outputs.Contains(null)) return false;
            return true;
        }
    }

    // *** Edit - Switch profiles
    public void ChangeProfileAndDetachAll(DataPortProfile profile) {
        // m_profile may be null on construction only
        if (m_profile != null) m_profile.NotActiveWith(this);
        m_profile = profile;
        m_profile.ActiveWith(this);
        InternalChangeProfileAndDetachAll(profile.NInputs, ref m_inputs);
        InternalChangeProfileAndDetachAll(profile.NOutputs, ref m_outputs);
    }

    // *** Edit - Attach inputs
    public void AttachInput(IDataObjMeta obj, int port=0) {
        #if DEBUG
            if (port > m_inputs.Count || port < 0) {
                throw new System.ArgumentOutOfRangeException(
                    "Port " + port + " out of range [0.." + m_inputs.Count + "] for profile " + m_profile.Name
                );
            }
            if (m_inputs[port] != null) {
                Debug.LogWarning("Overwriting existing port connection " + port + " for profile " + m_profile.Name);
            }
        #endif
        m_inputs[port] = obj;
    }
    public void AttachInput(IDataObjMeta obj, string inputName) {
        int port = m_profile.GetInputPortFromName(inputName);
        #if DEBUG
            if (port < 0) {
                throw new System.ArgumentOutOfRangeException(inputName + " is not a valid inputName for profile " + m_profile.Name);
            }
        #endif
        m_inputs[port] = obj;
    }
    public void AttachAllInputs(List<IDataObjMeta> objs) {
        InternalAttachAllXputs(objs, ref m_inputs);
    }

    // *** Edit - Attach outputs
    public void AttachOutput(IDataObjMeta obj, int port=0) {
        #if DEBUG
            if (port > m_outputs.Count || port < 0) {
                throw new System.ArgumentOutOfRangeException("Port " + port + " out of range [0.." + m_outputs.Count + "]");
            }
            if (m_outputs[port] != null) {
                Debug.LogWarning("Overwriting existing port connection " + port);
            }
        #endif
        m_outputs[port] = obj;
    }
    public void AttachOutput(IDataObjMeta obj, string outputName) {
        int port = m_profile.GetOutputPortFromName(outputName);
        #if DEBUG
            if (port < 0) {
                throw new System.ArgumentOutOfRangeException(outputName + " is not a valid outputName for profile " + m_profile.Name);
            }
        #endif
        m_outputs[port] = obj;
    }
    public void AttachAllOutputs(List<IDataObjMeta> objs) {
        InternalAttachAllXputs(objs, ref m_outputs);
    }

    // *** Edit - Attach all
    public void AttachAll(IDataObjMeta obj0) {
        #if DEBUG
            int nPorts = m_inputs.Count + m_outputs.Count;
            if (nPorts != 1) {
                throw new System.MissingFieldException("1 connection given for " + nPorts + " ports for profile " + m_profile.Name);
            }
        #endif
        if (m_inputs.Count > 0) {
            m_inputs[0] = obj0;
        } else {
            m_outputs[0] = obj0;
        }
    }
    public void AttachAll(IDataObjMeta obj0, IDataObjMeta obj1) {
        #if DEBUG
            int nPorts = m_inputs.Count + m_outputs.Count;
            if (nPorts != 2) {
                throw new System.MissingFieldException("2 connections given for " + nPorts + " ports for profile " + m_profile.Name);
            }
        #endif
        switch (m_inputs.Count) {
            case 0:
                m_outputs[0] = obj0;
                m_outputs[1] = obj1;
                return;
            case 1:
                m_inputs[0] = obj0;
                m_outputs[0] = obj1;
                return;
            default:
                m_inputs[0] = obj0;
                m_inputs[1] = obj1;
                return;
        }
    }
    public void AttachAll(IDataObjMeta obj0, IDataObjMeta obj1, IDataObjMeta obj2) {
        #if DEBUG
            int nPorts = m_inputs.Count + m_outputs.Count;
            if (nPorts != 3) {
                throw new System.MissingFieldException("3 connections given for " + nPorts + " ports for profile " + m_profile.Name);
            }
        #endif
        switch (m_inputs.Count) {
            case 0:
                m_outputs[0] = obj0;
                m_outputs[1] = obj1;
                m_outputs[2] = obj2;
                return;
            case 1:
                m_inputs[0] = obj0;
                m_outputs[0] = obj1;
                m_outputs[1] = obj2;
                return;
            case 2:
                m_inputs[0] = obj0;
                m_inputs[1] = obj1;
                m_outputs[0] = obj2;
                return;
            default:
                m_inputs[0] = obj0;
                m_inputs[1] = obj1;
                m_inputs[2] = obj2;
                return;
        }
    }
    public void AttachAll(IDataObjMeta obj0, IDataObjMeta obj1, IDataObjMeta obj2, IDataObjMeta obj3) {
        #if DEBUG
            int nPorts = m_inputs.Count + m_outputs.Count;
            if (nPorts != 4) {
                throw new System.MissingFieldException("4 connections given for " + nPorts + " ports for profile " + m_profile.Name);
            }
        #endif
        switch (m_inputs.Count) {
            case 0:
                m_outputs[0] = obj0;
                m_outputs[1] = obj1;
                m_outputs[2] = obj2;
                m_outputs[3] = obj3;
                return;
            case 1:
                m_inputs[0] = obj0;
                m_outputs[0] = obj1;
                m_outputs[1] = obj2;
                m_outputs[2] = obj3;
                return;
            case 2:
                m_inputs[0] = obj0;
                m_inputs[1] = obj1;
                m_outputs[0] = obj2;
                m_outputs[1] = obj3;
                return;
            case 3:
                m_inputs[0] = obj0;
                m_inputs[1] = obj1;
                m_inputs[2] = obj2;
                m_outputs[0] = obj3;
                return;
            default:
                m_inputs[0] = obj0;
                m_inputs[1] = obj1;
                m_inputs[2] = obj2;
                m_inputs[3] = obj3;
                return;
        }
    }
    public void AttachAll(IDataObjMeta obj0, IDataObjMeta obj1, IDataObjMeta obj2, IDataObjMeta obj3, IDataObjMeta obj4) {
        #if DEBUG
            int nPorts = m_inputs.Count + m_outputs.Count;
            if (nPorts != 5) {
                throw new System.MissingFieldException("5 connections given for " + nPorts + " ports for profile " + m_profile.Name);
            }
        #endif
        switch (m_inputs.Count) {
            case 0:
                m_outputs[0] = obj0;
                m_outputs[1] = obj1;
                m_outputs[2] = obj2;
                m_outputs[3] = obj3;
                m_outputs[4] = obj4;
                return;
            case 1:
                m_inputs[0] = obj0;
                m_outputs[0] = obj1;
                m_outputs[1] = obj2;
                m_outputs[2] = obj3;
                m_outputs[3] = obj4;
                return;
            case 2:
                m_inputs[0] = obj0;
                m_inputs[1] = obj1;
                m_outputs[0] = obj2;
                m_outputs[1] = obj3;
                m_outputs[2] = obj4;
                return;
            case 3:
                m_inputs[0] = obj0;
                m_inputs[1] = obj1;
                m_inputs[2] = obj2;
                m_outputs[0] = obj3;
                m_outputs[1] = obj4;
                return;
            case 4:
                m_inputs[0] = obj0;
                m_inputs[1] = obj1;
                m_inputs[2] = obj2;
                m_outputs[0] = obj3;
                m_outputs[1] = obj4;
                return;
            default:
                m_inputs[0] = obj0;
                m_inputs[1] = obj1;
                m_inputs[2] = obj2;
                m_inputs[3] = obj3;
                m_inputs[4] = obj4;
                return;
        }
    }
    public void AttachAll(params IDataObjMeta[] objs) {
        #if DEBUG
            int nPorts = m_inputs.Count + m_outputs.Count;
            if (nPorts != objs.Length) {
                throw new System.MissingFieldException(objs.Length + " connections given for " + nPorts + " ports for profile " + m_profile.Name);
            }
        #endif
        for (int i = 0; i < m_inputs.Count; ++i) {
            m_inputs[i] = objs[i];
        }
        for (int i = 0; i < m_outputs.Count; ++i) {
            m_outputs[i] = objs[i + m_inputs.Count];
        }
    }

    // *** Edit - Detach inputs
    public void DetachInput(int port) {
        #if DEBUG
            if (port < 0 || port >= m_inputs.Count) {
                throw new System.ArgumentOutOfRangeException("Port " + port + " out of range [0.." + m_inputs.Count + "]");
            }
        #endif
        m_inputs[port] = null;
    }
    public void DetachInput(string inputName) {
        int port = m_profile.GetInputPortFromName(inputName);
        #if DEBUG
            if (port < 0) {
                throw new System.ArgumentOutOfRangeException(inputName + " is not a valid inputName for profile " + m_profile.Name);
            }
        #endif
        m_inputs[port] = null;
    }
    public void DetachAllInputs() {
        for (int i = 0; i < m_inputs.Count; ++i) {
            m_inputs[i] = null;
        }
    }

    // *** Edit - Detach outputs
    public void DetachOutput(int port) {
        #if DEBUG
            if (port < 0 || port >= m_outputs.Count) {
                throw new System.ArgumentOutOfRangeException("Port " + port + " out of range [0.." + m_outputs.Count + "]");
            }
        #endif
        m_outputs[port] = null;
    }
    public void DetachOutput(string outputName) {
        int port = m_profile.GetOutputPortFromName(outputName);
        #if DEBUG
            if (port < 0) {
                throw new System.ArgumentOutOfRangeException(outputName + " is not a valid outputName for profile " + m_profile.Name);
            }
        #endif
        m_outputs[port] = null;
    }
    public void DetachAllOutputs() {
        for (int i = 0; i < m_outputs.Count; ++i) {
            m_outputs[i] = null;
        }
    }

    // *** Edit - Detach all
    public void DetachAll() {
        DetachAllInputs();
        DetachAllOutputs();
    }

    // *** Internal methods
    void InternalChangeProfileAndDetachAll(int size, ref List<IDataObjMeta> xputs) {
        if (size < xputs.Count) {
            for (int i = 0; i < size; ++i) {
                xputs[i] = null;
            }
            xputs.RemoveRange(size, xputs.Count - size);
            return;
        }
        if (size == xputs.Count) {
            for (int i = 0; i < size; ++i) {
                xputs[i] = null;
            }
            return;
        }
        // size > count
        for (int i = 0; i < xputs.Count; ++i) {
            xputs[i] = null;
        }
        int nDiff = size - xputs.Count;
        for (int i = 0; i < nDiff; ++i) {
            xputs.Add(null);
        }
        return;
    }
    void InternalAttachAllXputs(List<IDataObjMeta> objs, ref List<IDataObjMeta> xputs) {
        #if DEBUG
            if (objs.Count != xputs.Count) {
                throw new System.ArgumentOutOfRangeException("Cannot attach " + objs.Count + " inputs to " + xputs.Count + " input ports.");
            }
            int nOverwrites = 0;
            foreach (IDataObjMeta obj in xputs) {
                if (obj != null) {
                    ++nOverwrites;
                }
            }
            if (nOverwrites > 0) {
                Debug.LogWarning("Ovewriting " + nOverwrites + " existing port connections");
            }
        #endif
        for (int i = 0; i < xputs.Count; ++i) {
            xputs[i] = objs[i];
        }
    }
    void Init() {
        m_inputs = new List<IDataObjMeta>();
        m_outputs = new List<IDataObjMeta>();
    }

    // *** Constructors
    public ActiveDataPortConnections(DataPortProfile profile) {
        Init();
        ChangeProfileAndDetachAll(profile);
    }
    public ActiveDataPortConnections(DataPortProfile profile, IDataObjMeta obj0) {
        Init();
        ChangeProfileAndDetachAll(profile);
        AttachAll(obj0);
    }
    public ActiveDataPortConnections(DataPortProfile profile, IDataObjMeta obj0, IDataObjMeta obj1) {
        Init();
        ChangeProfileAndDetachAll(profile);
        AttachAll(obj0, obj1);
    }
    public ActiveDataPortConnections(DataPortProfile profile, IDataObjMeta obj0, IDataObjMeta obj1, IDataObjMeta obj2) {
        Init();
        ChangeProfileAndDetachAll(profile);
        AttachAll(obj0, obj1, obj2);
    }
    public ActiveDataPortConnections(DataPortProfile profile, IDataObjMeta obj0, IDataObjMeta obj1, IDataObjMeta obj2, IDataObjMeta obj3) {
        Init();
        ChangeProfileAndDetachAll(profile);
        AttachAll(obj0, obj1, obj2, obj3);
    }
    public ActiveDataPortConnections(DataPortProfile profile, IDataObjMeta obj0, IDataObjMeta obj1, IDataObjMeta obj2, IDataObjMeta obj3, IDataObjMeta obj4) {
        Init();
        ChangeProfileAndDetachAll(profile);
        AttachAll(obj0, obj1, obj2, obj3, obj4);
    }
    public ActiveDataPortConnections(DataPortProfile profile, params IDataObjMeta[] objs) {
        Init();
        ChangeProfileAndDetachAll(profile);
        AttachAll(objs);
    }

    // *** Destructor
    ~ActiveDataPortConnections() {
        if (m_profile != null) {
            m_profile.NotActiveWith(this);
        }
    }
}