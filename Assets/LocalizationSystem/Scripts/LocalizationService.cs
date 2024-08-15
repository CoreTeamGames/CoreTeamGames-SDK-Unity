using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace CoreTeamGamesSDK.Localization
{
    /// <summary>
    /// The in-game service for get localization
    /// </summary>
    [AddComponentMenu("CoreTeamGames/Localization/Localization Service")]
    [System.Serializable]
    public class LocalizationService : MonoBehaviour
    {
        #region Variables
        [SerializeReference] ILocalizator _standardLocalizator;
        [SerializeReference] ILocalizator _currentLocalizator;
        [SerializeReference] ILocalizator _fallbackLocalizator;
        #endregion

        #region Properties
        public Language CurrentLanguage { get => _currentLocalizator.CurrentLanguage; private set => _currentLocalizator.SwitchLanguage(value); }
        public ILocalizator StandardLocalizator => _standardLocalizator;
        public ILocalizator CurrentLocalizator { get => _currentLocalizator; }
        public ILocalizator FallbackLocalizator => _fallbackLocalizator;
        public List<Language> Languages { get => _currentLocalizator.Languages; }
        #endregion

        #region Events
        public delegate void onlanguageSelected(Language language);
        public onlanguageSelected onlanguageSelectedEvent;
        #endregion

        #region Code
        public void Awake() => Initialize();

        public void ChangeLanguage(Language language)
        {
            _currentLocalizator.SwitchLanguage(language);
            onlanguageSelectedEvent?.Invoke(CurrentLanguage);
            Debug.Log($"Set language to: \"{_currentLocalizator.CurrentLanguage.LangName}\"; \"{_currentLocalizator.CurrentLanguage.LangLocale}\"");
        }

        public void Initialize()
        {
            _standardLocalizator = new ClassicLocalizator();
            _fallbackLocalizator = new FallBackLocalizator();

            _currentLocalizator = _standardLocalizator;
            _currentLocalizator.Initialize();
            if (_currentLocalizator.CurrentLanguage == null)
            {
                _currentLocalizator = _fallbackLocalizator;
                _currentLocalizator.Initialize();
            }

            if (SearchlanguageByLocale(CultureInfo.CurrentCulture.ToString().ToLower()) != null)
                CurrentLanguage = SearchlanguageByLocale(CultureInfo.CurrentCulture.ToString().ToLower());
            else if (SearchlanguageByLocale("en-us") != null)
                CurrentLanguage = SearchlanguageByLocale("en-us");
            else CurrentLanguage = Languages[0];
            Debug.Log($"Set language to: \"{_currentLocalizator.CurrentLanguage.LangName}\"; \"{_currentLocalizator.CurrentLanguage.LangLocale}\"");
        }
        public void Start()
        {
            onlanguageSelectedEvent?.Invoke(CurrentLanguage);
        }

        #region Get Text
        //Get localized lines
        public string GetLocalizedLine(string fileName, string lineKey)
        {
            if (_currentLocalizator == null)
                return null;
            return _currentLocalizator.GetLocalizedLine(fileName, lineKey);
        }

        public Dictionary<string, string> GetLocalizedLines(string fileName, string[] lineKeys)
        {
            if (_currentLocalizator == null)
                return null;
            return _currentLocalizator.GetLocalizedLines(fileName, lineKeys);
        }

        //Get localized files
        public Dictionary<string, string> GetLocalizedFile(string fileName)
        {
            if (_currentLocalizator == null)
                return null;
            return _currentLocalizator.GetLocalizedTextFile(fileName);
        }
        #endregion

        #region Get Language
        public Language SearchlanguageByLocale(string _languageCode)
        {
            _languageCode = _languageCode.ToLower();
            foreach (var language in Languages)
            {
                if (language.LangLocale.ToLower() == _languageCode)
                    return language;
            }
            Debug.LogError($"Can't find language with locale \"{_languageCode}\"!");
            return null;
        }
        public List<Language> GetAllLanguages(string[] _path)
        {
            if (_currentLocalizator == null)
                return null;
            return _currentLocalizator.GetAllLanguages();
        }
        #endregion
        public bool LineExist(string fileName, string lineKey)
        {
            if (_currentLocalizator == null)
                return false;
            return _currentLocalizator.LineExist(fileName, lineKey);
        }

        #region Get Audio
        public AudioClip GetLocalizedAudioClip(string clipName)
        {
            if (_currentLocalizator == null)
                return null;
            return _currentLocalizator.GetLocalizedAudioClip(clipName);
        }
        #endregion
        #endregion
    }
}