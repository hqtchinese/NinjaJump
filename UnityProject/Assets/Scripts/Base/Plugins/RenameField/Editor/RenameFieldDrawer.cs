using UnityEngine;
using UnityEditor;


namespace GameBase.Editor
{
    [CustomPropertyDrawer(typeof(RenameFieldAttribute))]
    public class RenameFieldDrawer : PropertyDrawer
    {

    #if UNITY_EDITOR
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) 
        {
            RenameFieldAttribute attr = (RenameFieldAttribute)attribute;
            label.text = attr.FieldName;
            EditorGUI.PropertyField(position, property, label);
        }
    #endif
    }
    
}