using UnityEngine.Audio;
using UnityEngine;

namespace CoreTeamGamesSDK.SettingsService
{
    public class AudioMixerVolumeChanger : MonoBehaviour
    {
        [SerializeField] private SliderWithMixerExposedValue[] _sliderWithValue;
        [SerializeField] private AudioMixer _mixer;

        public void Awake()
        {
            SettingsService.Manager.OnValuesLoadedEvent += (SettingsValue[] _values) => { LoadValues(); };
            SettingsService.Manager.OnValuesUpdateEvent += (SettingsValue[] _values) => { ApplyVolumeToMixers(); };
        }

        public void LoadValues()
        {
            if (SettingsService.Manager != null)
            {
                foreach (var mixerGroup in _sliderWithValue)
                {
                    _mixer.SetFloat(mixerGroup.exposedValue, SettingsService.Manager.FindValue(mixerGroup.exposedValue).floatValue);
                    mixerGroup.Slider.onValueChanged.AddListener((float t) => ApplyVolumeToMixers());
                }
            }
        }

        public void ApplyVolumeToMixers()
        {
            foreach (var mixerGroup in _sliderWithValue)
            {
                _mixer.SetFloat(mixerGroup.exposedValue, mixerGroup.Slider.value);

                if (SettingsService.Manager != null)
                {
                    SettingsService.Manager.FindValue(mixerGroup.exposedValue).floatValue = mixerGroup.Slider.value;
                }
            }
        }
    }
}