using System.Collections.Generic;
using UnityEngine;

// DataSource<--Actor - VariableWatch pattern
//  DataSource employs IDataComponentPattern
//  
// DataSource-->Actor - Trigger pattern

public interface IDataObjectHeader : IObject {
    public DataTypeEnum DataType { get; }
}
public interface IDataObject<L> : IDataObjectHeader {
    public L Data { get; set; }
}

// L main type, C component type.  e.g. Vector2, float.  List<Vector2>, Vector2.
public interface IDataSetHeader : IDataObjectHeader {
    public DataTypeEnum ComponentType { get; }
    //public List<string> ComponentNames { get; }
    public string GetComponentName(int elem);
    public string GetComponentName(string elem);
}
public interface IDataComponentSet<L, C> : IDataSetHeader, IDataObject<L> {
    public C this[int elem] { get; set; }
    public C GetComponent(int elem);
    public C GetComponent(string elem);
    public void SetComponent(int elem, C value);
    public void SetComponent(string elem, C value);
}

