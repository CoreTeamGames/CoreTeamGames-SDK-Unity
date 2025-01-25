using UnityEngine;
using XNode;

namespace CoreTeamGamesSDK.DialogueSystem.Graph.Nodes
{
    public class ChoiseBranchNode : Node
    {
        #region Variables
        [Input(ShowBackingValue.Never, ConnectionType.Multiple)]
        [SerializeField]
        private Object _input;

        [Output(ShowBackingValue.Never, ConnectionType.Override, dynamicPortList = true)]
        [SerializeField]
        private string[] _output;
        private string[] _fallbackOutputLines;
        #endregion

        #region Properties
        public string[] ChoiseKeys => _output;
        #endregion
    }
}


// [CustomNodeEditor(typeof(ChoiseBranchNode))]
// public class ChoiseBranchNodeEditor : NodeEditor
// {
//     private ReorderableList _list;
//     SerializedProperty _output;

//     private void OnEnable()
//     {
//         _output = serializedObject.FindProperty("_output");
//         _list = new ReorderableList(serializedObject,_output);

//         _list.drawHeaderCallback =
//             (Rect rect) =>
//             {
//                 EditorGUI.LabelField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), $"Localize TMP_Text group members");
//             };
//             _list.drawElementCallback =
//             (Rect rect, int index, bool isActive, bool isFocused) =>
//             {
//                 var element = _list.serializedProperty.GetArrayElementAtIndex(index);
//                 rect.y += 2;
//             };
//     }

//     public override void OnBodyGUI()
//     {
//         _list.DoLayoutList();
//         // Update serialized object's representation
//         serializedObject.Update();

//         // Apply property modifications
//         serializedObject.ApplyModifiedProperties();

//     }
// }