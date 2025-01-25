using UnityEngine;
using XNode;

namespace CoreTeamGamesSDK.DialogueSystem.Graph.Nodes
{
    public class DialogueStartNode : Node
    {
        #region Variables
        [Output(ShowBackingValue.Never, ConnectionType.Override)]
        [SerializeField]
        private Object _output;
        #endregion

        #region Getters
        public override object GetValue(NodePort port)
        {
            return this;
        }
        #endregion
    }
}