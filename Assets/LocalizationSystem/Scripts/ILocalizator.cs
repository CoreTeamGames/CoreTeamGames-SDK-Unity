using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace CoreTeamGamesSDK.Localization
{
    /// <summary>
    /// The base interface for localizators
    /// </summary>
    public interface ILocalizator
    {
        LocalizatorSettings Settings { get; }
        Language CurrentLanguage { get; }
        List<Language> Languages { get; }

        #region Methods
        void Initialize();

        void SwitchLanguage(Language language);
        List<Language> GetAllLanguages();
        bool LanguageExist(Language language);

        Dictionary<string, string> GetLocalizedTextFile(string fileName);
        string GetLocalizedLine(string fileName, string lineKey);
        Dictionary<string,string>GetLocalizedLines(string fileName, string[] lineKeys);
        bool LineExist(string fileName, string lineKey);

        AudioClip GetLocalizedAudioClip(string clipName);
        bool LocalizedAudioClipExist(string clipName);

        Texture2D GetLocalizedTexture2D(string textureName);
        bool LocalizedTexture2DExist(string textureName);

        string GetCurrentPathToLocalizationFolder();
        #endregion
    }
}