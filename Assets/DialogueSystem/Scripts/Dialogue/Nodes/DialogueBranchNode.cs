using UnityEngine;
using XNode;

namespace CoreTeamGamesSDK.DialogueSystem.Graph.Nodes
{
    public class DialogueBranchNode : Node
    {
        #region Variables
        [Input(ShowBackingValue.Never, ConnectionType.Multiple)]
        [SerializeField]
        private Object _input;

        [Output(ShowBackingValue.Never, ConnectionType.Override)]
        [SerializeField]
        private Object _output;

        [SerializeField] private DialogueStep _step;
        #endregion

        #region Properties
        public DialogueStep DialogueStep => _step;
        #endregion

        #region Getters
        public override object GetValue(NodePort port)
        {
            return this;
        }
        #endregion
    }
}