using Object = UnityEngine.Object;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    [Serializable]
    public class SettingsValueWithUIElement
    {
        [SerializeField] EUIElementType _elementType;
        [SerializeField] Object _uiElement;
        [SerializeField] SettingsValue _value;

        public EUIElementType ElementType => _elementType;
        public Object UIElement => _uiElement;
        public SettingsValue Value => _value;

        public void SetValue(object value)
        {
            if (_value.ValueType == SettingsValueType.boolValue)
            {
                _value.boolValue = (bool)value;
            }
            else if (_value.ValueType == SettingsValueType.intValue)
            {
                _value.intValue = (int)value;

            }
            else if (_value.ValueType == SettingsValueType.floatValue)
            {
                _value.floatValue = (float)value;

            }
            else if (_value.ValueType == SettingsValueType.stringValue)
            {
                _value.stringValue = (string)value;

            }

            else
            {

            }
        }
    
        public SettingsValueWithUIElement(Object uiElement,SettingsValue value,EUIElementType elementType)
        {
            _uiElement = uiElement;
            _value = value;
            _elementType = elementType;
        }
    }

    [SerializeField] private SettingsValueWithUIElement[] _settingsValuesWithUIElements;

    public void AddSettingsValues()
    {
        SettingsManager _manager = (SettingsManager)FindObjectOfType(typeof(SettingsManager));

        if (_manager == null)
        {
            Debug.LogError($"Can{"'"}t find SettingsManager! on Scene");
            return;
        }

        List<SettingsValueWithUIElement> _elements = new List<SettingsValueWithUIElement>();

        foreach (var value in _manager.SettingsValuesBaseList.SettingsValuesList)
        {
            _elements.Add(new SettingsValueWithUIElement(null,value,0));
        }
        _settingsValuesWithUIElements = _elements.ToArray();
    }
}