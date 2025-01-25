using UnityEditorInternal;
using UnityEngine.UI;
using UnityEngine;
using UnityEditor;
using TMPro;

namespace CoreTeamGamesSDK.SettingsService.Editor
{
    [CustomEditor(typeof(SettingsMenu))]
    public class SettingsMenuEditor : UnityEditor.Editor
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


            _valuesList.elementHeight = SingleLineHeight * 5 + 4;
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

                EditorGUI.LabelField(new Rect(rect.x, rect.y, rect.width, SingleLineHeight), $"Value Name: {'"'}{_value.FindPropertyRelative("_valueName").stringValue}{'"'}.");
                EditorGUI.LabelField(new Rect(rect.x, rect.y + SingleLineHeight, rect.width, SingleLineHeight), $"Value Type: {'"'}{_value.FindPropertyRelative("_valueType").enumDisplayNames[_value.FindPropertyRelative("_valueType").enumValueIndex]}{'"'}.");
                EditorGUI.PropertyField(new Rect(rect.x, rect.y + SingleLineHeight * 2, rect.width, SingleLineHeight), _element.FindPropertyRelative("_elementType"));
                switch ((EUIElementType)_element.FindPropertyRelative("_elementType").enumValueIndex)
                {
                    case EUIElementType.Toggle:
                        EditorGUI.ObjectField(new Rect(rect.x, rect.y + SingleLineHeight * 3, rect.width, SingleLineHeight), _uiElement, typeof(Toggle));
                        break;

                    case EUIElementType.DropDown:
                        EditorGUI.ObjectField(new Rect(rect.x, rect.y + SingleLineHeight * 3, rect.width, SingleLineHeight), _uiElement, typeof(TMP_Dropdown));
                        break;

                    case EUIElementType.InputField:
                        EditorGUI.ObjectField(new Rect(rect.x, rect.y + SingleLineHeight * 3, rect.width, SingleLineHeight), _uiElement, typeof(TMP_InputField));
                        break;

                    case EUIElementType.Slider:
                        EditorGUI.ObjectField(new Rect(rect.x, rect.y + SingleLineHeight * 3, rect.width, SingleLineHeight), _uiElement, typeof(Slider));
                        break;

                    default:
                        break;
                }
                EditorGUI.DrawRect(new Rect(rect.x, rect.y + SingleLineHeight * 4 + SingleLineHeight / 2, rect.width, 1), new Color(0.5f, 0.5f, 0.5f, 1));
            };
        }


        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            _valuesList.DoLayoutList();
            if (GUILayout.Button("Update Content") == true)
            {
                (serializedObject.targetObject as SettingsMenu).UpdateContent();
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}