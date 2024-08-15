using UnityEditor;

namespace CoreTeamGamesSDK.Localization.Editor
{
    [CustomEditor(typeof(TMPDropdownLanguageSwitcher))]
    public class TMPDropdownLanguageSwitcherEditor : UnityEditor.Editor
    {
        #region Code
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.LabelField($"This component doesn\'t need to customize.", EditorStyles.boldLabel);
            EditorGUILayout.LabelField($"In dropdown \"On Value Changed\"", EditorStyles.boldLabel);
            EditorGUILayout.LabelField($"you don't need to select events of this script", EditorStyles.boldLabel);
        }
        #endregion
    }
}