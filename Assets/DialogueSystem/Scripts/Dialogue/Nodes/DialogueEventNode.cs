using UnityEngine;
using XNode;

namespace CoreTeamGamesSDK.DialogueSystem.Graph.Nodes
{
    public class DialogueEventNode : Node
    {
        #region Variables
        [Input(ShowBackingValue.Never, ConnectionType.Multiple)]
        [SerializeField]
        private Object _input;

        [Output(ShowBackingValue.Never, ConnectionType.Override)]
        [SerializeField]
        private Object _output;

        [SerializeField] private string _eventName;
        #endregion

        #region Properties
        public string EventName => _eventName;
        #endregion
    }
}