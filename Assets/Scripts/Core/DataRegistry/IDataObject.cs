using System.Collections.Generic;
using UnityEngine;

public interface IDataObjectHeader : IObject {
    public DataTypeEnum DataType { get; }
    // TODO - Add conversion to IDataSetObjectHeader if available
}
public interface IDataObject<L> : IDataObjectHeader {
    public L Data { get; set; }
}


