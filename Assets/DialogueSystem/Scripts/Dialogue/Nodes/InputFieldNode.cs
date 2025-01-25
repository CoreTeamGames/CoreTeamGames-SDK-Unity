using CoreTeamGamesSDK.DialogueSystem.Enums;
using UnityEngine;
using XNode;

namespace CoreTeamGamesSDK.DialogueSystem.Graph.Nodes
{
    public class InputFieldNode : Node
    {
        #region Variables
        [Input(ShowBackingValue.Never, ConnectionType.Multiple)]
        [SerializeField]
        private Object _input;

        [Output(ShowBackingValue.Never, ConnectionType.Override)]
        [SerializeField]
        private Object _output;

        [SerializeField] private EContentType _contentType;
        [SerializeField] private string _variableName;
        [SerializeField] private string _descriptionLineKey;
        [SerializeField] private string _placeholderLineKey;
        #endregion

        #region Properties
        public EContentType ContentType => _contentType;
        public string VariableName => _variableName;
        public string DescriptionLineKey => _descriptionLineKey;
        public string PlaceholderLineKey => _placeholderLineKey;
        public string DescriptionLine { get; set; }
        public string PlaceholderLine { get; set; }
        #endregion
    }
}