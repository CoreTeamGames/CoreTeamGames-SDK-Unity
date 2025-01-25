using UnityEngine.UI;
using UnityEngine;
using System;

namespace CoreTeamGamesSDK.SettingsService
{
    [Serializable]
    public class SliderWithMixerExposedValue
    {
        #region Variables
        [SerializeField] private Slider _slider;
        [SerializeField] private string _exposedValue;
        #endregion

        #region Properties
        public Slider Slider => _slider;
        public string exposedValue => _exposedValue;
        #endregion
    }
}