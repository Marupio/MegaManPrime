using System.Collections.Generic;

public class PipelineProfile {
    List<string> m_inputNames;
    List<DataTypeEnum> m_inputTypes;
    List<string> m_outputNames;
    List<DataTypeEnum> m_outputTypes;
    public int NInputs { get=>m_inputTypes.Count; }
    public int NOutputs { get=>m_outputTypes.Count; }
    public List<string> InputNames { get=>m_inputNames; set=>m_inputNames=value; }
    public List<DataTypeEnum> InputTypes { get=>m_inputTypes; set=>m_inputTypes=value; }
    public List<string> OutputNames { get=>m_outputNames; set=>m_outputNames=value; }
    public List<DataTypeEnum> OutputTypes { get=>m_outputTypes; set=>m_outputTypes=value; }

    public void AddInput(string name, DataTypeEnum type) {
        m_inputNames.Add(name);
        m_inputTypes.Add(type);
    }
    public void AddOutput(string name, DataTypeEnum type) {
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

    public PipelineProfile() { InitData(); }
    public PipelineProfile(string inputName, DataTypeEnum inputType, string outputName, DataTypeEnum outputType) {
        InitData();
        m_inputNames.Add(inputName);
        m_inputTypes.Add(inputType);
        m_outputNames.Add(outputName);
        m_outputTypes.Add(outputType);
    }
    public PipelineProfile(PipelineProfile pp) {
        m_inputNames = new List<string>(pp.m_inputNames);
        m_inputTypes = new List<DataTypeEnum>(pp.m_inputTypes);
        m_outputNames = new List<string>(pp.m_outputNames);
        m_outputTypes = new List<DataTypeEnum>(pp.m_outputTypes);
    }
}
