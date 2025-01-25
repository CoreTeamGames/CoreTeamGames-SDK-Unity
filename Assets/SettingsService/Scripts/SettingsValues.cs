using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using System;

namespace CoreTeamGamesSDK.SettingsService
{
    [CreateAssetMenu(menuName = "CoreTeamSDK/Settings/Values")]
    public class SettingsValues : ScriptableObject
    {
        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private List<SettingsValue> _settingsValues;

        public List<SettingsValue> SettingsValuesList
        {
            get
            {
                List<SettingsValue> _values = new List<SettingsValue>();
                foreach (var item in _settingsValues)
                {
                    _values.Add(item);
                }
                if (_audioMixer != null)
                {
                    string[] _mixerValues = GetExposedParameters(_audioMixer);
                    if (_mixerValues.Length != 0)
                    {
                        foreach (var item in _mixerValues)
                        {
                            SettingsValue _value = new SettingsValue(item, ESettingsValueType.floatValue);
                            if (_audioMixer.GetFloat(item, out float mixerFloatValue))
                            {
                                _value.floatValue = mixerFloatValue;
                            }
                            _values.Add(_value);
                        }
                    }
                }
                return _values;
            }
        }

        private string[] GetExposedParameters(AudioMixer mixer)
        {
            List<string> exposedParams = new List<string>();

            // Using reflection to access the AudioMixer's ExposedParameters
            var dynMixer = new UnityEditor.SerializedObject(mixer);
            var parameters = dynMixer.FindProperty("m_ExposedParameters");

            if (parameters != null && parameters.isArray)
            {
                for (int i = 0; i < parameters.arraySize; i++)
                {
                    var param = parameters.GetArrayElementAtIndex(i);
                    var nameProp = param.FindPropertyRelative("name");
                    if (nameProp != null)
                    {
                        exposedParams.Add(nameProp.stringValue);
                    }
                }
            }

            return exposedParams.ToArray();
        }
    }

    [Serializable]
    public class SettingsValue
    {
        [SerializeField] private string _valueName;
        [SerializeField] private ESettingsValueType _valueType;
        public string stringValue;
        public int intValue;
        public float floatValue;
        public bool boolValue;

        public SettingsValue(string valueName, ESettingsValueType valueType)
        {
            _valueName = valueName;
            _valueType = valueType;
        }

        public SettingsValue()
        { }

        public string ValueName => _valueName;
        public ESettingsValueType ValueType => _valueType;
    }


}