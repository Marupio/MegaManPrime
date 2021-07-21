using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectFinder : MonoBehaviour
{
    public string m_finderForWhat;

    [Header("Finder Settings")]
    [SerializeField] private bool m_specifyLayerMask;
    [SerializeField] private LayerMask m_layerMask;
    [SerializeField] private bool m_specifyMinDepth;
    [SerializeField] private float m_minDepth;
    [SerializeField] private bool m_specifyMaxDepth;
    [SerializeField] private float m_maxDepth;
    [SerializeField] private bool m_specifyNameContains;
    [SerializeField] private string m_nameContains;
    [SerializeField] private bool m_specifyNameExact;
    [SerializeField] private string m_nameExact;
    [SerializeField] private bool m_specifyTagContains;
    [SerializeField] private string m_tagContains;
    [SerializeField] private bool m_specifyTagExact;
    [SerializeField] private string m_tagExact;

    public ObjectFinder() {}

    // For easy filter setting

    public void SetLayerMask(LayerMask varIn)
    {
        m_specifyLayerMask = true;
        m_layerMask = varIn;
    }

    public void SetMinDepth(float varIn)
    {
        m_specifyMinDepth = true;
        m_minDepth = varIn;
    }

    public void SetMaxDepth(float varIn)
    {
        m_specifyMaxDepth = true;
        m_maxDepth = varIn;
    }

    public void SetNameContains(string varIn)
    {
        m_specifyNameContains = true;
        m_nameContains = varIn;
    }

    public void SetNameExact(string varIn)
    {
        m_specifyNameExact = true;
        m_nameExact = varIn;
    }

    public void SetTagContains(string varIn)
    {
        m_specifyTagContains = true;
        m_tagContains = varIn;
    }

    public void SetTagExact(string varIn)
    {
        m_specifyTagExact = true;
        m_tagExact = varIn;
    }

    /// <summary>
    /// Returns true if valid, if invalid, throws Debug.LogError messages and returns false
    /// </summary>
    public bool Valid()
    {
        if (m_specifyLayerMask && m_layerMask == 0)
        {
            Debug.LogError("LayerMask is specified but empty.");
            return false;
        }
        if (m_specifyNameContains && string.IsNullOrEmpty(m_nameContains))
        {
            Debug.LogError("NameContains is specified but empty.");
            return false;
        }
        if (m_specifyNameExact && string.IsNullOrEmpty(m_nameExact))
        {
            Debug.LogError("NameExact is specified but empty.");
            return false;
        }
        if (m_specifyTagContains && string.IsNullOrEmpty(m_tagContains))
        {
            Debug.LogError("TagContains is specified but empty.");
            return false;
        }
        if (m_specifyTagExact && string.IsNullOrEmpty(m_tagExact))
        {
            Debug.LogError("TagExact is specified but empty.");
            return false;
        }
        if (m_specifyNameContains && m_specifyNameExact || m_specifyTagContains && m_specifyTagExact)
        {
            Debug.LogError("Invalid settings: specify[Name|Tag]Exact and specify[Name|Tag]Contains cannot both be true.");
            return false;
        }
        return true;
    }

    /// <summary>
    /// Performs search and expects to find only one object
    /// </summary>
    /// <returns>The first object that meets the criteria, or null if none</returns>
    public GameObject FindObject(bool logWarningIfMultiple = true, bool logErrorIfNone = true)
    {
        List<GameObject> objList = FindObjects();
        if (objList.Count == 0)
        {
            if (logErrorIfNone)
            {
                Debug.LogError("ObjectFinder failed to find any objects with the given criteria");
            }
            return null;
        }
        if (objList.Count > 1 && logWarningIfMultiple)
        {
            // List<string> names = new List<string>();
            string names = "";
            foreach(GameObject obj in objList)
            {
                // names.Add(obj.name);
                names += " " + obj.name;
            }
            Debug.LogWarning("ObjectFinder found " + objList.Count + ", was expecting only one.\nNames: [" + names + "]");
        }
        return objList[0];
    }

    /// <summary>
    /// Returns a list of GameObjects that meet the specified criteria
    /// </summary>
    public List<GameObject> FindObjects()
    {
        if (!Valid())
        {
            return new List<GameObject>();
        }

        List<GameObject> objList = new List<GameObject>();
        // Initialise and apply tag filters
        if (m_specifyTagExact)
        {
            objList = InitWithExactTag();
        }
        else
        {
            objList = InitWithAllObjects();
        }
        ApplyRemainingFilters(ref objList);
        return objList;
    }

    /// <summary>
    /// Performs search and expects to find only one object
    /// </summary>
    /// <returns>The first object that meets the criteria, or null if none</returns>
    public T FindComponent<T>(bool logWarningIfMultiple = true, bool logErrorIfNone = true) where T : Component
    {
        List<T> compList = FindComponents<T>();
        if (compList.Count == 0)
        {
            if (logErrorIfNone)
            {
                Debug.LogError("ObjectFinder failed to find any components with the given criteria");
            }
            return default(T);
        }
        if (compList.Count > 1 && logWarningIfMultiple)
        {
            Debug.LogWarning("ObjectFinder found " + compList.Count + ", was expecting only one");
        }
        return compList[0];
    }

    /// <summary>
    /// Returns a list of Components (of a given Type) that meet the specified criteria
    /// </summary>
    public List<T> FindComponents<T>() where T : Component
    {
        if (!Valid())
        {
            return new List<T>();
        }

        List<GameObject> objList = new List<GameObject>();
        // Initialise and apply tag filters
        if (m_specifyTagExact)
        {
            objList = InitWithExactTag();
        }
        else
        {
            objList = InitByComponentType<T>();
        }
        ApplyRemainingFilters(ref objList);

        // Grab components of specified type
        List<T> compList = new List<T>();
        foreach (GameObject obj in objList)
        {
            T comp = obj.GetComponent<T>();
            if (comp)
            {
                compList.Add(comp);
            }
        }
        return compList;
    }

    private List<GameObject> InitWithExactTag()
    {
        GameObject[] objArray = GameObject.FindGameObjectsWithTag(m_tagExact);
        List<GameObject> objList = new List<GameObject>(objArray);
        foreach (GameObject obj in objArray)
        {
            objList.Add(obj);
        }
        return objList;
    }

    private List<GameObject> InitWithAllObjects()
    {
        List<GameObject> objList = new List<GameObject>();
        GameObject[] results = Object.FindObjectsOfType<GameObject>(false);
        if (m_specifyTagContains)
        {
            foreach (GameObject res in results)
            {
                if (res.gameObject.tag.Contains(m_tagContains))
                {
                    objList.Add(res.gameObject);
                }
            }
        }
        else
        {
            foreach (GameObject res in results)
            {
                objList.Add(res.gameObject);
            }
        }
        return objList;
    }

    private List<GameObject> InitByComponentType<T>() where T : Component
    {
        List<GameObject> objList = new List<GameObject>();
        T[] results = Object.FindObjectsOfType<T>(false);
        if (m_specifyTagContains)
        {
            foreach (T res in results)
            {
                if (res.gameObject.tag.Contains(m_tagContains))
                {
                    objList.Add(res.gameObject);
                }
            }
        }
        else
        {
            foreach (T res in results)
            {
                objList.Add(res.gameObject);
            }
        }
        return objList;
    }

    private void ApplyRemainingFilters(ref List<GameObject> objList)
    {
        // Run through layerMask
        if (m_specifyLayerMask)
        {
            for (int i = objList.Count-1; i >= 0; --i)
            {
                if (!GeneralGameTools.IsInLayerMask(objList[i], m_layerMask))
                {
                    objList.Remove(objList[i]);
                }
            }
        }
        if (m_specifyMinDepth)
        {
            objList.RemoveAll(obj => obj.transform.position.z < m_minDepth);
        }
        if (m_specifyMaxDepth)
        {
            objList.RemoveAll(obj => obj.transform.position.z > m_maxDepth);
        }
        if (m_specifyNameExact)
        {
            objList.RemoveAll(obj => obj.name != m_nameExact);
        }
        else if (m_specifyNameContains)
        {
            objList.RemoveAll(obj => !obj.name.Contains(m_nameContains));
        }
    }
}
