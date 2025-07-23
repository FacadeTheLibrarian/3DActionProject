using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(NamedArrayAttribute))]
#endif
public class NamedArrayDrawer : PropertyDrawer {
#if UNITY_EDITOR
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        try {
            int index = int.Parse(property.propertyPath.Split('[', ']')[1]);
            EditorGUI.PropertyField(position, property, new GUIContent(((NamedArrayAttribute)attribute).NAMES[index]), true);
        } catch {
            EditorGUI.PropertyField(position, property, label);
        }
    }
#endif
}