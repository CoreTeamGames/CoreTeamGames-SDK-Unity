using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using UnityEditor.Graphs;

[CustomEditor(typeof(SettingsValues))]
public class SettingsValuesEditor : Editor
{
    private ReorderableList _valuesList;

    private float SingleLineHeight => EditorGUIUtility.singleLineHeight;
    private void OnEnable()
    {

        _valuesList = new ReorderableList(serializedObject,
                serializedObject.FindProperty("_settingsValues"),
                true, true, true, true);


        _valuesList.elementHeight = SingleLineHeight * 3 + 4;
        _valuesList.drawHeaderCallback =
        (Rect rect) =>
        {
            EditorGUI.LabelField(new Rect(rect.x, rect.y, rect.width, SingleLineHeight), $"Settings Values");
        };
        _valuesList.drawElementCallback =
        (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            rect.y += 2;
            var element = _valuesList.serializedProperty.GetArrayElementAtIndex(index);
            SettingsValueType _valueType = (SettingsValueType)element.FindPropertyRelative("_valueType").enumValueIndex;
            EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, SingleLineHeight), element.FindPropertyRelative("_valueName"));
            EditorGUI.PropertyField(new Rect(rect.x, rect.y+SingleLineHeight, rect.width, SingleLineHeight), element.FindPropertyRelative("_valueType"));

            switch (_valueType)
            {
                case SettingsValueType.boolValue:
                    EditorGUI.Toggle(new Rect(rect.x, rect.y + SingleLineHeight * 2, rect.width, SingleLineHeight),"Value", element.FindPropertyRelative("boolValue").boolValue);
                    break;
                case SettingsValueType.intValue:
                    EditorGUI.IntField(new Rect(rect.x, rect.y + SingleLineHeight * 2, rect.width, SingleLineHeight),"Value", element.FindPropertyRelative("intValue").intValue);
                    break;
                case SettingsValueType.floatValue:
                    EditorGUI.FloatField(new Rect(rect.x, rect.y + SingleLineHeight * 2, rect.width, SingleLineHeight),"Value", element.FindPropertyRelative("floatValue").floatValue);
                    break;
                case SettingsValueType.stringValue:
                    EditorGUI.TextField(new Rect(rect.x, rect.y + SingleLineHeight * 2, rect.width, SingleLineHeight),"Value", element.FindPropertyRelative("stringValue").stringValue);
                    break;
                default:
                    break;
            }
        };

    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        _valuesList.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }
}
