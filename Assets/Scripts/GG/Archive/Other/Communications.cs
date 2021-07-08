// using UnityEngine;

// // I don't think I'm going to use any of these classes

// public interface ITransmitter {

// }

// public interface IReceiver {
//     //*** Manual interface
//     public void AddTrigger(string name);
//     public void AddBool(string name, bool defaultValue=true);
//     public void AddInt(string name, int defaultValue=0);
//     public void AddFloat(string name, float defaultValue=0f);
//     public void AddVector2Int(string name, Vector2Int defaultValue=0f);
//     public void AddVector2(string name, Vector2 defaultValue=0f);
//     public void AddVector3Int(string name, Vector3Int defaultValue=0f);
//     public void AddVector3(string name, Vector3 defaultValue=0f);
//     public void AddVector4(string name, Vector4 defaultValue=0f);
//     public void AddQuaternion(string name, Quaternion defaultValue=0f);
//     public bool HasTrigger(string name);
//     public bool HasBool(string name);
//     public bool HasInt(string name);
//     public bool HasFloat(string name);
//     public bool HasVector2Int(string name);
//     public bool HasVector2(string name);
//     public bool HasVector3Int(string name);
//     public bool HasVector3(string name);
//     public bool HasVector4(string name);
//     public bool HasQuaternion(string name);
//     public void SetTrigger(string name);
//     public void SetBool(string name, bool defaultValue=true);
//     public void SetInt(string name, int defaultValue=0);
//     public void SetFloat(string name, float defaultValue=0f);
//     public void SetVector2Int(string name, Vector2Int defaultValue=0f);
//     public void SetVector2(string name, Vector2 defaultValue=0f);
//     public void SetVector3Int(string name, Vector3Int defaultValue=0f);
//     public void SetVector3(string name, Vector3 defaultValue=0f);
//     public void SetVector4(string name, Vector4 defaultValue=0f);
//     public void SetQuaternion(string name, Quaternion defaultValue=0f);
//     // *** Communication pipeline
//     public void ProcessPacket(Packet pktIn);
//     // *** Responses
//     public void Execute(int type); // And what?
// }

// public class Packet {
//     enum Direction {
//         Up,     // Parent-ward
//         Down    // Child-ward
//     }
//     public Direction direction;
//     public int nLevels;
//     public DataObjectFilter targetFilter;
// }

// public class QueryPacket : Packet {}
// public class ResponsePacketBool : Packet {}
// public class ResponsePacketInt : Packet {}
// public class ResponsePacketFloat : Packet {}
// public class ResponsePacketVector2Int : Packet {}
// public class ResponsePacketVector2 : Packet {}
// public class ResponsePacketVector3Int : Packet {}
// public class ResponsePacketVector3 : Packet {}
// public class ResponsePacketVector4 : Packet {}
// public class ResponsePacketQuaternion : Packet {}
// public class ResponsePacketListBool : Packet {}
// public class ResponsePacketListInt : Packet {}
// public class ResponsePacketListFloat : Packet {}
// public class ResponsePacketListVector2Int : Packet {}
// public class ResponsePacketListVector2 : Packet {}
// public class ResponsePacketListVector3Int : Packet {}
// public class ResponsePacketListVector3 : Packet {}
// public class ResponsePacketListVector4 : Packet {}
// public class ResponsePacketListQuaternion : Packet {}
// public class ResponsePacketKVariablesBool : Packet {}
// public class ResponsePacketKVariablesInt : Packet {}
// public class ResponsePacketKVariablesFloat : Packet {}
// public class ResponsePacketKVariablesVector2Int : Packet {}
// public class ResponsePacketKVariablesVector2 : Packet {}
// public class ResponsePacketKVariablesVector3Int : Packet {}
// public class ResponsePacketKVariablesVector3 : Packet {}
// public class ResponsePacketKVariablesVector4 : Packet {}
// public class ResponsePacketKVariablesQuaternion : Packet {}
// public class ResponsePacketKVariablesExtBool : Packet {}
// public class ResponsePacketKVariablesExtInt : Packet {}
// public class ResponsePacketKVariablesExtFloat : Packet {}
// public class ResponsePacketKVariablesExtVector2Int : Packet {}
// public class ResponsePacketKVariablesExtVector2 : Packet {}
// public class ResponsePacketKVariablesExtVector3Int : Packet {}
// public class ResponsePacketKVariablesExtVector3 : Packet {}
// public class ResponsePacketKVariablesExtVector4 : Packet {}
// public class ResponsePacketKVariablesExtQuaternion : Packet {}
