using System;
using UnityEngine;

// public interface IActiveDataPortConnections {
//     // Connect inputs / outputs
//     //  * Base is IDataObjMeta
//     // Derived can apply restrictions to the connections, e.g.
//     //  * Inputs can only be ISourceDataObjMeta  < type constraint
//     //  * Outputs can only be IDerivedDataObjMeta
// }
// TODO - Update these comments - it is dated

/// <summary>
/// I contain the data objects that are connected to the active DataPortProfile
/// </summary>
public class ActiveDataPortConnections<I, O> where I : DataObjList where O : DataObjList
{
    protected I m_inputs;
    protected O m_outputs;
    protected DataPortProfile m_profile;

    // *** Access
    public I Inputs { get=>m_inputs; }
    public O Outputs { get=>m_outputs; }
    public DataPortProfile Profile { get=> m_profile; }

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
    public virtual void ChangeProfileAndDetachAll(DataPortProfile profile) {
        // m_profile may be null on construction only
        if (m_profile != null) m_profile.NotActiveWith(this);
        m_profile = profile;
        m_profile.ActiveWith(this);
        m_inputs.ResizeAndFill(profile.NInputs, null);
        m_outputs.ResizeAndFill(profile.NOutputs, null);
    }

    // *** Edit - Attach inputs
    public void AttachInput(IDataObjMeta obj, int port=0) {
        #if DEBUG
            if (port > m_inputs.Count || port < 0) {
                Debug.LogException(
                    new System.ArgumentOutOfRangeException(
                        "Port " + port + " out of range [0.." + m_inputs.Count + "] for profile " + m_profile.Name
                    )
                );
                return;
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
                Debug.LogException(
                    new System.ArgumentOutOfRangeException(inputName + " is not a valid inputName for profile " + m_profile.Name)
                );
                return;
            }
        #endif
        m_inputs[port] = obj;
    }
    public void AttachAllInputs(DataObjList objs) {
        InternalAttachAllXputs(objs, m_inputs);
    }

    // *** Edit - Attach outputs
    public virtual void AttachOutput(IDataObjMeta obj, int port=0) {
        #if DEBUG
            if (port > m_outputs.Count || port < 0) {
                Debug.LogException(new System.ArgumentOutOfRangeException("Port " + port + " out of range [0.." + m_outputs.Count + "]"));
                return;
            }
            if (m_outputs[port] != null) {
                Debug.LogWarning("Overwriting existing port connection " + port);
            }
        #endif
        m_outputs[port] = obj;
    }
    public virtual void AttachOutput(IDataObjMeta obj, string outputName) {
        int port = m_profile.GetOutputPortFromName(outputName);
        #if DEBUG
            if (port < 0) {
                Debug.LogException(new System.ArgumentOutOfRangeException(outputName + " is not a valid outputName for profile " + m_profile.Name));
                return;
            }
        #endif
        m_outputs[port] = obj;
    }
    public virtual void AttachAllOutputs(DataObjList objs) {
        InternalAttachAllXputs(objs, m_outputs);
    }

    // *** Edit - Attach all
    public virtual void AttachAll(IDataObjMeta obj0) {
        #if DEBUG
            int nPorts = m_inputs.Count + m_outputs.Count;
            if (nPorts != 1) {
                Debug.LogException(new System.MissingFieldException("1 connection given for " + nPorts + " ports for profile " + m_profile.Name));
                return;
            }
        #endif
        if (m_inputs.Count > 0) {
            m_inputs.SetAt(0, obj0);
        } else {
            m_outputs.SetAt(0, obj0);
        }
    }
    public virtual void AttachAll(IDataObjMeta obj0, IDataObjMeta obj1) {
        #if DEBUG
            int nPorts = m_inputs.Count + m_outputs.Count;
            if (nPorts != 2) {
                Debug.LogException(new System.MissingFieldException("2 connections given for " + nPorts + " ports for profile " + m_profile.Name));
                return;
            }
        #endif
        switch (m_inputs.Count) {
            case 0:
                m_outputs.SetAt(0, obj0);
                m_outputs.SetAt(1, obj1);
                return;
            case 1:
                m_inputs.SetAt(0, obj0);
                m_outputs.SetAt(0, obj1);
                return;
            default:
                m_inputs.SetAt(0, obj0);
                m_inputs.SetAt(1, obj1);
                return;
        }
    }
    public virtual void AttachAll(IDataObjMeta obj0, IDataObjMeta obj1, IDataObjMeta obj2) {
        #if DEBUG
            int nPorts = m_inputs.Count + m_outputs.Count;
            if (nPorts != 3) {
                Debug.LogException(new System.MissingFieldException("3 connections given for " + nPorts + " ports for profile " + m_profile.Name));
                return;
            }
        #endif
        switch (m_inputs.Count) {
            case 0:
                m_outputs.SetAt(0, obj0);
                m_outputs.SetAt(1, obj1);
                m_outputs.SetAt(2, obj2);
                return;
            case 1:
                m_inputs.SetAt(0, obj0);
                m_outputs.SetAt(0, obj1);
                m_outputs.SetAt(1, obj2);
                return;
            case 2:
                m_inputs.SetAt(0, obj0);
                m_inputs.SetAt(1, obj1);
                m_outputs.SetAt(0, obj2);
                return;
            default:
                m_inputs.SetAt(0, obj0);
                m_inputs.SetAt(1, obj1);
                m_inputs.SetAt(2, obj2);
                return;
        }
    }
    public virtual void AttachAll(IDataObjMeta obj0, IDataObjMeta obj1, IDataObjMeta obj2, IDataObjMeta obj3) {
        #if DEBUG
            int nPorts = m_inputs.Count + m_outputs.Count;
            if (nPorts != 4) {
                Debug.LogException(new System.MissingFieldException("4 connections given for " + nPorts + " ports for profile " + m_profile.Name));
                return;
            }
        #endif
        switch (m_inputs.Count) {
            case 0:
                m_outputs.SetAt(0, obj0);
                m_outputs.SetAt(1, obj1);
                m_outputs.SetAt(2, obj2);
                m_outputs.SetAt(3, obj3);
                return;
            case 1:
                m_inputs.SetAt(0, obj0);
                m_outputs.SetAt(0, obj1);
                m_outputs.SetAt(1, obj2);
                m_outputs.SetAt(2, obj3);
                return;
            case 2:
                m_inputs.SetAt(0, obj0);
                m_inputs.SetAt(1, obj1);
                m_outputs.SetAt(0, obj2);
                m_outputs.SetAt(1, obj3);
                return;
            case 3:
                m_inputs.SetAt(0, obj0);
                m_inputs.SetAt(1, obj1);
                m_inputs.SetAt(2, obj2);
                m_outputs.SetAt(0, obj3);
                return;
            default:
                m_inputs.SetAt(0, obj0);
                m_inputs.SetAt(1, obj1);
                m_inputs.SetAt(2, obj2);
                m_inputs.SetAt(3, obj3);
                return;
        }
    }
    public virtual void AttachAll(IDataObjMeta obj0, IDataObjMeta obj1, IDataObjMeta obj2, IDataObjMeta obj3, IDataObjMeta obj4) {
        #if DEBUG
            int nPorts = m_inputs.Count + m_outputs.Count;
            if (nPorts != 5) {
                Debug.LogException(new System.MissingFieldException("5 connections given for " + nPorts + " ports for profile " + m_profile.Name));
                return;
            }
        #endif
        switch (m_inputs.Count) {
            case 0:
                m_outputs.SetAt(0, obj0);
                m_outputs.SetAt(1, obj1);
                m_outputs.SetAt(2, obj2);
                m_outputs.SetAt(3, obj3);
                m_outputs.SetAt(4, obj4);
                return;
            case 1:
                m_inputs.SetAt(0, obj0);
                m_outputs.SetAt(0, obj1);
                m_outputs.SetAt(1, obj2);
                m_outputs.SetAt(2, obj3);
                m_outputs.SetAt(3, obj4);
                return;
            case 2:
                m_inputs.SetAt(0, obj0);
                m_inputs.SetAt(1, obj1);
                m_outputs.SetAt(0, obj2);
                m_outputs.SetAt(1, obj3);
                m_outputs.SetAt(2, obj4);
                return;
            case 3:
                m_inputs.SetAt(0, obj0);
                m_inputs.SetAt(1, obj1);
                m_inputs.SetAt(2, obj2);
                m_outputs.SetAt(0, obj3);
                m_outputs.SetAt(1, obj4);
                return;
            case 4:
                m_inputs.SetAt(0, obj0);
                m_inputs.SetAt(1, obj1);
                m_inputs.SetAt(2, obj2);
                m_inputs.SetAt(3, obj3);
                m_outputs.SetAt(0, obj4);
                return;
            default:
                m_inputs.SetAt(0, obj0);
                m_inputs.SetAt(1, obj1);
                m_inputs.SetAt(2, obj2);
                m_inputs.SetAt(3, obj3);
                m_inputs.SetAt(4, obj4);
                return;
        }
    }
    public virtual void AttachAll(params IDataObjMeta[] objs) {
        #if DEBUG
            int nPorts = m_inputs.Count + m_outputs.Count;
            if (nPorts != objs.Length) {
                Debug.LogException(new System.MissingFieldException(objs.Length + " connections given for " + nPorts + " ports for profile " + m_profile.Name));
                return;
            }
        #endif
        for (int i = 0; i < m_inputs.Count; ++i) {
            m_inputs.SetAt(i, objs[i]);
        }
        for (int i = 0; i < m_outputs.Count; ++i) {
            m_outputs.SetAt(i, objs[i + m_inputs.Count]);
        }
    }

    // *** Edit - Detach inputs
    public void DetachInput(int port) {
        #if DEBUG
            if (port < 0 || port >= m_inputs.Count) {
                Debug.LogException(new System.ArgumentOutOfRangeException("Port " + port + " out of range [0.." + m_inputs.Count + "]"));
                return;
            }
        #endif
        m_inputs[port] = null;
    }
    public void DetachInput(string inputName) {
        int port = m_profile.GetInputPortFromName(inputName);
        #if DEBUG
            if (port < 0) {
                Debug.LogException(new System.ArgumentOutOfRangeException(inputName + " is not a valid inputName for profile " + m_profile.Name));
                return;
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
                Debug.LogException(new System.ArgumentOutOfRangeException("Port " + port + " out of range [0.." + m_outputs.Count + "]"));
                return;
            }
        #endif
        m_outputs[port] = null;
    }
    public void DetachOutput(string outputName) {
        int port = m_profile.GetOutputPortFromName(outputName);
        #if DEBUG
            if (port < 0) {
                Debug.LogException(new System.ArgumentOutOfRangeException(outputName + " is not a valid outputName for profile " + m_profile.Name));
                return;
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
    void InternalAttachAllXputs(DataObjList objs, DataObjList xputs) {
        #if DEBUG
            if (objs.Count != xputs.Count) {
                Debug.LogException(new System.ArgumentOutOfRangeException("Cannot attach " + objs.Count + " connections to " + xputs.Count + " ports."));
                return;
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
            xputs.SetAt(i, objs[i]);
        }
    }
    void Init() {
        m_inputs = Activator.CreateInstance<I>();
        m_outputs = Activator.CreateInstance<O>();
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

// public class ActiveDerivedDataPortConnections : ActiveDataPortConnections {}
