using UnityEngine;
using System;

namespace CoreTeamGamesSDK.Other.VariableStorage
{
    ///<summary>
    /// The variable in VariableStorage
    ///</summary>
    [Serializable]
    public class Variable
    {
        #region Variables
        // The name of variable
        [SerializeField]
        private string _variableName;
        // The type of variable
        [SerializeField]
        private EVariableType _variableType;
        ///<summary>
        /// The int value of variable
        ///</summary>
        public int integerValue;
        ///<summary>
        /// The float value of variable
        ///</summary>
        public float floatValue;
        ///<summary>
        /// The bool value of variable
        ///</summary>
        public bool boolValue;
        ///<summary>
        /// The string value of variable
        ///</summary>
        public string stringValue;
        ///<summary>
        /// The object value of variable
        ///</summary>
        public object objectValue;
        ///<summary>
        /// The enumerator index value of variable
        ///</summary>
        public int enumIndexValue;
        #endregion

        #region Properties
        ///<summary>
        /// The name of variable
        ///</summary>
        public string VariableName => _variableName;
        ///<summary>
        /// The type of variable
        ///</summary>
        public EVariableType VariableType => _variableType;
        #endregion

        #region Constructors
        ///<summary>
        /// Creates new variable with integer value
        ///</summary>
        public Variable(string variableName, int integerValue)
        {
            _variableName = variableName;
            _variableType = EVariableType.Integer;
            this.integerValue = integerValue;
        }

        ///<summary>
        /// Creates new variable with float value
        ///</summary>
        public Variable(string variableName, float floatValue)
        {
            _variableName = variableName;
            _variableType = EVariableType.Float;
            this.floatValue = floatValue;
        }

        ///<summary>
        /// Creates new variable with boolean value
        ///</summary>
        public Variable(string variableName, bool boolValue)
        {
            _variableName = variableName;
            _variableType = EVariableType.Boolean;
            this.boolValue = boolValue;
        }

        ///<summary>
        /// Creates new variable with string value
        ///</summary>
        public Variable(string variableName, string stringValue)
        {
            _variableName = variableName;
            _variableType = EVariableType.String;
            this.stringValue = stringValue;
        }

        ///<summary>
        /// Creates new variable with object value
        ///</summary>
        public Variable(string variableName, object objectValue)
        {
            _variableName = variableName;
            _variableType = EVariableType.Object;
            this.objectValue = objectValue;
        }

        ///<summary>
        /// Creates new variable with selected variable type
        ///</summary>
        public Variable(string variableName, EVariableType variableType)
        {
            _variableName = variableName;
            _variableType = variableType;
        }
        #endregion

        #region Getters
        public object GetVariableValue()
        {
            switch (_variableType)
            {
                case EVariableType.Integer:
                    return integerValue;

                case EVariableType.Float:
                    return floatValue;

                case EVariableType.Boolean:
                    return boolValue;

                case EVariableType.String:
                    return stringValue;

                case EVariableType.Object:
                    return objectValue;

                case EVariableType.EnumIndex:
                    return enumIndexValue;
                default:
                    return null;
            }
        }

        public int GetEnumIndex<T>(T enumerator) where T : IComparable, IFormattable, IConvertible
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("enumerator must be enum type");

            if (_variableType != EVariableType.EnumIndex)
            {
                Debug.LogError("VariableType is not enumerator index");
                return 0;
            }

            if (typeof(T).GetEnumValues().Length < enumIndexValue)
            {
                return typeof(T).GetEnumValues().Length;
            }
            else if (enumIndexValue < 0)
            {
                return 0;
            }
            else
            {
                return enumIndexValue;
            }
        }
        #endregion

        #region Setters
        public void SetVariableValue(object variableValue)
        {
            switch (_variableType)
            {
                case EVariableType.Integer:
                    if (variableValue.GetType() == typeof(int))
                    {
                        integerValue = (int)variableValue;
                    }
                    break;

                case EVariableType.Float:
                    if (variableValue.GetType() == typeof(float))
                    {
                        floatValue = (float)variableValue;
                    }
                    break;

                case EVariableType.Boolean:
                    if (variableValue.GetType() == typeof(bool))
                    {
                        boolValue = (bool)variableValue;
                    }
                    break;

                case EVariableType.String:
                    if (variableValue.GetType() == typeof(string))
                    {
                        stringValue = (string)variableValue;
                    }
                    break;

                case EVariableType.Object:
                    if (variableValue.GetType() == typeof(UnityEngine.Object))
                    {
                        objectValue = variableValue;
                    }
                    break;

                case EVariableType.EnumIndex:
                    if (variableValue.GetType() == typeof(Enum))
                    {
                        enumIndexValue = (int)variableValue;
                    }
                    break;

                default:
                    break;
            }
        }
        #endregion
    }
}