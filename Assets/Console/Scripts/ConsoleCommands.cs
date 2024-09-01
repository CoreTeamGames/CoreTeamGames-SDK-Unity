using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata;
using UnityEngine;

[CreateAssetMenu(menuName ="CoreTeamSDK/Console/ConsoleCommands")]
public class ConsoleCommands : ScriptableObject
{
[SerializeField] ConsoleCommand[] _commands;
}

[Serializable]
public class ConsoleCommand
{
    [SerializeField] string _commandName;
    [SerializeField] string _commandDescription;
    [SerializeField] Action _action;
    [SerializeField] Argument[] _arguments;
}
[Serializable]
public class Argument
{
    [SerializeField] ArgumentType _type;
    [SerializeField] object _defaultValue;
    [SerializeField] string _argumentDescription;
}
public enum ArgumentType
{
    INT,
    FLOAT,
    STRING,
    VECTOR2,
    VECTOR3,
    VECTOR4,
}