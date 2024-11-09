using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace CoreTeamGamesSDK.Localization
{
    /// <summary>
    /// The script for switch language in game with using TMP_Dropdown
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("CoreTeam Games SDK/Localization/TextMeshPro Dropdown Language Switcher")]
    [RequireComponent(typeof(TMP_Dropdown))]
    public class TMPDropdownLanguageSwitcher : MonoBehaviour
    {
        #region Variables
        private TMP_Dropdown _dropdown;
        private LocalizationService _service;
        private int _currentLanguageIndex;
        #endregion

        #region Code
        private void Awake()
        {
            _dropdown = GetComponent<TMP_Dropdown>();

            if (GameObject.FindGameObjectWithTag("LocalizationService") != null)
            {
                if (GameObject.FindGameObjectWithTag("LocalizationService").TryGetComponent<LocalizationService>(out LocalizationService service))
                {
                    _service = service;
                }
                else
                {
                    Debug.LogError($"Can\'t find LocalizationService script in gameObject with tag \"LocalizationService\"!");
                    return;
                }
            }
            else
            {
                Debug.LogError($"Can\'t find LocalizationService object with tag \"LocalizationService\"!");
                return;
            }

            _service.onlanguageSelectedEvent += OnLanguageChanged;
            _dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
        }

        private void Start()
        {
            SetupDropdown();
        }

        private void OnLanguageChanged(Language language)
        {
            for (int i = 0; i < _service.Languages.Count; i++)
            {
                if (_service.CurrentLocalizator.Languages[i] == _service.CurrentLocalizator.CurrentLanguage)
                    _currentLanguageIndex = i;
            }
            _dropdown.SetValueWithoutNotify(_currentLanguageIndex);
        }

        public void SetupDropdown()
        {
            if (_service == null)
                return;

            List<string> _languageNames = new List<string>();

            for (int i = 0; i < _service.CurrentLocalizator.Languages.Count; i++)
            {
                _languageNames.Add(_service.CurrentLocalizator.Languages[i].LangName);
                if (_service.CurrentLocalizator.Languages[i] == _service.CurrentLocalizator.CurrentLanguage)
                    _currentLanguageIndex = i;
            }

            _dropdown.ClearOptions();
            _dropdown.AddOptions(_languageNames);
            _dropdown.SetValueWithoutNotify(_currentLanguageIndex);
        }

        private void OnDropdownValueChanged(int value)
        {
            if (_service == null)
                return;

            _service.ChangeLanguage(_service.CurrentLocalizator.Languages[value]);
        }

        private void OnDestroy()
        {
            _dropdown.onValueChanged.RemoveListener(OnDropdownValueChanged);
        }
        #endregion
    }
}