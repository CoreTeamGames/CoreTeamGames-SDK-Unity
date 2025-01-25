using UnityEngine;

namespace CoreTeamGamesSDK.SettingsService
{
    [System.Serializable]
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
            if (_value.ValueType == ESettingsValueType.boolValue)
            {
                _value.boolValue = (bool)value;
            }
            else if (_value.ValueType == ESettingsValueType.intValue)
            {
                _value.intValue = (int)value;

            }
            else if (_value.ValueType == ESettingsValueType.floatValue)
            {
                _value.floatValue = (float)value;

            }
            else if (_value.ValueType == ESettingsValueType.stringValue)
            {
                _value.stringValue = (string)value;

            }

            else
            {

            }
        }

        public SettingsValueWithUIElement(Object uiElement, SettingsValue value, EUIElementType elementType)
        {
            _uiElement = uiElement;
            _value = value;
            _elementType = elementType;
        }

    }
}