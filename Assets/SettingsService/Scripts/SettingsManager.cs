using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace CoreTeamGamesSDK.SettingsService
{
    ///<summary>
    /// The settings manager what can save and load settings
    ///</summary>
    public class SettingsManager : MonoBehaviour
    {
        #region Variables
        [SerializeField] private bool _allowAutoLoadSettings = true;
        [SerializeField] SettingsValues _settingsValuesBaseList;

        private SettingsValue[] _settingsValues;
        #endregion

        #region Events
        public delegate void OnValuesUpdate(SettingsValue[] values);
        public delegate void OnValuesLoaded(SettingsValue[] values);
        public delegate void OnValuesSaved();

        public OnValuesUpdate OnValuesUpdateEvent;
        public OnValuesLoaded OnValuesLoadedEvent;
        public OnValuesSaved OnValuesSavedEvent;
        #endregion

        #region Properties
        public SettingsValues SettingsValuesBaseList => _settingsValuesBaseList;
        public SettingsValue[] SettingsValues => _settingsValues;
        #endregion

        #region Code
        private void Start()
        {
            if (_allowAutoLoadSettings)
            {
                LoadSettings();
            }
        }

        public void SetValues(SettingsValue[] values)
        {
            Dictionary<string, object> _values = new Dictionary<string, object>();
            foreach (var value in values)
            {
                object _objectValue = null;

                switch (value.ValueType)
                {
                    case ESettingsValueType.intValue:
                        _objectValue = value.intValue;
                        break;

                    case ESettingsValueType.floatValue:
                        _objectValue = value.floatValue;
                        break;

                    case ESettingsValueType.boolValue:
                        _objectValue = value.boolValue;
                        break;

                    case ESettingsValueType.stringValue:
                        _objectValue = value.stringValue;
                        break;

                    default:
                        break;
                }

                _values.Add(value.ValueName.ToLower(), _objectValue);
            }

            SettingsIO.WriteSettings(_values);
        }

        public void UpdateValues(SettingsValue[] values)
        {
            //Describe method
        }

        public void LoadSettings()
        {
            _settingsValues = GetValues();

            if (_settingsValues == null)
                return;

            OnValuesLoadedEvent?.Invoke(_settingsValues);
        }

        public SettingsValue FindValue(string valueName)
        {
            valueName = valueName.ToLower();

            foreach (var item in _settingsValues)
            {
                if (item.ValueName.ToLower() == valueName)
                    return item;
            }

            return null;
        }

        public SettingsValue[] GetValues()
        {
            Dictionary<string, object> _values = SettingsIO.ReadSettings();
            List<SettingsValue> _settingsValuesList = new List<SettingsValue>();

            if (_values.Count > 0)
            {
                foreach (var value in _settingsValuesBaseList.SettingsValuesList)
                {
                    if (_values.ContainsKey(value.ValueName.ToLower()))
                    {
                        _settingsValuesList.Add(new SettingsValue(value.ValueName, value.ValueType));

                        switch (value.ValueType)
                        {
                            case ESettingsValueType.intValue:
                                _settingsValuesList[_settingsValuesList.Count - 1].intValue = Convert.ToInt32(_values[value.ValueName.ToLower()]);
                                break;

                            case ESettingsValueType.floatValue:
                                _settingsValuesList[_settingsValuesList.Count - 1].floatValue = Convert.ToSingle(_values[value.ValueName.ToLower()]);
                                break;

                            case ESettingsValueType.boolValue:
                                _settingsValuesList[_settingsValuesList.Count - 1].boolValue = Convert.ToBoolean(_values[value.ValueName.ToLower()]);
                                break;

                            case ESettingsValueType.stringValue:
                                _settingsValuesList[_settingsValuesList.Count - 1].stringValue = Convert.ToString(_values[value.ValueName.ToLower()]);
                                break;

                            default:
                                break;
                        }
                    }
                    else
                    {
                        _settingsValuesList.Add(new SettingsValue(value.ValueName, value.ValueType));

                        switch (value.ValueType)
                        {
                            case ESettingsValueType.intValue:
                                _settingsValuesList[_settingsValuesList.Count - 1].intValue = value.intValue;
                                break;

                            case ESettingsValueType.floatValue:
                                _settingsValuesList[_settingsValuesList.Count - 1].floatValue = value.floatValue;
                                break;

                            case ESettingsValueType.boolValue:
                                _settingsValuesList[_settingsValuesList.Count - 1].boolValue = value.boolValue;
                                break;

                            case ESettingsValueType.stringValue:
                                _settingsValuesList[_settingsValuesList.Count - 1].stringValue = value.stringValue;
                                break;

                            default:
                                break;
                        }
                    }
                }
                return _settingsValuesList.ToArray();
            }
            else
            {
                return _settingsValuesBaseList.SettingsValuesList.ToArray();
            }
        }
        #endregion
    }
}