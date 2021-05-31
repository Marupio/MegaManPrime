// using UnityEngine;
// using UnityEditor;

// [CustomPropertyDrawer(typeof(SurfaceModel))]
// public class SurfaceModelDrawer : PropertyDrawer
// {
//     // Draw the property inside the given rect
//     public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//     {
//         if (property == null)
//         {
//             return;
//         }

//         // Using BeginProperty / EndProperty on the parent property means that
//         // prefab override logic works on the entire property.
//         EditorGUI.BeginProperty(position, label, property);

//         // Draw label
//         position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

//         // Don't make child fields be indented
//         var indent = EditorGUI.indentLevel;
//         EditorGUI.indentLevel = 0;

//         // Calculate rects
//         var tagNameRect = new Rect(position.x, position.y, 20, position.height);
//         var wallVelocityRect = new Rect(position.x + 25, position.y, 50, position.height);
//         var resistanceRect = new Rect(position.x + 80, position.y, 20, position.height);
//         var slidableRect = new Rect(position.x + 105, position.y, position.width - 105, position.height);

//         Debug.Log("Here I am");

//     // public float mu;
//     // public Vector2 wallVelocity;
//     // public float resistance;
//     // public bool slidable;

//         // Draw fields - passs GUIContent.none to each so they are drawn without labels
//         EditorGUI.PropertyField(tagNameRect, property.FindPropertyRelative("mu"), GUIContent.none);
//         EditorGUI.PropertyField(wallVelocityRect, property.FindPropertyRelative("wallVelocity"), GUIContent.none);
//         EditorGUI.PropertyField(resistanceRect, property.FindPropertyRelative("resistance"), GUIContent.none);
//         EditorGUI.PropertyField(slidableRect, property.FindPropertyRelative("slidable"), GUIContent.none);

//         // Set indent back to what it was
//         EditorGUI.indentLevel = indent;

//         EditorGUI.EndProperty();
//     }
// }