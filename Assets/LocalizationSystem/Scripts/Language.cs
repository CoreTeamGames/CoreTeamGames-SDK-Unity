using UnityEngine;

namespace CoreTeamGamesSDK.Localization
{
    [System.Serializable]
    public class Language
    {
        #region Variables
        [SerializeField] string _langLocale;
        [SerializeField] string _langName;
        [SerializeField] bool _isRTL;
        [SerializeField] string _path;
        #endregion

        #region Properties

        public string LangLocale { get => _langLocale; }
        public string LangName { get => _langName; }
        public bool IsRTL { get => _isRTL; }
        public string Path { get => _path; }
        #endregion

        #region Constructors
        public Language(string langCode, string langName, bool isRTL, string path)
        {
            _langLocale = langCode;
            _langName = langName;
            _isRTL = isRTL;
            _path = path;
        }
        #endregion
    }
}