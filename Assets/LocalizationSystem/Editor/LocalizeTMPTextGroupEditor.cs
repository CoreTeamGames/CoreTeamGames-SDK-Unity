using UnityEditorInternal;
using UnityEditor;
using UnityEngine;

namespace CoreTeamGamesSDK.Localization.Editor
{
    [CustomEditor(typeof(LocalizeTMPTextGroup))]
    public class LocalizeTMPTextGroupEditor : UnityEditor.Editor
    {
        private SerializedProperty _localizationFileName;
        private ReorderableList _localizeTMPTextGroupMembersList;


        private void OnEnable()
        {
            _localizationFileName = serializedObject.FindProperty("_localizationFileName");

            _localizeTMPTextGroupMembersList = new ReorderableList(serializedObject,
                    serializedObject.FindProperty("_localizeTMPTextGroupMembers"),
                    true, true, true, true);

            _localizeTMPTextGroupMembersList.drawHeaderCallback =
            (Rect rect) =>
            {
                EditorGUI.LabelField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), $"Localize TMP_Text group members");
            };
            _localizeTMPTextGroupMembersList.drawElementCallback =
            (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                var element = _localizeTMPTextGroupMembersList.serializedProperty.GetArrayElementAtIndex(index);
                rect.y += 2;
                EditorGUI.ObjectField(new Rect(rect.x, rect.y, rect.width / 2.5f, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("_text"),typeof(TMPro.TMP_Text),GUIContent.none);
                EditorGUI.LabelField(new Rect(rect.x + rect.width / 2.5f + 2, rect.y, rect.width - rect.width / 5f-2, EditorGUIUtility.singleLineHeight), new GUIContent("Line key:","The key of line for get localized line"));
                EditorGUI.PropertyField(
                     new Rect(rect.x + rect.width - rect.width / 2.5f, rect.y, rect.width / 2.5f, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("_lineKey"), GUIContent.none);
            };

        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(_localizationFileName, new GUIContent("File Name", "The name of File for localize"));
            _localizeTMPTextGroupMembersList.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }
    }
}