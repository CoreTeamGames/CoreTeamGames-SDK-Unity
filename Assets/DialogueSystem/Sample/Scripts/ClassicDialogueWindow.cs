using CoreTeamGamesSDK.DialogueSystem.Graph.Nodes;
using CoreTeamGamesSDK.DialogueSystem;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using XNode;

[RequireComponent(typeof(CanvasGroup))]
public class ClassicDialogueWindow : MonoBehaviour, IDialogueWindow
{
    InputFieldNode _currentNode;
    private CanvasGroup _group;
    private DialogueStep _currentDialogueStep;
    private bool _isOutput = false;
    private float _nextTimeToOutputSymbol;
    private float _currentOutputSpeed;
    [SerializeField] private float _outputSpeed;
    [SerializeField] private TMP_Text _textBox;
    [SerializeField] private TMP_Text _nameBox;
    [SerializeField] private AudioSource _source;
    [SerializeField] private RectTransform _choiseButtonsRoot;
    [SerializeField] private Image _firstCharacterPortrait;
    [SerializeField] private Image _secondCharacterPortrait;

    [Header("Buttons Set-Up")]
    [SerializeField] private Button _choiseButtonPrefab;

    [Header("InputField Set-Up")]
    [SerializeField] private CanvasGroup _inputFieldCanvasGroup;
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private TMP_Text _inputFieldPlaceHolderText;
    [SerializeField] private TMP_Text _inputFieldDescriptionText;

    [Header("Events")]
    [SerializeField] private ChoiseSelectedEvent _onChoiseSelected;
    [SerializeField] private InputFieldSubmitEvent _inputFieldSubmitEvent;
    [SerializeField] private UnityEvent _onDialogueWindowStartShow;
    [SerializeField] private UnityEvent _onDialogueWindowShows;
    [SerializeField] private UnityEvent _onDialogueWindowStartsHide;
    [SerializeField] private UnityEvent _onDialogueWindowHides;

    public bool IsOutput => _isOutput;
    public float OutputSpeed => _outputSpeed;
    public Image FirstCharacterPortrait => _firstCharacterPortrait;
    public Image SecondCharacterPortrait => _secondCharacterPortrait;
    public DialogueStep CurrentDialogueStep => _currentDialogueStep;
    public UnityEvent OnDialogueWindowStartShow => _onDialogueWindowStartShow;
    public UnityEvent OnDialogueWindowShows => _onDialogueWindowShows;
    public UnityEvent OnDialogueWindowStartsHide => _onDialogueWindowStartsHide;
    public UnityEvent OnDialogueWindowHides => _onDialogueWindowHides;
    public ChoiseSelectedEvent OnChoiseSelected => _onChoiseSelected;
    public InputFieldSubmitEvent InputFieldSubmit => _inputFieldSubmitEvent;

    private void Awake()
    {
        _group = GetComponent<CanvasGroup>();
        _onChoiseSelected = new ChoiseSelectedEvent();
        _inputFieldSubmitEvent = new InputFieldSubmitEvent();

        if (_inputField != null)
            _inputField.onSubmit.AddListener(OnInputSubmit);
    }

    private void OnDestroy()
    {
        if (_inputField != null)
            _inputField.onSubmit.RemoveListener(OnInputSubmit);
    }

    public void HideDialogueWindow()
    {
        _onDialogueWindowStartsHide.Invoke();
        _group.alpha = 0;
        _group.interactable = false;
        _onDialogueWindowHides.Invoke();
    }

    public void OnDialogueChoiseSelected(Node nextNode)
    {
        _onChoiseSelected.Invoke(nextNode);
        ClearChoises();
    }

    public void ShowDialogueChoises(Dictionary<string, Node> choises)
    {
        ClearChoises();
        _nameBox.text = "";
        _textBox.text = "";
        foreach (var item in choises)
        {
            Button _currentChoiseButton = Instantiate(_choiseButtonPrefab, _choiseButtonsRoot);

            _currentChoiseButton.gameObject.SetActive(true);
            _currentChoiseButton.onClick.AddListener(() => { OnDialogueChoiseSelected(item.Value); });

            TMP_Text _currentChoiseButtonText = _currentChoiseButton.GetComponentInChildren<TMP_Text>();
            _currentChoiseButtonText.text = item.Key;
        }
    }

    public void ShowDialogueWindow()
    {
        ClearChoises();
        _onDialogueWindowStartShow.Invoke();
        _group.alpha = 1;
        _group.interactable = true;
        _onDialogueWindowShows.Invoke();
    }

    public void SkipLineOutput()
    {
        if (_currentDialogueStep == null)
            return;

        StopAllCoroutines();

        _isOutput = false;
        _textBox.text = _currentDialogueStep.Line;
    }

    public void StartLineOutput(DialogueStep step)
    {
        if (step == null)
            return;

        _currentOutputSpeed = step.OverrideOutputSpeed ? step.OverridenOutputSpeed : OutputSpeed;
        _currentDialogueStep = step;
        _textBox.text = "";
        _nameBox.text = step.CharacterName;
        if (step.IsLeft)
        {
            _firstCharacterPortrait.sprite = step.Emotion;
        }
        else
        {
            _secondCharacterPortrait.sprite = step.Emotion;
        }

        if (step.OverrideOutputSpeed && step.OverridenOutputSpeed == 0f)
            _textBox.text = step.Line;
        else
            StartCoroutine(LineOutput(step));
    }

    private IEnumerator LineOutput(DialogueStep step)
    {
        _isOutput = true;

        _nextTimeToOutputSymbol = Time.time;

        for (int i = 0; i < step.Line.Length; i++)
        {
            if (step.Line[i] == '<')
            {
                for (int f = i; f < step.Line.Length; f++)
                {
                    _textBox.text += step.Line[f];
                    if (step.Line[f] == '>')
                    {
                        i = f;
                        break;
                    }
                }
                continue;
            }
            else
            {
                _textBox.text += step.Line[i];

                if (_source != null && step.Voice != null)
                    _source.PlayOneShot(step.Voice);

                _nextTimeToOutputSymbol += _currentOutputSpeed;

                while (Time.time < _nextTimeToOutputSymbol)
                {
                    yield return null;
                }
            }
        }

        _isOutput = false;
    }
    public void ClearChoises()
    {
        for (int i = 0; i < _choiseButtonsRoot.childCount; i++)
        {
            if (!_choiseButtonsRoot.GetChild(i).gameObject.activeInHierarchy)
                continue;

            Destroy(_choiseButtonsRoot.GetChild(i).gameObject);
        }
    }

    public void ShowInputField(InputFieldNode node)
    {
        _nameBox.text = "";
        _textBox.text = "";

        _inputFieldCanvasGroup.alpha = 1;
        _inputFieldCanvasGroup.interactable = true;
        _inputFieldCanvasGroup.blocksRaycasts = true;

        _currentNode = node;
        _inputFieldDescriptionText.text = node.DescriptionLine;
        _inputFieldPlaceHolderText.text = node.PlaceholderLine;

        switch (node.ContentType)
        {
            case CoreTeamGamesSDK.DialogueSystem.Enums.EContentType.Standard:
                _inputField.contentType = TMP_InputField.ContentType.Standard;
                break;

            case CoreTeamGamesSDK.DialogueSystem.Enums.EContentType.IntegerNumber:
                _inputField.contentType = TMP_InputField.ContentType.IntegerNumber;
                break;

            case CoreTeamGamesSDK.DialogueSystem.Enums.EContentType.DecimalNumber:
                _inputField.contentType = TMP_InputField.ContentType.DecimalNumber;
                break;

            case CoreTeamGamesSDK.DialogueSystem.Enums.EContentType.Alphanumeric:
                _inputField.contentType = TMP_InputField.ContentType.Alphanumeric;
                break;

            case CoreTeamGamesSDK.DialogueSystem.Enums.EContentType.Name:
                _inputField.contentType = TMP_InputField.ContentType.Name;
                break;

            case CoreTeamGamesSDK.DialogueSystem.Enums.EContentType.Password:
                _inputField.contentType = TMP_InputField.ContentType.Password;
                break;

            case CoreTeamGamesSDK.DialogueSystem.Enums.EContentType.Pin:
                _inputField.contentType = TMP_InputField.ContentType.Pin;
                break;

            default:
                _inputField.contentType = TMP_InputField.ContentType.Standard;
                break;
        }
    }

    public void OnInputSubmit(string content)
    {
        if (_currentNode == null)
            return;

        _inputFieldSubmitEvent?.Invoke(_currentNode, content);
    }

    public void HideInputField()
    {
        _inputFieldCanvasGroup.alpha = 0;
        _inputFieldCanvasGroup.interactable = false;
        _inputFieldCanvasGroup.blocksRaycasts = false;
        _inputField.text = "";

        _currentNode = null;
    }
}