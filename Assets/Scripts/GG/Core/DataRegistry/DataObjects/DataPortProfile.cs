using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataPortProfile {
    string m_name;
    List<string> m_inputNames;
    List<DataTypeEnum> m_inputTypes;
    List<string> m_outputNames;
    List<DataTypeEnum> m_outputTypes;
    public string Name { get=>m_name; set=>m_name=value;}
    public int NInputs { get=>m_inputTypes.Count; }
    public int NOutputs { get=>m_outputTypes.Count; }
    public List<string> InputNames {
        get=>m_inputNames;
        set {
            m_inputNames=value;
            #if DEBUG
                if (GeneralTools.CountDuplicates(m_inputNames) > 0) {
                    Debug.LogWarning("Duplicate names in inputs for profile " + m_name);
                }
            #endif
        }
    }
    public List<DataTypeEnum> InputTypes { get=>m_inputTypes; set=>m_inputTypes=value; }
    public List<string> OutputNames {
        get=>m_outputNames;
        set {
            m_outputNames=value;
            #if DEBUG
                if (GeneralTools.CountDuplicates(m_outputNames) > 0) {
                    Debug.LogWarning("Duplicate names in outputs for profile " + m_name);
                }
            #endif
        }
    }
    public List<DataTypeEnum> OutputTypes { get=>m_outputTypes; set=>m_outputTypes=value; }

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

    public void AddInput(string name, DataTypeEnum type) {
        #if DEBUG
            if (m_inputNames.Contains(name)) {
                Debug.LogWarning("Adding duplicate name '" + name + "' in inputs for profile " + m_name);
            }
        #endif
        m_inputNames.Add(name);
        m_inputTypes.Add(type);
    }
    public void AddOutput(string name, DataTypeEnum type) {
        #if DEBUG
            if (m_outputNames.Contains(name)) {
                Debug.LogWarning("Adding duplicate name '" + name + "' in outputs for profile " + m_name);
            }
        #endif
        m_outputNames.Add(name);
        m_outputTypes.Add(type);
    }
    public void RemoveInput(string name) {
        int removeIndex = m_inputNames.FindIndex(x => x == name);
        m_inputNames.RemoveAt(removeIndex);
        m_inputTypes.RemoveAt(removeIndex);
    }
    public void RemoveOutput(string name) {
        int removeIndex = m_outputNames.FindIndex(x => x == name);
        m_outputNames.RemoveAt(removeIndex);
        m_outputTypes.RemoveAt(removeIndex);
    }

    void InitData() {
        m_inputNames = new List<string>();
        m_inputTypes = new List<DataTypeEnum>();
        m_outputNames = new List<string>();
        m_outputTypes = new List<DataTypeEnum>();
    }

    public DataPortProfile() { InitData(); }
    public DataPortProfile(string name, string inputName, DataTypeEnum inputType, string outputName, DataTypeEnum outputType) {
        InitData();
        m_name = name;
        m_inputNames.Add(inputName);
        m_inputTypes.Add(inputType);
        m_outputNames.Add(outputName);
        m_outputTypes.Add(outputType);
    }
    public DataPortProfile(DataPortProfile pp) {
        m_name = pp.m_name;
        m_inputNames = new List<string>(pp.m_inputNames);
        m_inputTypes = new List<DataTypeEnum>(pp.m_inputTypes);
        m_outputNames = new List<string>(pp.m_outputNames);
        m_outputTypes = new List<DataTypeEnum>(pp.m_outputTypes);
    }
}
