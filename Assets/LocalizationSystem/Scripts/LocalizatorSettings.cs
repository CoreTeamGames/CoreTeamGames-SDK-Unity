using UnityEngine;
using System;

namespace CoreTeamGamesSDK.Localization
{
    /// <summary>
    /// The settings class for localizator
    /// </summary>
    [System.Serializable]
    public class LocalizatorSettings
    {
        #region Variables
        [SerializeField] private string _localizedTextAssetSubDirectory;
        [SerializeField] private string _localizedAudioClipSubDirectory;
        [SerializeField] private string _localizedTexture2DSubDirectory;
        [SerializeField] private string _textAssetFileExtension;
        [SerializeField] private LocalizationFilesDirectory _localizationFilesDirectory;
        [SerializeField] private string _customLocalizationFilesDirectory;
        #endregion

        #region Properties
        /// <summary>
        /// The sub directory for Text assets
        /// </summary>
        public string LocalizedTextAssetSubDirectory { get => _localizedTextAssetSubDirectory; }
        /// <summary>
        /// The sub directory for AudioClip assets
        /// </summary>
        public string LocalizedAudioClipSubDirectory { get => _localizedAudioClipSubDirectory; }
        /// <summary>
        /// The sub directory for Texture2D assets
        /// </summary>
        public string LocalizedTexture2DSubDirectory { get => _localizedTexture2DSubDirectory; }
        /// <summary>
        /// The extension of text assets with localized lines
        /// </summary>
        public string TextAssetFileExtension { get => _textAssetFileExtension; }
        /// <summary>
        /// The presets of directory of localization files
        /// </summary>
        public LocalizationFilesDirectory LocalizationFilesDirectory { get => _localizationFilesDirectory; }
        /// <summary>
        /// the custom path to localization directory
        /// </summary>
        public string CustomLocalizationFilesDirectory { get => _customLocalizationFilesDirectory; }
        #endregion

        #region Constructors
        public LocalizatorSettings()
        {
            _localizedTextAssetSubDirectory = "Text";
            _localizedAudioClipSubDirectory = "Audio";
            _localizedTexture2DSubDirectory = "Textures";
            _textAssetFileExtension = "localize";
            _localizationFilesDirectory = LocalizationFilesDirectory.streamingAssets;
        }
        public LocalizatorSettings(string textAssetFileExtension)
        {
            _localizedTextAssetSubDirectory = "Text";
            _localizedAudioClipSubDirectory = "Audio";
            _localizedTexture2DSubDirectory = "Textures";
            _textAssetFileExtension = textAssetFileExtension;
            _localizationFilesDirectory = LocalizationFilesDirectory.streamingAssets;
        }
        public LocalizatorSettings(string localizedTextAssetSubDirectory, string localizedAudioClipSubDirectory, string localizedTexture2DSubDirectory, string textAssetFileExtension, LocalizationFilesDirectory localizationFilesDirectory, string customLocalizationFilesDirectory) : this(localizedTextAssetSubDirectory)
        {
            _localizedAudioClipSubDirectory = localizedAudioClipSubDirectory ?? throw new ArgumentNullException(nameof(localizedAudioClipSubDirectory));
            _localizedTexture2DSubDirectory = localizedTexture2DSubDirectory ?? throw new ArgumentNullException(nameof(localizedTexture2DSubDirectory));
            _textAssetFileExtension = textAssetFileExtension ?? throw new ArgumentNullException(nameof(textAssetFileExtension));
            _localizationFilesDirectory = localizationFilesDirectory;
            _customLocalizationFilesDirectory = customLocalizationFilesDirectory ?? throw new ArgumentNullException(nameof(customLocalizationFilesDirectory));
        }
        #endregion
    }

    /// <summary>
    /// The presets of directory of localization files
    /// </summary>
    public enum LocalizationFilesDirectory
    {
        streamingAssets,
        resources,
        dataPath,
        persistentDataPath,
        custom
    }
}