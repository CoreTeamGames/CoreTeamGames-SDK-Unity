using System.Collections.Generic;
using UnityEngine;

namespace CoreTeamGamesSDK.Localization
{
    ///<summary>
    /// Standard fallback localizator for get localization from StreamingAssets folder
    ///</summary>
    public class FallBackLocalizator : ILocalizator
    {
        [SerializeField] TextAssetArray _textAssets;
        [SerializeField] AudioClipArray _audioAssets;
        [SerializeField] Texture2DArray _textureAssets;
        public LocalizatorSettings Settings => throw new System.NotImplementedException();

        public Language CurrentLanguage => throw new System.NotImplementedException();

        public List<Language> Languages => throw new System.NotImplementedException();

        public List<Language> GetAllLanguages()
        {
            throw new System.NotImplementedException();
        }

        public string GetCurrentPathToLocalizationFolder()
        {
            throw new System.NotImplementedException();
        }

        public AudioClip GetLocalizedAudioClip(string clipName)
        {
            throw new System.NotImplementedException();
        }

        public string GetLocalizedLine(string fileName, string lineKey)
        {
            throw new System.NotImplementedException();
        }

        public Dictionary<string, string> GetLocalizedLines(string fileName, string[] lineKeys)
        {
            throw new System.NotImplementedException();
        }

        public Dictionary<string, string> GetLocalizedTextFile(string fileName)
        {
            throw new System.NotImplementedException();
        }

        public Texture2D GetLocalizedTexture2D(string textureName)
        {
            throw new System.NotImplementedException();
        }

        public void Initialize()
        {
            throw new System.NotImplementedException();
        }

        public bool LanguageExist(Language language)
        {
            throw new System.NotImplementedException();
        }

        public bool LineExist(string fileName, string lineKey)
        {
            throw new System.NotImplementedException();
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
            throw new System.NotImplementedException();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}