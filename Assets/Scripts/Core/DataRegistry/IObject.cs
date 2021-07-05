/// <summary>
/// A basic header for objects that can be stored in the DataRegistry.  Other objects may depend on data in this object.
/// This can be attached to the actual object, or a seperate header used to refer to that object
/// </summary>
public interface IObject {
    /// <summary>
    /// Name assigned to this object, can be duplicated
    /// </summary>
    public string Name { get; }
    /// <summary>
    /// A unique ID number, assigned upon creation, and never changes
    /// </summary>
    /// <value></value>
    public long Id { get; }
    /// <summary>
    /// Set ID to a new value
    /// </summary>
    public void SetId(long id);
    /// <summary>
    /// The DataRegistry that I'm registered in, or null if orphan
    /// </summary>
    public IObjectRegistry Parent { get; }
    /// <summary>
    /// Used by IObjectRegistry classes to remove the IObject from the registry
    /// </summary>
    public void InternalSetOrphan();
    /// <summary>
    /// A unique tag indicating when it was last modified, relative to other tags
    /// </summary>
    public ModTag MTag { get; }
    /// <summary>
    /// Indicates this data object has changed, updates MTag to latest
    /// </summary>
    public void SetModified();
    // public List<string> [Type]DataTableOfContents
    // public List<Type> [Type]Data
    /// <summary>
    /// Return a header-only IDataObject copy of this object
    /// </summary>
    // TODO - This may mess up my data model.  Maybe get rid of this and just use the IObject reference!
    public ObjectHeader CloneHeader();
    /// <summary>
    /// Convert this to an IObjectRegistry, or null if it is not one
    /// </summary>
    public IObjectRegistry ObjectRegistry();
    /// <summary>
    /// Convert this to an IDataObjectHeader, or null if it is not one.
    /// </summary>
    public IDataObjectHeader DataObjectHeader();
}
