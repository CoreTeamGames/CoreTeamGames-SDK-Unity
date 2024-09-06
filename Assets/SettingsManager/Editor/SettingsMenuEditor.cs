using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine.UI;
using TMPro;

[CustomEditor(typeof(SettingsMenu))]
public class SettingsMenuEditor : Editor
{
    private ReorderableList _valuesList;
    private SettingsMenu _menu;
    private float SingleLineHeight => EditorGUIUtility.singleLineHeight;

    private void OnEnable()
    {
_menu = (SettingsMenu)target;

        _valuesList = new ReorderableList(serializedObject,
                serializedObject.FindProperty("_settingsValuesWithUIElements"),
                false, true, false, false);


        _valuesList.elementHeight = SingleLineHeight * 4 + 4;
        _valuesList.drawHeaderCallback =
        (Rect rect) =>
        {
            EditorGUI.LabelField(new Rect(rect.x, rect.y, rect.width, SingleLineHeight), $"Settings Values");
        };
        _valuesList.drawElementCallback =
        (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            rect.y += 2;
            var _element = _valuesList.serializedProperty.GetArrayElementAtIndex(index);
            var _uiElement = _element.FindPropertyRelative("_uiElement");
            var _value = _element.FindPropertyRelative("_value");

            EditorGUI.LabelField(new Rect(rect.x, rect.y, rect.width, SingleLineHeight * 2), $"Value Name: {'"'}{_value.FindPropertyRelative("_valueName").stringValue}{'"'}.\nValue Type: {'"'}{_value.FindPropertyRelative("_valueType").enumDisplayNames[_value.FindPropertyRelative("_valueType").enumValueIndex]}{'"'}");
            EditorGUI.PropertyField(new Rect(rect.x, rect.y + SingleLineHeight * 2, rect.width, SingleLineHeight), _element.FindPropertyRelative("_elementType"));
            if (_element.FindPropertyRelative("_elementType").enumValueIndex == 0)
            {
                EditorGUI.ObjectField(new Rect(rect.x, rect.y + SingleLineHeight * 3, rect.width, SingleLineHeight), _uiElement, typeof(Toggle));
            }
            else if (_element.FindPropertyRelative("_elementType").enumValueIndex == 1)
            {
                EditorGUI.ObjectField(new Rect(rect.x, rect.y + SingleLineHeight * 3, rect.width, SingleLineHeight), _uiElement, typeof(TMP_InputField));
            }
            else if (_element.FindPropertyRelative("_elementType").enumValueIndex == 2)
            {
                EditorGUI.ObjectField(new Rect(rect.x, rect.y + SingleLineHeight * 3, rect.width, SingleLineHeight), _uiElement, typeof(TMP_Dropdown));
            }
            else if (_element.FindPropertyRelative("_elementType").enumValueIndex == 3)
            {
                EditorGUI.ObjectField(new Rect(rect.x, rect.y + SingleLineHeight * 3, rect.width, SingleLineHeight), _uiElement, typeof(Slider));
            }

        };
    }


    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        _valuesList.DoLayoutList();
        if (GUILayout.Button("Update Content"))
        {
            _menu.AddSettingsValues();
        }
        serializedObject.ApplyModifiedProperties();
    }
}