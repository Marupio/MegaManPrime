// using System.Collections.Generic;




// TODO - sample derivedUpdater

// public class doublerDerivedUpdater : DerivedUpdaterBase {
//     public void InitModule() {
//         m_module.Profiles.AddRange(
//             new List<DataPortProfile> {
//                 new DataPortProfile("float", "x", DataTypeEnum.Float, "y", DataTypeEnum.Float),
//                 new DataPortProfile("Vector2", "x", DataTypeEnum.Vector2Type, "y", DataTypeEnum.Vector2Type),
//                 new DataPortProfile("Vector3", "x", DataTypeEnum.Vector3Type, "y", DataTypeEnum.Vector3Type)
//             }
//         );
//     }
//     public DataPortProfile(
//         string name,
//         string inputName,
//         DataTypeEnum inputType,
//         string outputName,
//         DataTypeEnum outputType,
//         object activateWithConnections = null
//     );
//         }
//     }

//     public doublerDerivedUpdater(string activeProfileName, InputVariableTypeWhatever x);
// }

// // Usage
//     FloatSourceDataObj xVar("x", parent);
//     doublerDerivedUpdater ddu("float", xVar);
//     FloatDerivedDataObj yVar = ddu.SingleOutput();
//     // ... or
//     FloatDerivedDataObj yVar = d


//     DoublerDerivedUpdater m_ddu;
//     FloatSourceDataObj m_xVar;
//     FloatDerivedDataObj m_yVar;

//     mrConstructor() {
//         m_ddu = new DoublerDerivedUpdater()
//         m_xVar = new FloatSourceDataObj("x", m_ddu, 0.5);
//         m_yVar = m_ddu.ActivateSingleOutput("float", m_xVar)
//     }