using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata;
using Microsoft.SqlServer.Server;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "CoreTeamSDK/Settings/Values")]
public class SettingsValues : ScriptableObject
{
    [SerializeField] private List<SettingsValue> _settingsValues;

    public List<SettingsValue> SettingsValuesList => _settingsValues;
}

[Serializable]
public class SettingsValue
{
    [SerializeField] private string _valueName;
    [SerializeField] private SettingsValueType _valueType;
    public string stringValue;
    public int intValue;
    public float floatValue;
    public bool boolValue;

    public string ValueName => _valueName;
    public SettingsValueType ValueType => _valueType;
}

public enum SettingsValueType
{
    intValue,
    floatValue,
    boolValue,
    stringValue,
}