using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using CoreTeamGamesSDK.Localization;

namespace CoreTeamGamesSDK.Localization
{
    [AddComponentMenu("CoreTeamGames/Localization/Localize TextMeshPro Text Group")]
    public class LocalizeTMPTextGroup : MonoBehaviour
    {
        #region Variables
        [SerializeField] private string _localizationFileName;
        [SerializeField] private List<LocalizeTMPTextGroupMember> _localizeTMPTextGroupMembers = new List<LocalizeTMPTextGroupMember>();
        [SerializeField] private LocalizationService _localizationService;
        #endregion

        #region Properties
        public string LocalizationFileName { get => _localizationFileName; }
        public List<LocalizeTMPTextGroupMember> LocalizeTMPTextGroupMembers { get => _localizeTMPTextGroupMembers; }
        public LocalizationService LocalizationService { get => _localizationService; }
        #endregion

        #region Code
        public void Start()
        {
            _localizationService = GameObject.FindGameObjectWithTag("LocalizationService").GetComponent<LocalizationService>();

            if (_localizationService == null)
            {
                Debug.LogError("Can't find Localization service!");
                return;
            }
            else if (_localizationService.CurrentLanguage.Path != "")
            {
                Localize();
            }
            _localizationService.onlanguageSelectedEvent += (Language language) => { Localize(); };
        }

        public void Localize()
        {
            if (_localizationService == null || _localizationService.CurrentLanguage.Path == "")
                return;

            List<string> _lineKeys = new List<string>();
            foreach (var localizeTMPTextGroupMember in _localizeTMPTextGroupMembers)
            {
                _lineKeys.Add(localizeTMPTextGroupMember.LineKey);
            }

            Dictionary<string, string> _localizedLines = _localizationService.GetLocalizedLines(_localizationFileName, _lineKeys.ToArray());
            foreach (var localizeTMPTextGroupMember in _localizeTMPTextGroupMembers)
            {
                if (_localizedLines.TryGetValue(localizeTMPTextGroupMember.LineKey, out string line))
                {
                    localizeTMPTextGroupMember.Text.text = line;
                }
            }
        }

        public void Add(LocalizeTMPTextGroupMember localizeTMPTextGroupMember) => _localizeTMPTextGroupMembers.Add(localizeTMPTextGroupMember);

        public void AddRange(IEnumerable<LocalizeTMPTextGroupMember> localizeTMPTextGroupMembers)
        {
            _localizeTMPTextGroupMembers.AddRange(localizeTMPTextGroupMembers);
        }
        private void OnDestroy()
        {
            if (_localizationService == null)
                return;

            _localizationService.onlanguageSelectedEvent -= (Language language) => { Localize(); };
        }
        #endregion
    }

    [Serializable]
    public class LocalizeTMPTextGroupMember
    {
        #region Variables
        [SerializeField] private TMP_Text _text;
        [SerializeField] private string _lineKey;
        #endregion

        #region Properties
        public TMP_Text Text { get => _text;}
        public string LineKey { get => _lineKey; }
        #endregion

        #region Constructors
        public LocalizeTMPTextGroupMember(TMP_Text text, string lineKey)
        {
            _text = text ?? throw new ArgumentNullException(nameof(text));
            _lineKey = lineKey ?? throw new ArgumentNullException(nameof(lineKey));
        }
        #endregion
    }
}