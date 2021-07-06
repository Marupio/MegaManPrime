using System.Collections.Generic;
using UnityEngine;

public interface IDataObjectHeader : IObject {
    /// <summary>
    /// Data type involved
    /// </summary>
    public DataTypeEnum DataType { get; }
    /// <summary>
    /// If data depends on another DataObject, this is not null
    /// </summary>
    public IDerivedDataObjectHeader DerivedDataObject();
    /// <summary>
    /// If data is a set with selectable components, this is not null
    /// </summary>
    public IDataSetObjectHeader DataSetObjectHeader();
}

public class DataObjectHeader : ObjectHeader, IDataObjectHeader {
    public virtual DataTypeEnum DataType { get=>DataTypeEnum.None; }
    public virtual IDerivedDataObjectHeader DerivedDataObject() {
        if (this is IDerivedDataObjectHeader) { return this as IDerivedDataObjectHeader; }
        return null;
    }
    public virtual IDataSetObjectHeader DataSetObjectHeader() {
        if (this is IDataSetObjectHeader) { return this as IDataSetObjectHeader; }
        return null;
    }
    public DataObjectHeader(string name, IObjectRegistry parent = null) : base (name, parent) {}
    public DataObjectHeader(IDataObjectHeader obj) : base (obj) {}
    public DataObjectHeader(DataObjectHeader obj) : base (obj) {}
    public DataObjectHeader() {}
}
