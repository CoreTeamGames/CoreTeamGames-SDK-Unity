using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace CoreTeamGamesSDK.Other.VariableStorage
{
    [CustomEditor(typeof(VariablesStorage))]
    public class VariableStoragesEditor : Editor
    {
        private SerializedProperty _property;
        private ReorderableList _list;

        private void OnEnable()
        {
            _property = serializedObject.FindProperty("_variables");

            _list = new ReorderableList(serializedObject, _property, true, true, true, true)
            {
                drawHeaderCallback = DrawListHeader,
                drawElementCallback = DrawListElement,
                elementHeight = EditorGUIUtility.singleLineHeight * 3 + 4,
            };
        }

        private void DrawListHeader(Rect rect)
        {
            EditorGUI.LabelField(rect, "Variables");
        }

        private void DrawListElement(Rect rect, int index, bool isActive, bool isFocused)
        {
            rect.y += 2;
            var item = _property.GetArrayElementAtIndex(index);

            item.FindPropertyRelative("_variableName").stringValue = EditorGUI.TextField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), "Variable Name", item.FindPropertyRelative("_variableName").stringValue);
            EVariableType _varType = (EVariableType)EditorGUI.EnumPopup(new Rect(rect.x, rect.y + EditorGUIUtility.singleLineHeight, rect.width, EditorGUIUtility.singleLineHeight), "Variable Type", (EVariableType)item.FindPropertyRelative("_variableType").enumValueIndex);
            item.FindPropertyRelative("_variableType").enumValueIndex = ((int)_varType);

            Rect _varValueRect = new Rect(rect.x, rect.y + EditorGUIUtility.singleLineHeight * 2, rect.width, EditorGUIUtility.singleLineHeight);
            switch ((EVariableType)item.FindPropertyRelative("_variableType").enumValueIndex)
            {
                case EVariableType.Integer:
                    item.FindPropertyRelative("integerValue").intValue = EditorGUI.IntField(_varValueRect, "Variable Value", item.FindPropertyRelative("integerValue").intValue);
                    break;

                case EVariableType.Float:
                    item.FindPropertyRelative("floatValue").floatValue = EditorGUI.FloatField(_varValueRect, "Variable Value", item.FindPropertyRelative("floatValue").floatValue);
                    break;

                case EVariableType.Boolean:
                    item.FindPropertyRelative("boolValue").boolValue = EditorGUI.Toggle(_varValueRect, "Variable Value", item.FindPropertyRelative("boolValue").boolValue);
                    break;

                case EVariableType.String:
                    item.FindPropertyRelative("stringValue").stringValue = EditorGUI.TextField(_varValueRect, "Variable Value", item.FindPropertyRelative("stringValue").stringValue);
                    break;

                case EVariableType.Object:
                // Исправить ошибку
                    item.FindPropertyRelative("objectValue").objectReferenceValue = EditorGUI.ObjectField(_varValueRect, "Variable Value", item.FindPropertyRelative("objectValue").objectReferenceValue, typeof(Object), true);
                    break;

                case EVariableType.EnumIndex:
                    item.FindPropertyRelative("enumIndexValue").intValue = EditorGUI.IntField(_varValueRect, "Variable Value", item.FindPropertyRelative("enumIndexValue").intValue);
                    break;

                default:
                    EditorGUI.LabelField(_varValueRect, $"Variable with type {'"'}{(EVariableType)item.FindPropertyRelative("_variableType").enumValueIndex}{'"'} doesn{"'"}t support value! Add supporting of this type");
                    break;
            }
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.Space();
            _list.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }
    }
}