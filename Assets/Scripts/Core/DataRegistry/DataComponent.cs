using System.Collections.Generic;
using UnityEngine;

public interface IDataComponent<T> : IDataObject {
    public T Data { get; set; }
    public DataTypeEnum DataType { get; }
}

public class DcBool: DataObjectHeader, IDataComponent<bool> {
    protected bool m_data;
    public virtual bool Data { get=>m_data; set=>m_data=value; }
    public DataTypeEnum DataType { get=>DataTypeEnum.Bool; }
}
public class DcBoolCollider: DcBool {
    Collider2D m_collider;
    ContactFilter2D m_groundFilter;
    public override bool Data {
        get {
            return m_collider.IsTouching(m_groundFilter);
        }
    }
}

public class DataComponent<T> : DataObjectHeader {
    T m_data;
    
}

// DataComponentBool groundedDataObject = entity.FindDataComponent<bool>("Grounded");
// if (groundedDataObject.Data == true) {

// }

// public class DataComponentBoolColliderCheck : DataComponentBool {
//     public override T Data {
//         get {
//             return collider2D.yadayadayada()
//         }
//     }
// }