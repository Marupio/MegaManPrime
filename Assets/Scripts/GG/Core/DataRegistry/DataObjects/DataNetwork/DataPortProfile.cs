using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// I am a single configuration of inputs and outputs, presumably for some kind of object that takes inputs and returns outputs.
/// I also have a name, in case there are more than one possible configurations of data input / output.
/// </summary>
public class DataPortProfile {
    public static readonly DataPortProfile Null = new DataPortProfile("Null");
    string m_name;
    List<string> m_inputNames;
    List<DataTypeEnum> m_inputTypes;
    List<string> m_outputNames;
    List<DataTypeEnum> m_outputTypes;
    List<object> m_activeWithConnections;

    // *** Access
    public string Name { get=>m_name; set=>m_name=value;}
    public int NInputs { get=>m_inputTypes.Count; }
    public int NOutputs { get=>m_outputTypes.Count; }
    public List<string> InputNames {
        get=>m_inputNames;
        set {
            if (m_activeWithConnections.Count > 0) {
                Debug.LogError("Attempting to modify active DataPortProfile");
                return;
            }
            m_inputNames=value;
            #if DEBUG
                if (GeneralTools.CountDuplicates(m_inputNames) > 0) {
                    Debug.LogWarning("Duplicate names in inputs for profile " + m_name);
                }
            #endif
        }
    }
    public List<DataTypeEnum> InputTypes {
        get=>m_inputTypes;
        set {
            if (m_activeWithConnections.Count > 0) {
                Debug.LogError("Attempting to modify active DataPortProfile");
                return;
            }
            m_inputTypes=value;
        }
    }
    public List<string> OutputNames {
        get=>m_outputNames;
        set {
            if (m_activeWithConnections.Count > 0) {
                Debug.LogError("Attempting to modify active DataPortProfile");
                return;
            }
            m_outputNames=value;
            #if DEBUG
                if (GeneralTools.CountDuplicates(m_outputNames) > 0) {
                    Debug.LogWarning("Duplicate names in outputs for profile " + m_name);
                }
            #endif
        }
    }
    public List<DataTypeEnum> OutputTypes {
        get=>m_outputTypes;
        set {
            if (m_activeWithConnections.Count > 0) {
                Debug.LogError("Attempting to modify active DataPortProfile");
                return;
            }
            m_outputTypes=value;
        }
    }
    List<object> ActiveWithConnections { get=>m_activeWithConnections; }

    // *** Query
    public int GetInputPortFromName(string name) { // returns -1 if not found
        return m_inputNames.FindIndex(x => x == name);
    }
    public int GetOutputPortFromName(string name) { // returns -1 if not found
        return m_outputNames.FindIndex(x => x == name);
    }
    public int GetPortFromName(string name, out bool portIsAnInput) { // returns -1 if not found
        int index = m_inputNames.FindIndex(x => x == name);
        if (index >= 0) {
            portIsAnInput = true;
            return index;
        }
        portIsAnInput = false;
        return m_outputNames.FindIndex(x => x == name);
    }

    // *** Edit
    public void AddInput(string name, DataTypeEnum type) {
        if (m_activeWithConnections.Count > 0) {
            Debug.LogError("Attempting to modify active DataPortProfile");
            return;
        }
        #if DEBUG
            if (m_inputNames.Contains(name)) {
                Debug.LogWarning("Adding duplicate name '" + name + "' in inputs for profile " + m_name);
            }
        #endif
        m_inputNames.Add(name);
        m_inputTypes.Add(type);
    }
    public void AddOutput(string name, DataTypeEnum type) {
        if (m_activeWithConnections.Count > 0) {
            Debug.LogError("Attempting to modify active DataPortProfile");
            return;
        }
        #if DEBUG
            if (m_outputNames.Contains(name)) {
                Debug.LogWarning("Adding duplicate name '" + name + "' in outputs for profile " + m_name);
            }
        #endif
        m_outputNames.Add(name);
        m_outputTypes.Add(type);
    }
    public void RemoveInput(string name) {
        if (m_activeWithConnections.Count > 0) {
            Debug.LogError("Attempting to modify active DataPortProfile");
            return;
        }
        int removeIndex = m_inputNames.FindIndex(x => x == name);
        m_inputNames.RemoveAt(removeIndex);
        m_inputTypes.RemoveAt(removeIndex);
    }
    public void RemoveOutput(string name) {
        int removeIndex = m_outputNames.FindIndex(x => x == name);
        m_outputNames.RemoveAt(removeIndex);
        m_outputTypes.RemoveAt(removeIndex);
    }
    public void ActiveWith(object adp) {
        if (m_activeWithConnections.Contains(adp)) {
            Debug.LogWarning("Attempting to add duplicate ActiveDataPortConnection to DataPortProfile " + m_name);
            return;
        }
        m_activeWithConnections.Add(adp);
    }
    public void NotActiveWith(object adp) {
        int index = m_activeWithConnections.IndexOf(adp);
        if (index < 0) {
            Debug.LogError("Attempting to remove missing ActiveDataPortConnection from DataPortProfile " + m_name);
            return;
        }
        m_activeWithConnections.RemoveAt(index);
    }

    // *** Internal methods
    void InitData() {
        m_inputNames = new List<string>();
        m_inputTypes = new List<DataTypeEnum>();
        m_outputNames = new List<string>();
        m_outputTypes = new List<DataTypeEnum>();
        m_activeWithConnections = new List<object>();
    }

    public DataPortProfile() { InitData(); }
    public DataPortProfile(string name) { m_name = name; InitData(); }
    public DataPortProfile(
        string name,
        string inputName,
        DataTypeEnum inputType,
        string outputName,
        DataTypeEnum outputType,
        object activateWithConnections = null
    ) {
        InitData();
        m_name = name;
        m_inputNames.Add(inputName);
        m_inputTypes.Add(inputType);
        m_outputNames.Add(outputName);
        m_outputTypes.Add(outputType);
        if (activateWithConnections != null) {
            m_activeWithConnections.Add(activateWithConnections);
        }
    }
    public DataPortProfile(DataPortProfile pp, object activateWithConnections = null) {
        m_name = pp.m_name;
        m_inputNames = new List<string>(pp.m_inputNames);
        m_inputTypes = new List<DataTypeEnum>(pp.m_inputTypes);
        m_outputNames = new List<string>(pp.m_outputNames);
        m_outputTypes = new List<DataTypeEnum>(pp.m_outputTypes);
        if (activateWithConnections != null) {
            m_activeWithConnections.Add(activateWithConnections);
        }
    }
}
