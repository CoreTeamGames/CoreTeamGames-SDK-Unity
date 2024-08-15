using UnityEditorInternal;
using UnityEngine;
using UnityEditor;

namespace CoreTeamGamesSDK.Localization.Editor
{
    [CustomEditor(typeof(LocalizeTMPText))]
    public class LocalizeTMPTextEditor : UnityEditor.Editor
    {
        #region Variables
        private ReorderableList _variablesList;
        private SerializedProperty _fileName;
        private SerializedProperty _lineKey;
        #endregion

        #region Code
        private void OnEnable()
        {
            _fileName = serializedObject.FindProperty("_fileName");
            _lineKey = serializedObject.FindProperty("_lineKey");

            _variablesList = new ReorderableList(serializedObject,
                    serializedObject.FindProperty("variables"),
                    true, true, true, true);

            _variablesList.drawHeaderCallback =
            (Rect rect) =>
            {
                EditorGUI.LabelField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), $"Variables");
            };
            _variablesList.drawElementCallback =
            (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                var variableName = _variablesList.serializedProperty.GetArrayElementAtIndex(index);
                rect.y += 2;

                EditorGUI.LabelField(new Rect(40, rect.y, rect.width, EditorGUIUtility.singleLineHeight), $"Variable {index}: ");

                EditorGUI.PropertyField(
                    new Rect(120, rect.y, rect.width - 80, EditorGUIUtility.singleLineHeight),
                variableName, GUIContent.none);
            };
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.LabelField("Set-Up translate", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_fileName, new GUIContent("File Name", "The name of File for localize"));
            EditorGUILayout.PropertyField(_lineKey, new GUIContent("Line Key", "The key of line for localize"));
            EditorGUILayout.Space();
            _variablesList.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }
        #endregion
    }
}