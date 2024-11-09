using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace CoreTeamGamesSDK.Localization
{
    /// <summary>
    /// The class for Localize a TMP_Text Assets
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("CoreTeam Games SDK/Localization/Localize TextMeshPro Text")]
    [RequireComponent(typeof(TMP_Text))]
    public class LocalizeTMPText : MonoBehaviour
    {
        #region Variables
        #region Private variables
        LocalizationService _textLocalizator;
        [SerializeField] string _lineKey;
        [SerializeField] string _fileName;
        TMP_Text _text;
        #endregion

        #region Public variables
        public List<string> variables;
        #endregion
        #endregion

        #region Properties
        public string lineId
        {
            get => _lineKey;
            set
            {
                if (value != "")
                {
                    _lineKey = value;
                    if (_textLocalizator == null || _textLocalizator.CurrentLanguage.LangLocale == "")
                        return;

                    Localize();
                }
            }
        }
        #endregion

        #region Code
        public void Awake()
        {
            _text = GetComponent<TMP_Text>();
            _textLocalizator = GameObject.FindGameObjectWithTag("LocalizationService").GetComponent<LocalizationService>();

            if (_textLocalizator == null)
            {
                Debug.LogError("Can't find Localization service!");
                return;
            }
            Localize();

            _textLocalizator.onlanguageSelectedEvent += (Language language) => { Localize(); };
        }

        public void Localize()
        {
            if (_textLocalizator.LineExist(_fileName, _lineKey))
            {
                _text.isRightToLeftText = _textLocalizator.CurrentLanguage.IsRTL;

                string _localizedText = _textLocalizator.GetLocalizedLine(_fileName, _lineKey);
                if (variables.Count > 0)
                {
                    for (int i = 0; i < variables.Count; i++)
                    {
                        if (_localizedText.Contains("{" + i + "}"))
                        {
                            _localizedText = _localizedText.Replace("{" + i + "}", variables[i]);
                        }
                    }
                }
                _text.text = _localizedText;
            }
            else
                _text.text = $"Can\'t find the localization line by line key: \"{_lineKey}\"";
        }

        public string ReturnLocalizedLine(string fileName, string lineKey)
        {
            if (_textLocalizator == null || _textLocalizator.CurrentLanguage.LangLocale == "")
                return null;

            return _textLocalizator.GetLocalizedLine(fileName, lineKey);
        }

        private void OnDestroy()
        {
            if (_textLocalizator == null)
                return;

            _textLocalizator.onlanguageSelectedEvent -= (Language language) => { Localize(); };
        }
        #endregion
    }
}