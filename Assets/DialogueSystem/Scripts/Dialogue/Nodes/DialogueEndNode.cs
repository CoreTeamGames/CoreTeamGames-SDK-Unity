using UnityEngine;
using XNode;

namespace CoreTeamGamesSDK.DialogueSystem.Graph.Nodes
{
    public class DialogueEndNode : Node
    {
        #region Variables
        [Input(ShowBackingValue.Never, ConnectionType.Multiple)]
        [SerializeField]
        private Object _input;
        #endregion

        #region Getters
        public override object GetValue(NodePort port)
        {
            return this;
        }
        #endregion
    }
}