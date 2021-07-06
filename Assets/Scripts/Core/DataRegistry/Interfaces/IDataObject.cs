using UnityEngine;

public interface IDataObject<L> : IDataObjectHeader {
    public L Data { get; set; }
}
