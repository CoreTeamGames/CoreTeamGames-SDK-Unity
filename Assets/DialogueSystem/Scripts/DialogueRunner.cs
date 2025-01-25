using CoreTeamGamesSDK.DialogueSystem.Graph.Nodes;
using CoreTeamGamesSDK.Other.VariableStorage;
using CoreTeamGamesSDK.DialogueSystem.Graph;
using System.Text.RegularExpressions;
using CoreTeamGamesSDK.Localization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using XNode;

namespace CoreTeamGamesSDK.DialogueSystem
{
    public class DialogueRunner : MonoBehaviour
    {
        #region Variables
        [SerializeField] private LocalizationService _localizationService;
        [SerializeField] private Dialogue _currentDialogue;
        [SerializeField] private string _namesFileName = "Names";
        [SerializeField] private Node _currentNode;
        [SerializeField] private Node _nextNode;
        [SerializeField] private VariablesStorage _variables;

        private bool _isChoiseBranch = false;
        private bool _isInputField = false;
        private DialogueReader _reader;
        private IDialogueWindow _window;
        private bool _isDialogueRunning;
        #endregion

        #region Events
        public delegate void OnDialogueEventTriggered(string eventName);
        public OnDialogueEventTriggered OnDialogueEventTriggeredEvent;
        #endregion

        #region Code
        private void Awake()
        {
            if (!FindObjectOfType<LocalizationService>())
                return;

            _localizationService = FindObjectOfType<LocalizationService>();
            _localizationService.onlanguageSelectedEvent += (Language l) => { OnLanguageChanged(); };


            if (GetComponent<IDialogueWindow>() == null)
                return;

            _window = GetComponent<IDialogueWindow>();
        }

        private void OnLanguageChanged()
        {
            if (!_isDialogueRunning)
                return;

            if (_currentDialogue == null)
                return;

            if (_currentNode == null)
                return;

            _reader = new DialogueReader(_currentDialogue.name, _namesFileName, _localizationService);

            RewriteLine();
        }

        private void Start()
        {
            if (_window == null)
                return;

            _window.OnChoiseSelected.AddListener(OnChoise);
            _window.InputFieldSubmit.AddListener(OnInputSubmit);
        }

        private void OnDestroy()
        {
            if (_window == null)
                return;

            _window.OnChoiseSelected.RemoveListener(OnChoise);
            _window.InputFieldSubmit.RemoveListener(OnInputSubmit);

            if (_localizationService == null)
                return;

            _localizationService.onlanguageSelectedEvent -= (Language l) => { OnLanguageChanged(); };
        }

        private void OnChoise(Node node)
        {
            if (node == null)
                EndDialogue();

            _isChoiseBranch = false;
            _nextNode = node;
            DialogueStep();
        }

        public void StartDialogue(Dialogue dialogue)
        {
            if (dialogue == null)
                return;

            if (_localizationService == null)
                return;

            if (_window == null)
                return;

            if (_isDialogueRunning)
                return;

            _reader = new DialogueReader(dialogue.name, _namesFileName, _localizationService);

            _currentDialogue = dialogue;
            _window.ShowDialogueWindow();
            _isDialogueRunning = true;
            _currentNode = _currentDialogue.GetRootNode();
            _nextNode = null;
            DialogueStep();
        }

        public void EndDialogue()
        {
            _currentNode = null;
            _nextNode = null;
            _currentDialogue = null;
            _isDialogueRunning = false;
            _window.HideDialogueWindow();
        }

        public void DialogueStep()
        {
            if (_window == null)
                return;

            if (!_isDialogueRunning || _currentDialogue == null)
                return;

            if (_currentNode == null)
            {
                EndDialogue();
                return;
            }

            if (_isChoiseBranch || _isInputField)
                return;

            if (_currentNode.GetType() == typeof(DialogueBranchNode))
            {
                if ((_currentNode as DialogueBranchNode).DialogueStep.CanSkipOutput && _window.IsOutput)
                {
                    _window.SkipLineOutput();
                    return;
                }
                else if(_window.IsOutput)
                return;
            }

            if (_nextNode != null)
            {
                if (_currentNode != _nextNode)
                    _currentNode = _nextNode;
            }

            NodePort[] _ports = _currentNode.Outputs.ToArray();

            switch (_currentNode.name.ToLower())
            {
                case "dialogue start":
                    if (_ports.Length == 0)
                    {
                        EndDialogue();
                        return;
                    }
                    _currentNode = _ports[0].Connection.node;
                    DialogueStep();
                    break;

                case "dialogue branch":
                    string _name = "";
                    string _line = "";

                    string _lineKey = (_currentNode as DialogueBranchNode).DialogueStep.LineKey;
                    string _nameKey = (_currentNode as DialogueBranchNode).DialogueStep.CharacterNameKey;

                    if (_reader.LocalizedDialogue.ContainsKey(_lineKey.ToLower()))
                        _line = _reader.LocalizedDialogue[_lineKey.ToLower()];

                    if (_reader.LocalizedNames.ContainsKey(_nameKey.ToLower()))
                        _name = _reader.LocalizedNames[_nameKey.ToLower()];

                    if (_line != "")
                    {
                        _line = InsertVariables(_line);
                    }

                    DialogueStep _step = new DialogueStep();

                    _step = (_currentNode as DialogueBranchNode).DialogueStep;
                    _step.Line = _line;
                    _step.CharacterName = _name;

                    _window.StartLineOutput(_step);

                    if (_ports.Length == 0)
                    {
                        _nextNode = null;
                        return;
                    }

                    _nextNode = _ports[0].Connection.node;
                    break;

                case "choise branch":

                    Dictionary<string, Node> _choises = new Dictionary<string, Node>();
                    for (int i = 0; i < (_currentNode as ChoiseBranchNode).ChoiseKeys.Length; i++)
                    {
                        if (_currentNode.GetOutputPort($"_output {i}").Connection == null)
                            continue;

                        _line = _reader.LocalizedDialogue[(_currentNode as ChoiseBranchNode).ChoiseKeys[i].ToLower()];

                        _choises.Add(_line, _currentNode.GetOutputPort($"_output {i}").Connection.node);
                    }

                    if (_choises.Count == 0)
                    {
                        EndDialogue();
                        return;
                    }

                    _isChoiseBranch = true;
                    _nextNode = null;
                    _window.ShowDialogueChoises(_choises);
                    break;

                case "dialogue end":
                    EndDialogue();
                    break;

                case "dialogue event":
                    if ((_currentNode as DialogueEventNode).EventName != "")
                        OnDialogueEventTriggeredEvent?.Invoke((_currentNode as DialogueEventNode).EventName);

                    if (_ports.Length == 0)
                    {
                        EndDialogue();
                        return;
                    }
                    _currentNode = _ports[0].Connection.node;
                    _nextNode = null;
                    DialogueStep();
                    break;

                case "assign variable":
                    AddVariable((_currentNode as AssignVariableNode).Variable);

                    if (_ports.Length == 0)
                    {
                        EndDialogue();
                        return;
                    }

                    _currentNode = _ports[0].Connection.node;
                    _nextNode = null;
                    DialogueStep();
                    break;

                case "edit variable":
                    EditVariable((_currentNode as EditVariableNode).Variable.VariableName, (_currentNode as EditVariableNode).Variable);

                    if (_ports.Length == 0)
                    {
                        EndDialogue();
                        return;
                    }

                    _currentNode = _ports[0].Connection.node;
                    _nextNode = null;
                    DialogueStep();
                    break;

                case "input field":
                    _isInputField = true;

                    string _descriptionLine = "";
                    string _placeholderLine = "";

                    string _descriptionLineKey = (_currentNode as InputFieldNode).DescriptionLineKey;
                    string _placeholderLineKey = (_currentNode as InputFieldNode).PlaceholderLineKey;

                    if (_reader.LocalizedDialogue.ContainsKey(_descriptionLineKey.ToLower()))
                        _descriptionLine = _reader.LocalizedDialogue[_descriptionLineKey.ToLower()];

                    if (_reader.LocalizedDialogue.ContainsKey(_placeholderLineKey.ToLower()))
                        _placeholderLine = _reader.LocalizedDialogue[_placeholderLineKey.ToLower()];

                    if (_descriptionLine != "")
                    {
                        _descriptionLine = InsertVariables(_descriptionLine);
                    }

                    if (_placeholderLine != "")
                    {
                        _placeholderLine = InsertVariables(_placeholderLine);
                    }

                    (_currentNode as InputFieldNode).DescriptionLine = _descriptionLine;
                    (_currentNode as InputFieldNode).PlaceholderLine = _placeholderLine;

                    _window.ShowInputField((_currentNode as InputFieldNode));

                    if (_ports.Length == 0)
                    {
                        _nextNode = null;
                        return;
                    }

                    _nextNode = _ports[0].Connection.node;
                    break;
            }
        }

        public void RewriteLine()
        {
            switch (_currentNode.name.ToLower())
            {
                case "dialogue branch":
                    string _name = "";
                    string _line = "";

                    string _lineKey = (_currentNode as DialogueBranchNode).DialogueStep.LineKey;
                    string _nameKey = (_currentNode as DialogueBranchNode).DialogueStep.CharacterNameKey;

                    if (_reader.LocalizedDialogue.ContainsKey(_lineKey.ToLower()))
                        _line = _reader.LocalizedDialogue[_lineKey.ToLower()];

                    if (_reader.LocalizedNames.ContainsKey(_nameKey.ToLower()))
                        _name = _reader.LocalizedNames[_nameKey.ToLower()];

                    if (_variables.Count > 0 && _line != "")
                    {
                        _line = InsertVariables(_line);
                    }

                    DialogueStep _step = new DialogueStep();

                    _step = (_currentNode as DialogueBranchNode).DialogueStep;
                    _step.Line = _line;
                    _step.CharacterName = _name;

                    _window.SkipLineOutput();
                    _window.StartLineOutput(_step);
                    break;

                case "choise branch":
                    Dictionary<string, Node> _choises = new Dictionary<string, Node>();
                    for (int i = 0; i < (_currentNode as ChoiseBranchNode).ChoiseKeys.Length; i++)
                    {
                        if (_currentNode.GetOutputPort($"_output {i}").Connection == null)
                            continue;

                        _line = _reader.LocalizedDialogue[(_currentNode as ChoiseBranchNode).ChoiseKeys[i].ToLower()];

                        _choises.Add(_line, _currentNode.GetOutputPort($"_output {i}").Connection.node);
                    }

                    if (_choises.Count == 0)
                    {
                        return;
                    }

                    _window.ShowDialogueChoises(_choises);
                    break;

                case "input field":

                    string _descriptionLine = "";
                    string _placeholderLine = "";

                    string _descriptionLineKey = (_currentNode as InputFieldNode).DescriptionLineKey;
                    string _placeholderLineKey = (_currentNode as InputFieldNode).PlaceholderLineKey;

                    if (_reader.LocalizedDialogue.ContainsKey(_descriptionLineKey.ToLower()))
                        _descriptionLine = _reader.LocalizedDialogue[_descriptionLineKey.ToLower()];

                    if (_reader.LocalizedDialogue.ContainsKey(_placeholderLineKey.ToLower()))
                        _placeholderLine = _reader.LocalizedDialogue[_placeholderLineKey.ToLower()];

                    if (_descriptionLine != "")
                    {
                        _descriptionLine = InsertVariables(_descriptionLine);
                    }

                    if (_placeholderLine != "")
                    {
                        _placeholderLine = InsertVariables(_placeholderLine);
                    }

                    (_currentNode as InputFieldNode).DescriptionLine = _descriptionLine;
                    (_currentNode as InputFieldNode).PlaceholderLine = _placeholderLine;

                    _window.ShowInputField((_currentNode as InputFieldNode));
                    break;

                default:
                    return;
            }
        }

        private string InsertVariables(string line)
        {
            var resultBuilder = new StringBuilder(line);

            var regex = new Regex(@"\{(\w+)\}");

            return regex.Replace(line, match =>
            {
                string key = match.Groups[1].Value;

                if (_variables.VariableExist(key))
                {
                    return _variables.GetVariable(key).GetVariableValue().ToString();
                }
                return "";
            });
        }

        private void OnInputSubmit(InputFieldNode node, string content)
        {
            _isInputField = false;

            if (node.VariableName != null)
            {
                if (_variables.VariableExist(node.VariableName))
                {
                    if (_variables.GetVariable(node.VariableName).VariableType == EVariableType.String)
                    {
                        _variables.GetVariable(node.VariableName).stringValue = content;
                    }
                }
            }
            _window.HideInputField();

            DialogueStep();
        }

        private void AddVariable(Variable variable)
        {
            if (variable.VariableName == null)
            {
                Debug.LogError("VariableName is null!");
            }
            else
            {
                _variables.AddVariable(variable);
            }
        }

        private void EditVariable(string variableName, object variableValue)
        {
            if (variableName == null)
            {
                Debug.LogError("VariableName is null!");
            }
            else if (!_variables.VariableExist(variableName))
            {
                _variables.GetVariable(variableName).SetVariableValue(variableValue);
            }
        }
        #endregion
    }
}