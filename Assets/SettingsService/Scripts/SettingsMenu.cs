using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

namespace CoreTeamGamesSDK.SettingsService
{
    public class SettingsMenu : MonoBehaviour
    {
        [SerializeField] private SettingsManager _manager;
        [SerializeField] private SettingsValueWithUIElement[] _settingsValuesWithUIElements;

        private void Awake()
        {
            _manager = SettingsService.Manager;

            if (_manager == null)
                gameObject.SetActive(false);

            _manager.OnValuesLoadedEvent += SetupValues;
        }

        private void OnDestroy()
        {
            _manager.OnValuesLoadedEvent -= SetupValues;
        }

        public void SetupValues(SettingsValue[] values)
        {
            if (values == null)
                return;

            foreach (var value in values)
            {
                foreach (var valueAndUIElement in _settingsValuesWithUIElements)
                {
                    if (valueAndUIElement.Value.ValueName == value.ValueName)
                    {
                        object _value = "";
                        switch (valueAndUIElement.Value.ValueType)
                        {
                            case ESettingsValueType.intValue:
                                _value = value.intValue;
                                break;

                            case ESettingsValueType.floatValue:
                                _value = value.floatValue;
                                break;

                            case ESettingsValueType.boolValue:
                                _value = value.boolValue;
                                break;

                            case ESettingsValueType.stringValue:
                                _value = value.stringValue;
                                break;
                        }

                        switch (valueAndUIElement.ElementType)
                        {
                            case EUIElementType.Toggle:
                                (valueAndUIElement.UIElement as Toggle).SetIsOnWithoutNotify(Convert.ToBoolean(_value));
                                break;

                            case EUIElementType.InputField:
                                (valueAndUIElement.UIElement as TMP_InputField).SetTextWithoutNotify(Convert.ToString(_value));
                                break;

                            case EUIElementType.DropDown:
                                (valueAndUIElement.UIElement as TMP_Dropdown).SetValueWithoutNotify(Convert.ToInt32(_value));
                                break;

                            case EUIElementType.Slider:
                                (valueAndUIElement.UIElement as Slider).SetValueWithoutNotify(Convert.ToSingle(_value));
                                break;

                            default:
                                break;
                        }
                    }
                }
            }
        }

        public void LoadValues()
        {
            if (_manager == null)
                return;

            SettingsValue[] values;

            _manager.LoadSettings();
            values = _manager.SettingsValues;

            if (values == null)
                return;


            SetupValues(values);
        }

        public void SaveValues(SettingsValue[] values)
        {
            if (_manager == null)
                return;

            _manager.SetValues(values);
        }

        public void SaveAndApplyValues()
        {
            List<SettingsValue> _settingsValues = new List<SettingsValue>();
            SettingsValue _settingsValue;
            string _uiElementValue = "";

            foreach (var item in _settingsValuesWithUIElements)
            {
                switch (item.ElementType)
                {
                    case EUIElementType.Toggle:
                        _uiElementValue = (item.UIElement as Toggle).isOn.ToString();
                        break;

                    case EUIElementType.InputField:
                        _uiElementValue = (item.UIElement as TMP_InputField).text.ToString();
                        break;

                    case EUIElementType.DropDown:
                        _uiElementValue = (item.UIElement as TMP_Dropdown).value.ToString();
                        break;

                    case EUIElementType.Slider:
                        _uiElementValue = (item.UIElement as Slider).value.ToString();
                        break;

                    default:
                        break;
                }

                _settingsValue = new SettingsValue(item.Value.ValueName, item.Value.ValueType);

                switch (item.Value.ValueType)
                {
                    case ESettingsValueType.intValue:
                        _settingsValue.intValue = Convert.ToInt32(_uiElementValue);
                        break;

                    case ESettingsValueType.floatValue:
                        _settingsValue.floatValue = Convert.ToSingle(_uiElementValue);
                        break;

                    case ESettingsValueType.boolValue:
                        _settingsValue.boolValue = Convert.ToBoolean(_uiElementValue);
                        break;

                    case ESettingsValueType.stringValue:
                        _settingsValue.stringValue = _uiElementValue;
                        break;

                    default:
                        break;
                }

                _settingsValues.Add(_settingsValue);
            }

            SaveValues(_settingsValues.ToArray());
        }

        public void UpdateContent()
        {
            List<SettingsValueWithUIElement> _list = new List<SettingsValueWithUIElement>();
            SettingsManager _manager = (SettingsManager)FindObjectOfType(typeof(SettingsManager));

            if (_manager == null)
            {
                Debug.LogError($"Can{"'"}t find SettingsManager!");
            }

            if (_settingsValuesWithUIElements.Length != 0)
            {
                foreach (var value in _manager.SettingsValuesBaseList.SettingsValuesList)
                {
                    bool _needToCreate = false;

                    for (int i = 0; i < _settingsValuesWithUIElements.Length; i++)
                    {
                        if (value.ValueName.ToLower() == _settingsValuesWithUIElements[i].Value.ValueName.ToLower())
                        {
                            _list.Add(new SettingsValueWithUIElement(_settingsValuesWithUIElements[i].UIElement, value, _settingsValuesWithUIElements[i].ElementType));
                            _needToCreate = false;
                            break;
                        }
                        _needToCreate = true;
                    }
                    if (_needToCreate)
                    {
                        _list.Add(new SettingsValueWithUIElement(null, value, EUIElementType.Toggle));
                    }

                }
            }
            else
            {
                foreach (var value in _manager.SettingsValuesBaseList.SettingsValuesList)
                {
                    _list.Add(new SettingsValueWithUIElement(null, value, EUIElementType.Toggle));
                }
            }

            _settingsValuesWithUIElements = _list.ToArray();
        }
    }
}