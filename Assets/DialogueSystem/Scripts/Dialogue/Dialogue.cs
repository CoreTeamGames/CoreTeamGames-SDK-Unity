using CoreTeamGamesSDK.DialogueSystem.Graph.Nodes;
using UnityEngine;
using XNode;

namespace CoreTeamGamesSDK.DialogueSystem.Graph
{
    [CreateAssetMenu(menuName = "CoreTeamSDK/Dialogue System/Dialogue")]
    public class Dialogue : NodeGraph
    {
        #region Getters
        public DialogueStartNode GetRootNode()
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i] is DialogueStartNode) return nodes[i] as DialogueStartNode;
            }
            return null;
        }
        #endregion
    }
}