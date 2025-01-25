using UnityEngine;
using XNode;

namespace CoreTeamGamesSDK.DialogueSystem.Graph.Nodes
{
    public class CommentaryNode : Node
    {
        #region Variables
        [SerializeField]
        [TextArea(1, 5)]
        private string _commentary;
        #endregion
    }
}