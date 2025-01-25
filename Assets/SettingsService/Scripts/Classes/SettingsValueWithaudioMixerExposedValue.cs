using UnityEngine;
using System;

namespace CoreTeamGamesSDK.SettingsService
{
    [Serializable]
    public class SettingsValueWithAudioMixerExposedValue
    {
        #region Variables
        [SerializeField] private string _settingsValue;
        [SerializeField] private string _mixerExposedValue;

        #endregion

        #region Properties
        public string SettingsValue => _settingsValue;
        public string MixerExposedValue => _mixerExposedValue;
        #endregion
    }
}