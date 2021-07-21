using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataPortProfileList : IEnumerable<DataPortProfile> {
    public List<DataPortProfile> m_data;
    int m_activeProfileIndex;
    DataPortProfile m_activeProfile;

    public DataPortProfile ActiveProfile { get=>m_activeProfile; }
    public bool SetActiveProfileAt(int index) {
        if (index < m_data.Count && index >= 0) {
            InternalSetIndex(index);
            return true;
        }
        return false;
    }
    public bool SetActiveProfileName(string profileName) {
        int index = NameToIndex(profileName);
        if (index >= 0) {
            InternalSetIndex(index);
            return true;
        }
        return false;
    }
    public bool SetActiveProfile(DataPortProfile dpp) {
        int index = m_data.IndexOf(dpp);
        if (index >= 0) {
            InternalSetIndex(index);
            return true;
        }
        return false;
    }

    // Select methods to pass-through to List
    public int Count { get=>m_data.Count; }
    public void Add(DataPortProfile dpp) {
        m_data.Add(dpp);
    }
    public void AddRange(List<DataPortProfile> dpps) {
        m_data.AddRange(dpps);
    }
    public void Remove(DataPortProfile dpp) {
        m_data.Remove(dpp);
    }
    public void RemoveAll(Predicate<DataPortProfile> predicate) {
        m_data.RemoveAll(predicate);
    }
    public void RemoveAt(int index) {
        m_data.RemoveAt(index);
    }

    public int NameToIndex(string name) {
        return m_data.IndexOf(m_data.Where(p => p.Name == name).FirstOrDefault());
    }
    public DataPortProfile this[int index] {
        get { return m_data[index]; }
        set { m_data[index] = value; }
    }
    public DataPortProfile this[string name] {
        get {
            int index = NameToIndex(name);
            return index >= 0 ? m_data[index] : null;
        }
        set {
            int index = NameToIndex(name);
            if (index >= 0) {
                m_data[index] = value;
            }
            Debug.LogError(name + " not in DataProfileList");
        }
    }

    // *** IEnumerable interface
    public IEnumerator<DataPortProfile> GetEnumerator()
    {
        return m_data.GetEnumerator();
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    protected void InternalSetIndex(int index) { // No checks
        m_activeProfileIndex = index;
        m_activeProfile = m_data[index];
    }

}