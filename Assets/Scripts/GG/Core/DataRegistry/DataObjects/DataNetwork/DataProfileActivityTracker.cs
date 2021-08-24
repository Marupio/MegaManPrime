using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class keeps records of input and output variable MTags to see if objects have been modified since the last time it was checked.
///     * Input tracking can be used for lazy evaluation (suppressing calculations if nothing has changed).
///     * Output tracking can be used to check for data alterations coming from elsewhere.
/// </summary>
public class DataProfileActivityTracker<I, O> where I : DataObjList where O : DataObjList
{
    // TODO - To clear the bugs here, we need to refactor this to accomodate the fact that a DataPortModule is nolonger index-based.  It is now
    // a dictionary, so we need to organize our data here by keys instead of by indices.
    DataPortModule<I, O> m_profiles;
    List<bool> m_activeByProfileIndex; // true if this profile index has ever seen activity
    List<long> m_inputTagHashByProfileIndex; // contains the latest hash of input mtags
    List<long> m_outputTagHashByProfileIndex; // contains the latest hash of output mtags
    bool m_inputEnabled; // when true, input tracking is enabled
    bool m_outputEnabled; // when true, output tracking is enabled

    public bool InputEnabled { get=>m_inputEnabled; set=>m_inputEnabled=value; }
    public bool OutputEnabled { get=>m_outputEnabled; set=>m_outputEnabled=value; }

    public bool HaveInputsChanged(DataPortProfile profile, ActiveDataPortConnections<I, O> connections) {
        if (!m_inputEnabled) {
            return true;
        }

        // Compile fix hack
        int index = 0;// m_profiles.Profiles.IndexOf(profile);
        #if DEBUG
            if (index < 0) {
                Debug.LogException(new System.IndexOutOfRangeException("profile " + profile.Name + " not found in list"));
            }
        #endif
        return HaveTheseChanged(index, connections.Inputs, m_inputTagHashByProfileIndex);
    }
    public bool HaveOutputsChanged(DataPortProfile profile, ActiveDataPortConnections<I, O> connections) {
        if (!m_outputEnabled) {
            return true;
        }
        // Compile fix hack
        int index = 0;//m_profiles.Profiles.IndexOf(profile);
        #if DEBUG
            if (index < 0) {
                Debug.LogException(new System.IndexOutOfRangeException("profile " + profile.Name + " not found in list"));
            }
        #endif
        return HaveTheseChanged(index, connections.Outputs, m_outputTagHashByProfileIndex);
    }

    bool HaveTheseChanged(int index, DataObjList xputs, List<long> xputTagHashByIndex) {
        if (CheckSizes(index, xputs, xputTagHashByIndex)) {
            if (!m_activeByProfileIndex[index]) {
                m_activeByProfileIndex[index] = true;
                xputTagHashByIndex[index] = GetHashCode(xputs);
                return true;
            } else {
                long newHash = GetHashCode(xputs);
                if (xputTagHashByIndex[index] != newHash) {
                    xputTagHashByIndex[index] = newHash;
                    return true;
                }
                return false;
            }
        }
        return true;
    }
    bool CheckSizes(int index, DataObjList xputs, List<long> xputTagHashByIndex) {
        if (m_activeByProfileIndex.Count <= index) {
            for (int i = m_activeByProfileIndex.Count; i <= index-1; ++i) {
                m_activeByProfileIndex.Add(false);
                xputTagHashByIndex.Add(0);
            }
            m_activeByProfileIndex.Add(true);
            xputTagHashByIndex.Add(GetHashCode(xputs));
            return false;
        }
        return true;
    }
    long GetHashCode(DataObjList objs) {
        unchecked {
            long hash = 17;
            foreach(IObj obj in objs) {
                hash = hash*486187739L + obj.MTag.Tag;
            }
            return hash;
        }
    }
    public DataProfileActivityTracker(DataPortModule<I, O> profiles) {
        m_profiles = profiles;
        m_activeByProfileIndex = new List<bool>();
        m_inputTagHashByProfileIndex = new List<long>();
        m_outputTagHashByProfileIndex = new List<long>();
    }
}