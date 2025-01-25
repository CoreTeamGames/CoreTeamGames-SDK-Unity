using CoreTeamGamesSDK.DialogueSystem.Graph.Nodes;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;
using XNode;

namespace CoreTeamGamesSDK.DialogueSystem
{
    public interface IDialogueWindow
    {
        #region Events
        UnityEvent OnDialogueWindowStartShow { get; }
        UnityEvent OnDialogueWindowShows { get; }
        UnityEvent OnDialogueWindowStartsHide { get; }
        UnityEvent OnDialogueWindowHides { get; }
        InputFieldSubmitEvent InputFieldSubmit { get; }
        ChoiseSelectedEvent OnChoiseSelected { get; }
        #endregion

        #region Properties
        DialogueStep CurrentDialogueStep { get; }
        bool IsOutput { get; }
        float OutputSpeed { get; }
        Image FirstCharacterPortrait { get; }
        Image SecondCharacterPortrait { get; }
        #endregion

        #region Methods
        void ShowDialogueWindow();
        void HideDialogueWindow();
        void StartLineOutput(DialogueStep step);
        void SkipLineOutput();
        void ShowDialogueChoises(Dictionary<string, Node> choises);
        void OnDialogueChoiseSelected(Node nextNode);
        void ShowInputField(InputFieldNode node);
        void HideInputField();
        void OnInputSubmit(string content);
        #endregion
    }

    public class ChoiseSelectedEvent : UnityEvent<Node> { }
    public class InputFieldSubmitEvent : UnityEvent<InputFieldNode, string> { }
}