using System.Collections.Generic;
using UnityEngine;

public interface IObjectRegistry : IObject {
    public IObject FindObject(long id, bool recursive=true);
    public List<IObject> FindObjects(string name, bool recursive=true);
    public List<string> TableOfContents(bool recursive=true);
    /// <summary>
    /// List of all child objects' ids, optionally recursive.
    /// </summary>
    public List<long> Index(bool recursive=true);
    public void Register(IObject obj);
    public bool Unregister(IObject obj);
    public bool Unregister(long id);
}


// Let's start working on interactions
// Collider is touching ground      <==> Yaxis.Enabled=true
// Collider is not touching ground  <==> Yaxis.Enabled=false
// I will build these as use cases present themselves
