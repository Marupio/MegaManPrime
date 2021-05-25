using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectFinder // : MonoBehaviour
{
    public bool m_specifyLayerMask { get; set; }
    public LayerMask m_layerMask { get; set; }
    public bool m_specifyMinDepth { get; set; }
    public float m_minDepth { get; set; }
    public bool m_specifyMaxDepth { get; set; }
    public float m_maxDepth { get; set; }
    public bool m_specifyNameContains { get; set; }
    public string m_nameContains { get; set; }
    public bool m_specifyNameExact { get; set; }
    public string m_nameExact { get; set; }
    public bool m_specifyTagContains { get; set; }
    public string m_tagContains { get; set; }
    public bool m_specifyTagExact { get; set; }
    public string m_tagExact { get; set; }


    public int FindObjects(List<GameObject> found)
    {
        if (m_specifyNameContains && m_specifyNameExact || m_specifyTagContains && m_specifyTagExact)
        {
            Debug.LogError("Invalid settings: specify[Name|Tag]Exact and specife[Name|Tag]Contains cannot both be true.  Aborting search!");
            found = null;
            return -1;
        }

        // Initialise with tags because exactTag has a good find function
        if (m_specifyTagExact)
        {
            GameObject[] objArray = GameObject.FindGameObjectsWithTag(m_tagExact);
            found = new List<GameObject>(objArray);
            foreach (GameObject obj in objArray)
            {
                found.Add(obj);
            }
        }
        else
        {
            found = new List<GameObject>();
            GameObject[] results = Object.FindObjectsOfType<GameObject>(false);
            if (m_specifyTagContains)
            {
                foreach (GameObject res in results)
                {
                    if (res.gameObject.tag.Contains(m_tagContains))
                    {
                        found.Add(res.gameObject);
                    }
                }
            }
            else
            {
                foreach (GameObject res in results)
                {
                    found.Add(res.gameObject);
                }
            }
        }
        // Run through layerMask
        if (m_specifyLayerMask)
        {
            foreach (GameObject obj in found)
            {
                if (!GeneralTools.IsInLayerMask(obj, m_layerMask))
                {
                    found.Remove(obj);
                }
            }
        }
        if (m_specifyMinDepth)
        {
            foreach (GameObject obj in found)
            {
                if (obj.transform.position.z < m_minDepth)
                {
                    found.Remove(obj);
                }
            }
        }
        if (m_specifyMaxDepth)
        {
            foreach (GameObject obj in found)
            {
                if (obj.transform.position.z > m_maxDepth)
                {
                    found.Remove(obj);
                }
            }
        }
        if (m_specifyNameExact)
        {
            foreach (GameObject obj in found)
            {
                if (obj.name != m_nameExact)
                {
                    found.Remove(obj);
                }
            }
        }
        else if (m_specifyNameContains)
        {
            foreach (GameObject obj in found)
            {
                if (!obj.name.Contains(m_nameContains))
                {
                    found.Remove(obj);
                }
            }
        }
        return found.Count;
    }

    public int FindComponents<T>(List<T> found) where T : Component
    {
        if (m_specifyNameContains && m_specifyNameExact || m_specifyTagContains && m_specifyTagExact)
        {
            Debug.LogError("Invalid settings: specify[Name|Tag]Exact and specife[Name|Tag]Contains cannot both be true.  Aborting search!");
            found = null;
            return -1;
        }

        List<GameObject> objList;
        // Initialise with tags because exactTag has a good find function
        if (m_specifyTagExact)
        {
            GameObject[] objArray = GameObject.FindGameObjectsWithTag(m_tagExact);
            objList = new List<GameObject>(objArray);
            foreach (GameObject obj in objArray)
            {
                objList.Add(obj);
            }
        }
        else
        {
            objList = new List<GameObject>();
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
        }
        // Run through layerMask
        if (m_specifyLayerMask)
        {
            foreach (GameObject obj in objList)
            {
                if (!GeneralTools.IsInLayerMask(obj, m_layerMask))
                {
                    objList.Remove(obj);
                }
            }
        }
        if (m_specifyMinDepth)
        {
            foreach (GameObject obj in objList)
            {
                if (obj.transform.position.z < m_minDepth)
                {
                    objList.Remove(obj);
                }
            }
        }
        if (m_specifyMaxDepth)
        {
            foreach (GameObject obj in objList)
            {
                if (obj.transform.position.z > m_maxDepth)
                {
                    objList.Remove(obj);
                }
            }
        }
        if (m_specifyNameExact)
        {
            foreach (GameObject obj in objList)
            {
                if (obj.name != m_nameExact)
                {
                    objList.Remove(obj);
                }
            }
        }
        else if (m_specifyNameContains)
        {
            foreach (GameObject obj in objList)
            {
                if (!obj.name.Contains(m_nameContains))
                {
                    objList.Remove(obj);
                }
            }
        }

        foreach (GameObject obj in objList)
        {
            T comp = obj.GetComponent<T>();
            if (comp)
            {
                found.Add(comp);
            }
        }
        return found.Count;
    }
}
