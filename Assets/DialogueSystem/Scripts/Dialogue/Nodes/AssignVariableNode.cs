using CoreTeamGamesSDK.Other.VariableStorage;
using UnityEngine;
using XNode;

namespace CoreTeamGamesSDK.DialogueSystem.Graph.Nodes
{
    public class AssignVariableNode : Node
    {
        #region Variables
        [Input(ShowBackingValue.Never, ConnectionType.Multiple)]
        [SerializeField]
        private Object _input;

        [Output(ShowBackingValue.Never, ConnectionType.Override)]
        [SerializeField]
        private Object _output;

        [SerializeField] private Variable _variable;
        #endregion

        #region Properties
        public Variable Variable => _variable;
        #endregion
    }
}