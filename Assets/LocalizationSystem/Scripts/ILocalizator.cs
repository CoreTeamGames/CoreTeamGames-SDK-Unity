using System.Collections.Generic;
using UnityEngine;

namespace CoreTeamGamesSDK.Localization
{
    /// <summary>
    /// The base interface for localizators
    /// </summary>
    public interface ILocalizator
    {
        #region Properties
        ///<summary>
        /// The current settings for localizator
        ///</summary>
        LocalizatorSettings Settings { get; }
        ///<summary>
        /// The current language what uses for localization
        ///</summary>
        Language CurrentLanguage { get; }
        ///<summary>
        /// The list of founded languages
        ///</summary>
        List<Language> Languages { get; }
        #endregion

        #region Methods
        ///<summary>
        /// This method for initialize the localizator and start it
        ///</summary>
        void Initialize();

        ///<summary>
        /// This method for switch language for localizator
        ///</summary>
        void SwitchLanguage(Language language);
        ///<summary>
        /// This method for get all languages in localization folder
        ///</summary>
        List<Language> GetAllLanguages();
        ///<summary>
        /// This method checks language file exist in localization folder or not
        ///</summary>
        bool LanguageExist(Language language);

        ///<summary>
        /// This method returns a localized splitted textfile for localization
        ///</summary>
        Dictionary<string, string> GetLocalizedTextFile(string fileName);
        ///<summary>
        /// This method returns a localized line from file by using lineKey
        ///</summary>
        string GetLocalizedLine(string fileName, string lineKey);
        ///<summary>
        /// This method returns a Dictionary of localized lines in file by using array of keys
        ///</summary>
        Dictionary<string, string> GetLocalizedLines(string fileName, string[] lineKeys);
        ///<summary>
        /// This method checks for localized line exist in file
        ///</summary>
        bool LineExist(string fileName, string lineKey);

        ///<summary>
        /// This method return a localized AudioClip
        ///</summary>
        AudioClip GetLocalizedAudioClip(string clipName);
        ///<summary>
        /// This method checks localized AudioClip exist or not
        ///</summary>
        bool LocalizedAudioClipExist(string clipName);

        ///<summary>
        /// This method return a localized Texture2D
        ///</summary>
        Texture2D GetLocalizedTexture2D(string textureName);
        ///<summary>
        /// This method checks localized Texture2D exist or not
        ///</summary>
        bool LocalizedTexture2DExist(string textureName);

        ///<summary>
        /// This method returns actual path to main folder of localization
        ///</summary>
        string GetCurrentPathToLocalizationFolder();
        #endregion
    }
}