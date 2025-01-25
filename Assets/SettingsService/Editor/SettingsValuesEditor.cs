using UnityEditorInternal;
using UnityEngine;
using UnityEditor;

namespace CoreTeamGamesSDK.SettingsService.Editor
{
    [CustomEditor(typeof(SettingsValues))]
    public class SettingsValuesEditor : UnityEditor.Editor
    {
        private ReorderableList _valuesList;

        private float SingleLineHeight => EditorGUIUtility.singleLineHeight;
        private void OnEnable()
        {

            _valuesList = new ReorderableList(serializedObject,
                    serializedObject.FindProperty("_settingsValues"),
                    true, true, true, true);


            _valuesList.elementHeight = SingleLineHeight * 2 + 4;
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
                ESettingsValueType _valueType = (ESettingsValueType)element.FindPropertyRelative("_valueType").enumValueIndex;
                EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, SingleLineHeight), element.FindPropertyRelative("_valueName"));
                EditorGUI.PropertyField(new Rect(rect.x, rect.y + SingleLineHeight, rect.width, SingleLineHeight), element.FindPropertyRelative("_valueType"));
            };

        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.ObjectField(serializedObject.FindProperty("_audioMixer"));
            _valuesList.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }
    }
}