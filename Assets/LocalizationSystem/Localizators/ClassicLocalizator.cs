using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace CoreTeamGamesSDK.Localization
{
    public class ClassicLocalizator : ILocalizator
    {
        private LocalizatorSettings _settings = new LocalizatorSettings();
        private char _delimiter = ',';
        private char _specialSymbolForIgnoreDelimiter = '\"';
        private string _pathToLocalizationFolder;
        private Language _currentLanguage;
        private List<Language> _languages;

        public LocalizatorSettings Settings { get => _settings; }

        public Language CurrentLanguage { get => _currentLanguage; }
        public List<Language> Languages { get => _languages; }

        public List<Language> GetAllLanguages()
        {
            List<Language> _languages = new List<Language>();
            string[] _paths = Directory.GetDirectories(_pathToLocalizationFolder);
            foreach (var path in _paths)
            {
                if (File.Exists($"{path}/Language.{_settings.TextAssetFileExtension}"))
                {
                    string[] _languageRawData = File.ReadAllLines($"{path}/Language.{_settings.TextAssetFileExtension}")[0].Split(_delimiter);
                    if (_languageRawData.Length == 3)
                        _languages.Add(new Language(_languageRawData[0], _languageRawData[1], Convert.ToBoolean(_languageRawData[2]), $"{path}"));
                    else
                    {
                        Debug.LogError($"Format Error in language file on path\"{path}/Language.{_settings.TextAssetFileExtension}\"");
                    }
                }
                else
                {
                    Debug.LogWarning($"Folder {path} doesn't contain any language or file {'"'}Language.{_settings.TextAssetFileExtension}{'"'} doesn't found or exist");
                }
            }
            return _languages;
        }

        public string GetCurrentPathToLocalizationFolder()
        {
            string _path;
            switch (_settings.LocalizationFilesDirectory)
            {
                case LocalizationFilesDirectory.streamingAssets:
                    _path = Application.streamingAssetsPath;
                    break;
                case LocalizationFilesDirectory.resources:
                    _path = Application.dataPath + "/resources";
                    break;
                case LocalizationFilesDirectory.dataPath:
                    _path = Application.dataPath;
                    break;
                case LocalizationFilesDirectory.persistentDataPath:
                    _path = Application.persistentDataPath;
                    break;
                case LocalizationFilesDirectory.custom:
                    _path = _settings.CustomLocalizationFilesDirectory;
                    break;
                default:
                    _path = _settings.CustomLocalizationFilesDirectory;
                    break;
            }
            return _path + "/Localization";
        }

        public AudioClip GetLocalizedAudioClip(string clipName)
        {
            if (!File.Exists($"{CurrentLanguage.Path}/{Settings.LocalizedAudioClipSubDirectory}/{clipName}.wav"))
            {
                Debug.LogError($"Can\'t find the audio file on path\"$\"{{Currentlanguage.Path}}/{{Settings.LocalizedAudioClipSubDirectory}}/{{clipName}}.wav\"\"");
                return null;
            }

            using (UnityWebRequest _request = UnityWebRequestMultimedia.GetAudioClip($"{CurrentLanguage.Path}/{Settings.LocalizedAudioClipSubDirectory}/{clipName}.wav", AudioType.WAV))
            {
                _request.SendWebRequest();

                if (_request.isNetworkError)
                {
                    Debug.LogError(_request.error);
                    return null;
                }

                while (_request.downloadProgress < 1)
                {

                }

                return DownloadHandlerAudioClip.GetContent(_request);
            };
        }

        public string GetLocalizedLine(string fileName, string lineKey)
        {
            Dictionary<string, string> _localizedFile = GetLocalizedTextFile(fileName);

            if (_localizedFile == null)
                return null;

            lineKey = lineKey.ToLower();

            if (_localizedFile.TryGetValue(lineKey, out string line))
            {
                return line;
            }
            Debug.LogError($"Can\'t find localized line by key: \"{lineKey}\" on path: \"{_pathToLocalizationFolder}/{Settings.LocalizedTextAssetSubDirectory}/{fileName}.{Settings.TextAssetFileExtension.Trim('.')}\"");
            return null;
        }
        public string[] SplitLocalizedLine(string line)
        {
            string[] _splittedLine = line.Split(_delimiter);

            if (_splittedLine.Length < 2)
                return null;

            if (_splittedLine[1][0] == _specialSymbolForIgnoreDelimiter)
            {
                string localizedLine = "";
                for (int i = 1; i < _splittedLine.Length; i++)
                {
                    localizedLine += $"{_delimiter}{_splittedLine[i]}";
                }
                return new string[] { _splittedLine[0], localizedLine.Remove(0, 2) };
            }
            else
            {
                return _splittedLine;
            }
        }
        public Dictionary<string, string> GetLocalizedTextFile(string fileName)
        {
            if (!File.Exists($"{_currentLanguage.Path}/{Settings.LocalizedTextAssetSubDirectory}/{fileName}.{Settings.TextAssetFileExtension.Trim('.')}"))
            {
                Debug.LogError($"Can't find file! file name is: \"{fileName}\"");
                return null;
            }
            Dictionary<string, string> _splittedFile = new Dictionary<string, string>();
            string[] _file = File.ReadAllLines($"{_currentLanguage.Path}/{Settings.LocalizedTextAssetSubDirectory}/{fileName}.{Settings.TextAssetFileExtension.Trim('.')}");
            string[] _splittedLine;
            foreach (var _line in _file)
            {
                _splittedLine = SplitLocalizedLine(_line);
                _splittedFile.Add(_splittedLine[0].ToLower(), _splittedLine[1]);
            }
            return _splittedFile;
        }

        public Texture2D GetLocalizedTexture2D(string textureName)
        {
            throw new System.NotImplementedException();
        }

        public void Initialize()
        {
            _pathToLocalizationFolder = GetCurrentPathToLocalizationFolder();
            _languages = GetAllLanguages();
            if (_languages.Count == 0)
                return;
            _currentLanguage = _languages[0];
        }

        public bool LanguageExist(Language language)
        {
            if (_languages.Count == 0)
                return false;

            foreach (var languageInFiles in _languages)
            {
                if (language == languageInFiles)
                    return true;
            }
            return false;
        }

        public bool LineExist(string fileName, string lineKey)
        {
            return GetLocalizedLine(fileName, lineKey) != null ? true : false;
        }

        public bool LocalizedAudioClipExist(string clipName)
        {
            throw new System.NotImplementedException();
        }

        public bool LocalizedTexture2DExist(string textureName)
        {
            throw new System.NotImplementedException();
        }

        public void SwitchLanguage(Language language)
        {
            if (!LanguageExist(language))
                return;

            _currentLanguage = language;
        }

        public Dictionary<string, string> GetLocalizedLines(string fileName, string[] lineKeys)
        {
            Dictionary<string, string> _localizedFile = GetLocalizedTextFile(fileName);

            if (_localizedFile == null)
                return null;

            Dictionary<string, string> _returnLines = new Dictionary<string, string>();

            foreach (string lineKey in lineKeys)
            {
                if (_localizedFile.TryGetValue(lineKey.ToLower(), out string line))
                {
                    _returnLines.Add(lineKey, line);
                }
                else
                {
                    Debug.LogError($"Can\'t find localized line by key: \"{lineKey}\" on path: \"{_pathToLocalizationFolder}/{Settings.LocalizedTextAssetSubDirectory}/{fileName}.{Settings.TextAssetFileExtension.Trim('.')}\"");
                }
            }
            return _returnLines;
        }
    }
}