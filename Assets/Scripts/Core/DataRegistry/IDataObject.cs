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
    public IDerivedDataObject DerivedDataObject();
    /// <summary>
    /// If data is a set with selectable components, this is not null
    /// </summary>
    public IDataSetObjectHeader DataSetObjectHeader();
}
public interface IDataObject<L> : IDataObjectHeader {
    public L Data { get; set; }
}


