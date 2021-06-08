using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Components that have death scenes (or actions to take before Destroy()) register with this class, and they get to perform their action
/// Classes with this interface are responsible for calling Destroy() on their GameObjects
/// </summary>
public interface ISelfDestruct
{
    /// <summary>
    /// Go through all overActors and have them play their death scene
    /// Ensure everyone is ready to Die, then destroy the object
    /// </summary>
    public void FinalRites();
    /// <summary>
    /// Register an overActor to the list
    /// </summary>
    /// <param name="overActor">Component that wants to do something before being Destroyed</param>
    public void IHaveFinalWords(IDie overActor);
    /// <summary>
    /// For whatever reason, an overACtor is now gone, so it nolonger needs to do anything before Destroy
    /// </summary>
    /// <param name="overActor">Component that lelft</param>
    public void NeverMind(IDie overActor);
}

public class DefaultISelfDestruct<T>: ISelfDestruct where T : MonoBehaviour, ISelfDestruct
{
    T m_parentClass;
    bool m_dead;

    /// <summary>
    /// OverActors are components that want to do something right before this object is destroyed
    /// </summary>
    private List<IDie> m_overActors;

    public DefaultISelfDestruct(T parentClassIn)
    {
        m_parentClass = parentClassIn;
    }

    public void Awake()
    {
        m_dead = false;
        GetOverActors();
    }


    public void FixedUpdate()
    {
        if (m_dead)
        {
            // Keep trying to die
            FinalRites();
        }
    }

    // *** ISelfDestruct interface
    public void FinalRites()
    {
        m_dead = true;
        bool readyToDie = true;
        foreach (IDie overActor in m_overActors)
        {
            if (!overActor.Dying())
            {
                overActor.Die();
            }
            if (!overActor.ReadyToDie())
            {
                readyToDie = false;
            }
        }
        if (readyToDie)
        {
            MonoBehaviour.Destroy(m_parentClass.gameObject);
        }
    }
    public void IHaveFinalWords(IDie overActor)
    {
        m_overActors.Add(overActor);
    }
    public void NeverMind(IDie overActor)
    {
        m_overActors.Remove(overActor);
    }

    // *** Internal functions
    private void GetOverActors()
    {
        IDie[] overActorArray = m_parentClass.GetComponentsInChildren<IDie>();
        m_overActors = new List<IDie>(overActorArray);
    }
}